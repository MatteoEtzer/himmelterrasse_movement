using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizeCubes : MonoBehaviour
{

    public GameObject CubePrefab;

    public Vector3 center;
    public Vector3 size;
    public int count;
    public GameObject[] listGameObjects;

    void Start()
    {
        SpawnCube();
    }

    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Q))
        {
            DestroyObjects();
            spawnCube();
        }
        */
    }

    
    public void SpawnCube()
    {
        listGameObjects = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x /2), Random.Range(-size.y / 2, size.y /2), Random.Range(-size.z / 2, size.z /2));

            GameObject clone = Instantiate(CubePrefab, pos, Quaternion.identity);
            
            listGameObjects[i] = clone;
        }
    }

    void DestroyObjects()
    {
        for (int i = 0; i < count; i++)
        {
            Destroy(listGameObjects[i]);
        }
    }

    public void DoAll()
    {
        DestroyObjects();
        SpawnCube();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(center, size);
    }
}