using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera cam;
    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    //ѕеремещает игрока и камеру на заданные величины
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.position += playerChangePos;
        cam.transform.position += cameraChangePos;

    }

}