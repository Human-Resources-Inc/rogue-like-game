using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Pathfinding;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    #region Стандартные атрибуты противника
    // --------------------------
    public Transform target;
    public bool isChasing;
    // --------------------------
    [Header("Health")]
    public int maxHealth;
    public int currentHealth;
    // --------------------------
    [Header("Attack related variables")]
    public Transform firePoint;
    public GameObject projectile;
    public int baseDamage;
    public float seeRange;
    public float fireRange;
    public float meleeRange;
    public float attackCooldown;
    public bool attackOnCooldown = false;
    public bool inAttackRange = false;
    // --------------------------
    [Header("Special related variables")]
    public bool hasSpecial = false;
    public bool specialOnCooldown;
    public float specialCooldown;
    public int specialChargeTime;
    public float specialRange;
    // --------------------------
    [Header("Utility variables")]
    public Animator animator;
    #endregion
    #region Навигация
    [HideInInspector] public EnemyMovement enemyMovement;
    #endregion


    #region Гизмос
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, seeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, specialRange);
    }
    #endregion

    public void Damage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (!isChasing && !enemyMovement.IsInvoking("ChasePlayer"))
        {
            isChasing = true;
            enemyMovement.CancelInvoke("Roam");
            enemyMovement.InvokeRepeating("ChasePlayer", 0, .5f);
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    #region Методы для создания кастомных атак
    public abstract IEnumerator MeleeAttack();

    public abstract IEnumerator RangedAttack();

    public abstract IEnumerator SpecialAttack();
    #endregion

    #region Стандартные типы атак
    public IEnumerator MeleeAttackStandard()
    {
        if (attackOnCooldown) yield break;


    }

    public IEnumerator RangedAttackStandard()
    {
        if (attackOnCooldown) yield break;

        Debug.Log("Ranged attack!");
        GameObject proj = Instantiate(projectile, firePoint.position, firePoint.rotation);

        Rigidbody2D rigidbody2D = proj.GetComponent<Rigidbody2D>();
        Projectile component = proj.GetComponent<Projectile>();
        Debug.Log("Spawned projectile");

        component.owner = gameObject;
        component.baseDamage = baseDamage;
        component.selfDamage = false;

        rigidbody2D.AddForce(firePoint.up * 20f, ForceMode2D.Impulse);
        attackOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
    }
    #endregion

    #region Логика атаки противника
    /// <summary>
    /// Метод для мобов не имеющих спец. атаку
    /// </summary>
    /// <param name="routine"></param>
    /// <param name="melee"></param>
    public void NoSpecialBrain(IEnumerator routine, bool melee = false)
    {
        if (isChasing)
        {
            if (UtilsClass.CanSeePlayer(transform.position, target.position))
            {
                if (UtilsClass.DetectPlayerInRange(transform.position, melee ? meleeRange : fireRange))
                {
                    inAttackRange = true;
                    StartCoroutine(routine);
                }
                else inAttackRange = false;
            }
        }
    }

    /// <summary>
    /// Метод для мобов имеющих спец. атаку
    /// </summary>
    /// <param name="routineNormal"></param>
    /// <param name="routineSpecial"></param>
    /// <param name="melee"></param>
    public void SpecialBrain(IEnumerator routineNormal, IEnumerator routineSpecial, bool melee = false)
    {
        Debug.Log("SpecialBrain is called");
        if (isChasing)
        {
            if (UtilsClass.CanSeePlayer(transform.position, target.position))
            {
                switch (!specialOnCooldown)
                {
                    // Используем специальную атаку, если она не на перезарядке
                    case true:
                        if (UtilsClass.DetectPlayerInRange(transform.position, specialRange))
                        {
                            specialOnCooldown = true;
                            inAttackRange = true;
                            StartCoroutine(routineSpecial);
                        }
                        break;
                    // Иначе, используем обычные атаки.
                    case false:
                        if (UtilsClass.DetectPlayerInRange(transform.position, melee ? meleeRange : fireRange))
                        {
                            inAttackRange = true;
                            StartCoroutine(routineNormal);
                        }
                        else inAttackRange = false;
                        break;
                }
            }
        }
    }
    #endregion

    public IEnumerator Die()
    {
        GameManager.instance.enemiesKilled++;
        animator.SetTrigger("Died");
        enemyMovement.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    
}
