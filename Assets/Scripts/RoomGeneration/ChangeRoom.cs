using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class ChangeRoom : MonoBehaviour
{
    [SerializeField] private Vector3 cameraChangePos;
    [SerializeField] private Vector3 playerChangePos;
    [SerializeField] private GameObject cameraBounds;
    [SerializeField] private AstarPath navmesh;


    void Start()
    {
        cameraBounds = FindObjectOfType<PolygonCollider2D>().gameObject;
        navmesh = FindObjectOfType<AstarPath>();
    }

    //ѕеремещает игрока и камеру на заданные величины
    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Player")) {
            other.transform.position += playerChangePos;
            cameraBounds.transform.position += cameraChangePos;

            var grid = AstarPath.active.data.gridGraph;
            grid.center += cameraChangePos;
            navmesh.Scan();
            }

    }

}