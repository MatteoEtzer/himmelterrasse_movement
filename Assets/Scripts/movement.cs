using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movement : MonoBehaviour {
 
    public float movementSpeed;
    public GameObject ObjectToBeCloned;
    public Vector3 move;
    public int minZ;
    public int maxZ;  

    public Transform CloudTarget;
    public Transform TreeTarget;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    public bool SearchButtonClicked = false;
    public bool ReturnButtonClicked = false;

    public string[] Namensliste;
    
    
    void Start () 
    {

    }
 
    void Update () {
        
        if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey ("w")) {
            transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * movementSpeed * 5f;
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

        if (SearchButtonClicked == true) 
        {
        StartCoroutine("SearchAnimation");
        SearchButtonClicked = false;
        }
        if (ReturnButtonClicked == true)
        {
        StartCoroutine("ReturnAnimation");
        ReturnButtonClicked = false;
        }
    }

    void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Tree")
            {
                GameObject tree = Instantiate(ObjectToBeCloned, collision.gameObject.transform.position + move, transform.rotation);
                //collision.gameObject.GetComponent<Collider>().enabled = false;
                GameObject text = tree.transform.Find("Profile_Text").gameObject;
                Vector3 posText = text.transform.position;
                Vector3 newPosText = new Vector3(posText.x, posText.y + Random.Range(minZ,maxZ), posText.z);
                text.transform.position = newPosText;
            } 
        }
    public void Search()
    {
        TreeTarget.position = this.transform.position;
        SearchButtonClicked = true;
        Debug.Log("Search-Button clicked");
    }   
    IEnumerator SearchAnimation()
    {
        for (float ft = 10f; ft >= 0; ft -= 0.1f)
        {
            Vector3 targetPosition = CloudTarget.TransformPoint(new Vector3(0, 0, 0));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime); 
            yield return null;
        }
    }
    IEnumerator ReturnAnimation()
    {
        for (float ft = 10f; ft >= 0; ft -= 0.1f)
        {
            Vector3 targetPosition = TreeTarget.TransformPoint(new Vector3(0, 0, 0));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            yield return null;
        }
    }
    public void ReturnToTree()
    {
        ReturnButtonClicked = true;
        Debug.Log("Return-Button clicked");
    }    
}
