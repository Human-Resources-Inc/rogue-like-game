using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevKit : MonoBehaviour
{
    public List<GameObject> mobPrefabs;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Instantiate(mobPrefabs[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Instantiate(mobPrefabs[1]);
    }
}
