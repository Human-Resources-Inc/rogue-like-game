using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyAttack : MonoBehaviour
{
    public float meleeRange;
    public float rangedRange;
    public int meleeDamage;
    public int rangedDamage;
    public GameObject projectilePrefab; // устанавливает чем моб "стреляет"
    public float force;
    public Transform firePoint;
    public Enemy enemy;

    private float lastShotAt = float.NegativeInfinity;
    
    [SerializeField]
    private float delayBetweenShots;


    private void Update()
    {
        //Time.time > lastRepath + repathTimeChasing
        if (Time.time > lastShotAt + delayBetweenShots && enemy.isChasing)
        {
            lastShotAt = Time.time;
            if (UtilsClass.DetectPlayerInRange(transform.position, meleeRange)) MeleeAttack();
            else if (UtilsClass.DetectPlayerInRange(transform.position, rangedRange)) RangedAttack();
        }

        
    }

    private void MeleeAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right);

        if (hit)
        {
            Debug.Log(hit.transform.gameObject.name);
            Player player = hit.transform.GetComponent<Player>();
            //Debug.Log(player);
            if (player != null)
            {
                player.Damage(meleeDamage);
            }
        }
    }

    private void RangedAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position + new Vector3(0, 0), Quaternion.identity);

        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        Rigidbody2D rigidbody2D = projectile.GetComponent<Rigidbody2D>();
        Projectile component = projectile.GetComponent<Projectile>();

        component.owner = gameObject;
        component.baseDamage = rangedDamage;
        component.powerUps = null;
        component.selfDamage = false;

        rigidbody2D.AddForce(firePoint.up * force, ForceMode2D.Impulse);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, meleeRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangedRange);
    }
#endif
}
