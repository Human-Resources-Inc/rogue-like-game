using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Pathfinding;

public class Enemy : MonoBehaviour, IDamageable
{
    public Transform target;
    public int maxHealth;
    public float speed = 200f;
    public float nextWaypointDistance = 1f;
    public float seeRange; // seeRange устанавливает радиус в котором моб видит игрока
    public float repathTimeChasing = .5f;
    public float repathTimeRoaming = 10f;
    public Rigidbody2D firePoint;
    public bool isChasing = false;

    private int currentHealth;
    private Vector2 startingPosition;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    
    private float lastRepath = float.NegativeInfinity;

    [SerializeField]
    private Seeker seeker;
    [SerializeField]
    private Rigidbody2D rigidbody2D;
    [SerializeField]
    private EnemyAttack enemyAttack;


    void Start()
    {
        currentHealth = maxHealth;
        startingPosition = rigidbody2D.position;
        target = FindObjectOfType<Player>().transform;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private Vector3 GetRoamingPosition(Vector3 startingPosition)
    {
        return startingPosition + UtilsClass.GetRandomDirection() * Random.Range(1f, 7f);
    }

    private void Update()
    {
        // Генерируем путь для моба: рандомно, если игрок еще не замечен или к игроку
        // если isChasing = true
        if (isChasing && Time.time > lastRepath + repathTimeChasing && seeker.IsDone() ||
            !isChasing && Time.time > lastRepath + repathTimeRoaming && seeker.IsDone())
        {
            lastRepath = Time.time;
            seeker.StartPath(transform.position, isChasing ? target.position : GetRoamingPosition(startingPosition), OnPathComplete);
        }

        if (UtilsClass.DetectPlayerInRange(transform.position, enemyAttack.meleeRange) && isChasing)
        {
            // Останавливаем моба, если игрок в радиусе атаки
            path = null;
        }
        else if (UtilsClass.DetectPlayerInRange(transform.position, enemyAttack.rangedRange) && isChasing &&
            UtilsClass.CanSeePlayer(transform.position, target.position))
        {
            path = null;
        }

        if (!isChasing)
        {
            if (UtilsClass.DetectPlayerInRange(transform.position, seeRange))
            {
                //Debug.Log($"CanSeePlayer: {UtilsClass.CanSeePlayer(transform.position, target.position)}");
                if (UtilsClass.CanSeePlayer(transform.position, target.position))
                {
                    isChasing = true;
                    Debug.Log("Chasing player");
                }
            }
        }

        Vector2 lookDirection = (Vector2)target.position - rigidbody2D.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        firePoint.rotation = angle;
        firePoint.transform.position = rigidbody2D.transform.position;
    }

    private void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } 
        else
        {
            reachedEndOfPath = false;
        }

        // Двигаем персонажа путём нахождения вектора направления и далее
        // высчитываем "силу" с которой мы "толкаем" персонажа по направлению к цели
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2D.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        rigidbody2D.AddForce(force);

        // Находим, дошли мы до определённой точки пути или нет
        float distance = Vector2.Distance(rigidbody2D.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            seeker.pathCallback -= OnPathComplete;
            return;
        }

        isChasing = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, seeRange);
    }
#endif
}
