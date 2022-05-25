using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class DiamondEnemy : EnemyBase
{
    [Header("Diamond specific variables")]
    [SerializeField] private float chargeSpeed;
    [SerializeField] private List<Transform> firePoints; 

    public override IEnumerator MeleeAttack()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator RangedAttack()
    {
        for (int i = 0; i == 5; i++)
        {
            foreach (var firePoint in firePoints)
            {
                GameObject proj = Instantiate(projectile, firePoint.position, firePoint.rotation);

                Rigidbody2D rigidbody2D = proj.GetComponent<Rigidbody2D>();
                Projectile component = proj.GetComponent<Projectile>();

                component.owner = gameObject;
                component.baseDamage = baseDamage;
                component.selfDamage = false;

                rigidbody2D.AddForce(firePoint.up * 20f, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(0.2f);
        }
        
    }

    public override IEnumerator SpecialAttack()
    {
        enemyMovement.CancelInvoke("ChasePlayer");

        animator.SetTrigger("Charging");
        Vector2 lastPlayerPosition = target.position;
        Vector2 dir = (lastPlayerPosition - enemyMovement.rb.position).normalized;
        Vector2 force = dir * chargeSpeed * Time.deltaTime;
        enemyMovement.rb.AddForce(force);

        yield return new WaitForSeconds(1);
        animator.SetTrigger("SpecialUsed");
        RangedAttack();

        yield return new WaitForSeconds(specialCooldown);
        specialOnCooldown = false;
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
        SpecialBrain(MeleeAttackStandard(), SpecialAttack(), true);
    }
}
