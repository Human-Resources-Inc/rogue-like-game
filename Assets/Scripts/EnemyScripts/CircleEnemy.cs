using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CircleEnemy : EnemyBase
{
    public override IEnumerator MeleeAttack()
    {
        yield break;
    }

    public override IEnumerator RangedAttack()
    {
        yield break;
    }

    public override IEnumerator SpecialAttack()
    {
        yield return null;
    }

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        currentHealth = maxHealth;
        target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        NoSpecialBrain(RangedAttackStandard());
    }
}
