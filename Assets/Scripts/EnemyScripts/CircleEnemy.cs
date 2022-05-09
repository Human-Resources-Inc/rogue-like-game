using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CircleEnemy : EnemyBase, IDamageable
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
        return;
    }

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        currentHealth = maxHealth;
        target = FindObjectOfType<Player>().transform;
    }

    private void Update()
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
                        SpecialAttack();
                        break;
                    // Иначе, используем обычные атаки.
                    // Этот код выполняется при случае что хотя бы один из атрибутов(meleeRange || fireRange)
                    // имеют значения
                    case false:
                        if (UtilsClass.DetectPlayerInRange(transform.position, fireRange))
                        {
                            RangedAttack();
                        }
                            
                        break;
                }
            }
        }
        #endregion
    }
}
