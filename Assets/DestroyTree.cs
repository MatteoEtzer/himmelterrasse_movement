using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTree : MonoBehaviour
{
    public float time;

    private void OnTriggerExit(Collider other)
    {
        Destroy(this.gameObject, time);
        if (other.gameObject.tag == "MainCamera")
        {
            
        }
    }
}
