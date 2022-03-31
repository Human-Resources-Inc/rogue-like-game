using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;

    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right,

    }

    private RoomVariants variants;
    private int rand = 0;
    public bool spawned;
    private float waitTime = 3f;
    private int roomsCount;

    public void Start()
    {
        variants = GameObject.FindGameObjectWithTag("RoomTypes").GetComponent<RoomVariants>();
        roomsCount = GameObject.FindGameObjectsWithTag("Rooms").Length;
        if (roomsCount < 10)
        {
            Invoke("Spawn", 0.2f);
        }
    }

    public void Spawn()
    {

        if (!spawned)
        {
            Instantiate(variants.rooms[0], transform.position, variants.rooms[0].transform.rotation);
        }
        spawned = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RoomPoint"))
        {
            if (gameObject.GetComponent<RoomSpawner>().spawned)
            {
                Destroy(other);
            }
            else if (other.GetComponent<RoomSpawner>().spawned)
            {
                Destroy(gameObject);
            }
            else if (gameObject.activeSelf)
            {
                other.gameObject.SetActive(false);
                Destroy(other);
            }
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("RoomPoint") && gameObject.activeSelf)
        {
            other.gameObject.SetActive(false);
            Destroy(other);
        }
    }
}
