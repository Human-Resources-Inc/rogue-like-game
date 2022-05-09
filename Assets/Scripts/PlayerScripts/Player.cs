using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int currentHealth;
    public float movementSpeed = 5f;
    public Rigidbody2D rigidbody2D;
    public List<PowerUps> activePowerUps;
    public Camera cam;

    private Vector2 playerPosition;
    private Vector2 mousePosition;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Texture2D playerCursor;
    [SerializeField]
    private Rigidbody2D firePoint;

    
    private void Start()
    {
        // Устанавливаем здоровье игрока и курсор
        currentHealth = maxHealth;
        Cursor.SetCursor(playerCursor, new Vector2(10, 10), CursorMode.Auto);
    }

    private void Update()
    {
        playerPosition.x = Input.GetAxisRaw("Horizontal");
        playerPosition.y = Input.GetAxisRaw("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        // Обновляем позицию игрока
        rigidbody2D.MovePosition(rigidbody2D.position + playerPosition * movementSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - rigidbody2D.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        firePoint.rotation = angle;
        firePoint.transform.position = rigidbody2D.transform.position;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            SceneManager.LoadScene("SampleScene");
    }
}
