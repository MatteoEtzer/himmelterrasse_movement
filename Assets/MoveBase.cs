using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBase : MonoBehaviour
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;

    public SpawnBases sBase;    //Prefab Base, die kopiert wird
    public SwipeInput Touch;

    public Vector3 pos;         //Position von Prefab Base in Update
    public float z;             //z Position von pos

    private void Start()
    {
        // Referenz auf unser SpawnBase Script
        sBase = GameObject.Find("SpawnManager").GetComponent<SpawnBases>();
        Touch = GameObject.Find("SpawnManager").GetComponent<SwipeInput>();
    }

    void Update()
    {
        pos = this.transform.position;
        z = pos.z;

        // Bewegung nach vorne
        if (Input.GetKey(moveForwardKey) || SwipeInput.swipedUp == true)
        {
            this.transform.position = new Vector3(pos.x,pos.y,pos.z + (Time.deltaTime));
        }

        // Bewegung nach hinten
        if (Input.GetKey(moveBackwardKey) || SwipeInput.swipedDown == true)
        {
            this.transform.position = new Vector3(pos.x, pos.y, pos.z - Time.deltaTime);
        }


        if (z > sBase.backVec.z)
        {
            sBase.SpawnAtStart();
            Destroy(this.gameObject);
            
        }

        if (z < sBase.startVec.z)
        {
            sBase.SpawnAtBack();
            Destroy(this.gameObject);
        }
    }
}
