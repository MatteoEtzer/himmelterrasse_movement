using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlong : MonoBehaviour
{
    public Transform camera;

    void Start()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, camera.position.z);
    }

    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, camera.position.z);
    }
}
