using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
 
    public float movementSpeed;
    public GameObject ObjectToBeCloned;
    public Vector3 move;
    
 
    // Use this for initialization
    void Start () 
    {
       
    }
 
    // Update is called once per frame
    void Update () {
        
        if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey ("w")) {
            transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed * 2.5f;
        }   else if (Input.GetKey ("w") && !Input.GetKey (KeyCode.LeftShift)) {
            transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed;
        } else if (Input.GetKey ("s")) {
            transform.position -= transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed;
        }
 
        if (Input.GetKey ("a") && !Input.GetKey ("d")) {
                transform.position += transform.TransformDirection (Vector3.left) * Time.deltaTime * movementSpeed;
            } else if (Input.GetKey ("d") && !Input.GetKey ("a")) {
                transform.position -= transform.TransformDirection (Vector3.left) * Time.deltaTime * movementSpeed;
            }
        if (Input.GetKey (KeyCode.LeftControl)) /*&& Input.GetKey (""))*/ {
        transform.position += transform.TransformDirection (Vector3.down) * Time.deltaTime * movementSpeed * 0.7f;
        } 
        if (Input.GetKey (KeyCode.Space)){
        transform.position += transform.TransformDirection (Vector3.up) * Time.deltaTime * movementSpeed * 1.5f;
        }
       
    }

     void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Tree")
            {
                Instantiate(ObjectToBeCloned, collision.gameObject.transform.position + move, transform.rotation);
                //collision.gameObject.GetComponent<Collider>().enabled = false;
            } 
        }
        
}


/*else if(_move.sqrMagnitude > 0f)
    {
        //ease out
        _move *= 0.5f;
        transform.Translate(_move * Time.deltaTime);
        //once we've reached a slow enough speed, zero out
        if(_move.magnitude < 0.01f) _move = Vector3.zero;
    }*/