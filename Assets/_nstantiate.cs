using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] listGameObjects;
    public int numberCopies = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberCopies; i++)
        {
            Destroy(listGameObjects[i]);
        }

        listGameObjects = new GameObject[numberCopies];

        for (int i = 0; i < numberCopies; i++)
        {
           GameObject clone = Instantiate(prefab);
            listGameObjects[i] = clone;
        }

        
    }



    void DestroyObjects()
    {
        for (int i = 0; i < numberCopies; i++)
        {
            Destroy(listGameObjects[i]);
        }
    }
}
