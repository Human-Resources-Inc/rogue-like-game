using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using System.Threading.Tasks;

public class SquareEnemy : EnemyBase
{
    public override IEnumerator MeleeAttack()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator RangedAttack()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator SpecialAttack()
    {
        enemyMovement.CancelInvoke("ChasePlayer");

        Vector3 lastPlayerPosition = target.position;
        animator.SetTrigger("Charging");
        enemyMovement.rb.mass = 100;

        yield return new WaitForSeconds(specialChargeTime);
        enemyMovement.rb.mass = 1;
        animator.SetTrigger("SpecialUsed");
        transform.position = lastPlayerPosition - new Vector3(0.5f, 0.5f);

        enemyMovement.InvokeRepeating("ChasePlayer", 0, .5f);

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
