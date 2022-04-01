using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvents : MonoBehaviour
{
    [Header("Doors")]
    public List<GameObject> doors;

    [Header ("Enemies")]
    public List<GameObject> enemies;

    [Header("Powerups")]
    public GameObject[] powerups;


    private bool spawned = false;
    private bool doorsDestroyed = false;
    void Start()
    {
        if (spawned == false)
        {
            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(false);
            }
            int CountToLeave = Random.Range(0, 3);
            foreach (GameObject enemy in enemies)
            {
                if (enemies.IndexOf(enemy) <= CountToLeave)
                {
                    if (enemy != null)
                        enemy.SetActive(false);
                    else enemies.Remove(enemy);
                }
                else
                {
                    enemies.Remove(enemy);
                    Destroy(enemy.gameObject);
                }
            }
            spawned = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !doorsDestroyed)
        {
            CloseDoors();
            StartCoroutine(CheckEnemies());
        }
        if (other.CompareTag("Door") && doorsDestroyed)
        {
            DestroyDoors();
        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyDoors();
    }
    public void DestroyDoors()
    {
        foreach(GameObject door in doors)
        {
             if(door != null)
                Destroy(door);
        }
        doorsDestroyed = true;
    }
    public void CloseDoors()
    {
        foreach (GameObject door in doors)
        {
            if (door != null) 
                door.SetActive(true);
        }

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
                 enemy.SetActive(true);
            else {
                enemies.Remove(enemy);
                Destroy(enemy.gameObject);
            }
        }
    }
}