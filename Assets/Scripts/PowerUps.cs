using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public string modifierName;
    public int damageModifier;
    public float speedModifier;
    public int healthModifier;
    public SpriteRenderer spriteRenderer;
    public Collider2D collider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player != null)
        {
            player.activePowerUps.Add(gameObject.GetComponent<PowerUps>());
            player.movementSpeed += speedModifier;
            player.maxHealth += healthModifier;
            player.currentHealth += healthModifier;
            player.baseDamage += damageModifier;
            player.healthBar.UpdateHealth();
            spriteRenderer.sprite = null;
            collider.enabled = false;
        }
    }
}
