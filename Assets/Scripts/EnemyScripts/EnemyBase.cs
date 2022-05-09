using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Pathfinding;

public abstract class EnemyBase : MonoBehaviour
{
    #region Стандартные атрибуты противника
    // --------------------------
    public Transform target;
    public bool isChasing;
    // --------------------------
    public int maxHealth;
    public int currentHealth;
    // --------------------------
    public float seeRange;
    public float fireRange;
    public float meleeRange;
    // --------------------------
    public bool hasSpecial = false;
    public float specialCooldown;
    public bool specialOnCooldown;
    public float specialRange;
    // --------------------------
    public Animator animator;
    // --------------------------
    public GameObject projectile;
    #endregion
    #region Навигация
    [HideInInspector] public EnemyMovement enemyMovement;
    #endregion


    private void Start()
    {
        
    }

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

    public void MeleeAttack()
    {
        Debug.Log("Melee Attack");
    }

    public void RangedAttack()
    {
        Debug.Log("Ranged Attack");
    }

    public abstract void SpecialAttack();


    
}
