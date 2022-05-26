using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
  public Image healthBarImage;
  public Player player;
  public void UpdateHealth() {
      healthBarImage.fillAmount = (float)player.currentHealth/(float)player.maxHealth;
  }
}
