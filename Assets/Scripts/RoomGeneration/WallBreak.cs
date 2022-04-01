using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreak : MonoBehaviour
{
    Vector3 v1, v2;
    public GameObject block;
    public bool isTurned = false;
    private RoomEvents roomEvents;
    
    void Start()
    {
        roomEvents = GetComponentInParent<RoomEvents>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            gameObject.SetActive(false);
            if (isTurned)
            {
                v1 = new Vector3(0.5f, 0f, 0f);
                v2 = new Vector3(-0.5f, 0f, 0f);
            }
            else
            {
                v1 = new Vector3(0f, 0.5f, 0f);
                v2 = new Vector3(0f, -0.5f, 0f);
            }
            Instantiate(block, gameObject.transform.position + v1, Quaternion.identity);
            Instantiate(block, gameObject.transform.position + v2, Quaternion.identity);
            roomEvents.doors.Remove(gameObject);
            Destroy(gameObject);
        }

    }

}
