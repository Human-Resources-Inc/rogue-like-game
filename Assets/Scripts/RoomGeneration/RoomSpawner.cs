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
        None
    }

    private RoomVariants variants;
    private int rand = 1;
    public bool spawned;
    private float waitTime = 0.1f;
    private int roomsCount;

    public void Start()
    {
        variants = GameObject.FindGameObjectWithTag("RoomTypes").GetComponent<RoomVariants>();
        roomsCount = GameObject.FindGameObjectsWithTag("Rooms").Length;
        Invoke("Spawn", waitTime);
    }

    public void Spawn()
    {

        if (!spawned)
        {
            if (direction == Direction.Top)
            {
                rand = Random.Range(0, variants.topRooms.Length);
                Instantiate(variants.topRooms[rand], transform.position, variants.topRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, variants.rightRooms.Length);
                Instantiate(variants.rightRooms[rand], transform.position, variants.rightRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Bottom)
            {
                rand = Random.Range(0, variants.bottomRooms.Length);
                Instantiate(variants.bottomRooms[rand], transform.position, variants.bottomRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, variants.leftRooms.Length);
                Instantiate(variants.leftRooms[rand], transform.position, variants.leftRooms[rand].transform.rotation);
            }
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
}