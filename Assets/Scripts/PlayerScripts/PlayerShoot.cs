using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float force = 20f;

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

        Rigidbody2D rigidbody2D = projectile.GetComponent<Rigidbody2D>();
        Projectile component = projectile.GetComponent<Projectile>();

        component.owner = player.gameObject;
        component.baseDamage = player.baseDamage;
        component.powerUps = player.activePowerUps;
        component.selfDamage = false;

        rigidbody2D.AddForce(firePoint.up * force, ForceMode2D.Impulse);
    }
}
