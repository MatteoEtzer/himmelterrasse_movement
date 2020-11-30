using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizeCubes : MonoBehaviour
{

    public GameObject CubePrefab;

    public Vector3 center;
    public Vector3 size;
    public int count;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < count; i++)
            {   
                spawnCube();
            }
        }
    }

    
    public void spawnCube()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x /2), Random.Range(-size.y / 2, size.y /2), Random.Range(-size.z / 2, size.z /2));

        Instantiate(CubePrefab, pos, Quaternion.identity); 
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawCube(center, size);
    }
}