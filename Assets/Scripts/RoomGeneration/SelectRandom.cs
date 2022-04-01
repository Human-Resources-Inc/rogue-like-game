using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRandom : MonoBehaviour
{
    public int LowCountToLeave = 1;
    public int HighCountToLeave = 2;
    [Range(0, 1)]
    private int CountToLeave;
    void Start()
    {
        CountToLeave = HighCountToLeave - LowCountToLeave;
        if (Random.value > 0.5f) CountToLeave += 1;
        while (transform.childCount > CountToLeave)
        {
            Transform childToDestroy = transform.GetChild(Random.Range(0, transform.childCount));
            DestroyImmediate(childToDestroy.gameObject);
        }
    }

}
