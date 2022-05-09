using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using System.Threading.Tasks;

public class SquareEnemy : EnemyBase, IDamageable
{
    public void Damage(int damage)
    {
        currentHealth -= damage;

        if (!isChasing && !enemyMovement.IsInvoking("ChasePlayer"))
        {
            isChasing = true;
            enemyMovement.CancelInvoke("Roam");
            enemyMovement.InvokeRepeating("ChasePlayer", 0, .5f);
        }

        if (currentHealth <= 0) Destroy(gameObject);
    }

    public override void SpecialAttack()
    {
        // НЕ РАБОТАЕТ!
        enemyMovement.CancelInvoke("ChasePlayer");
        Vector3 lastPlayerPosition = target.position;
        animator.SetBool("Charging", true);
        animator.SetBool("Charging", false);
        transform.position = lastPlayerPosition;
        animator.SetBool("Used", true);
        animator.SetBool("Used", false);
        enemyMovement.InvokeRepeating("ChasePlayer", 0, .5f);
        //TODO: (для mili) сделать отчет кулдауна
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        currentHealth = maxHealth;
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        #region Логика атаки противника
        if (isChasing)
        {
            if (UtilsClass.CanSeePlayer(transform.position, target.position))
            {
                switch (hasSpecial && !specialOnCooldown)
                {
                    // Используем специальную атаку, если такая существует
                    // и она не на перезарядке
                    case true:
                        if (UtilsClass.DetectPlayerInRange(transform.position, specialRange))
                        {
                            Debug.Log("Using Special!");
                            specialOnCooldown = true;
                            SpecialAttack();
                        }
                        break;
                    // Иначе, используем обычные атаки.
                    // Этот код выполняется при случае что хотя бы один из атрибутов(meleeRange || fireRange)
                    // имеют значения
                    case false:
                        if (UtilsClass.DetectPlayerInRange(transform.position, meleeRange))
                        {
                            MeleeAttack();
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
