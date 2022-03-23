using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject owner;
    public List<PowerUps> powerUps;
    public int baseDamage;
    public bool selfDamage = false;


    private int GetTrueDamage()
    {
        int damage = baseDamage;

        if (powerUps == null)
            return damage;

        if (powerUps.Count != 0)
        {
            foreach (var modifier in powerUps)
            {
                damage += modifier.damageModifier;
            }
        }
        return damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject entity = collision.gameObject;
        IDamageable entityInterface = entity.GetComponent<IDamageable>();
        if (entity != owner && entity.GetComponent<Projectile>() == null)
        {
            if (entityInterface != null)
            {
                entityInterface.Damage(GetTrueDamage());
            }
            Destroy(gameObject);
        }
    }
}
