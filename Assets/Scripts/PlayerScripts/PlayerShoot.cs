using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float force = 20f;

    private int baseDamage = 20;

    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform firePoint;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        Rigidbody2D rigidbody2D = projectile.GetComponent<Rigidbody2D>();
        Projectile component = projectile.GetComponent<Projectile>();

        component.owner = player.gameObject;
        component.baseDamage = baseDamage;
        component.powerUps = player.activePowerUps;
        component.selfDamage = false;

        rigidbody2D.AddForce(firePoint.up * force, ForceMode2D.Impulse);
    }
}
