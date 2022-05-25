using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    public Seeker seeker;
    public Rigidbody2D rb;
    public float movementSpeed;

    [SerializeField] private Transform target;
    [SerializeField] private EnemyBase enemy;
    [SerializeField] private Rigidbody2D firePoint;
    public Path path;
    private float nextWaypointDistance = 1f;
    private int currentWaypoint;
    private bool reachedEndOfPath = false;


    public void Roam()
    {
        Debug.Log("Roaming...");
        
        var direction = UtilsClass.GetRandomDirection() * 5;
        seeker.StartPath(rb.position, direction, OnPathComplete);
    }

    public void ChasePlayer()
    {
        Debug.Log("Building path to the player");
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
        else
        {
            Debug.LogError(p.errorLog);
        }
    }


    private void Start()
    {
        target = FindObjectOfType<Player>().transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        
        InvokeRepeating("Roam", 0, Random.Range(10, 20));
    }


    private void Update()
    {
        if (UtilsClass.DetectPlayerInRange(transform.position, enemy.seeRange) && !enemy.isChasing)
        {
            enemy.isChasing = true;
            CancelInvoke("Roam");
            Debug.Log("Cancelling Roam and Chasing the player!");
            InvokeRepeating("ChasePlayer", 0, .5f);
        }
    }


    void FixedUpdate()
    {
        UtilsClass.GetFireAngle(target.position, rb, firePoint);

        if (path == null || enemy.inAttackRange)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = dir * movementSpeed * Time.deltaTime;

        rb.AddForce(force);
        

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
