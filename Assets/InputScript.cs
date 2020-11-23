using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour
{
    public Transform TreeTarget;
    public Transform CloudTarget;
    public GameObject SearchButton;
    public GameObject ReturnButton;
    public GameObject SearchBar;
    public GameObject OSK;
    public bool SearchButtonClicked = false;
    public bool ReturnButtonClicked = false;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    // public KeyCode moveForwardKey;
    // public KeyCode moveBackwardKey;
    // public KeyCode moveRightKey;
    // public KeyCode moveLeftKey;

    //public SwipeInput Touch;

    public Vector3 pos;      

    void Start()
    {
        SearchBar.SetActive(false);
        ReturnButton.SetActive(false);
        OSK.SetActive(false);
    }

    void Update()
    {
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
        // pos = this.transform.position;


        // if (Input.GetKey(moveForwardKey)) // || SwipeInput.swipedUp == true)
        // {
        //     this.transform.position = new Vector3(pos.x,pos.y,pos.z + (Time.deltaTime));
        // }
        
        // if (Input.GetKey(moveBackwardKey)) // || SwipeInput.swipedUp == true)
        // {
        //     this.transform.position = new Vector3(pos.x,pos.y,pos.z - (Time.deltaTime));
        // }

        // if (Input.GetKey(moveLeftKey)) // || SwipeInput.swipedUp == true)
        // {
        //     this.transform.position = new Vector3(pos.x - (Time.deltaTime), pos.y, pos.z);
        // }

        // if (Input.GetKey(moveRightKey)) // || SwipeInput.swipedUp == true)
        // {
        //     this.transform.position = new Vector3(pos.x + (Time.deltaTime), pos.y, pos.z);
        // }

    }

    public void Search()
    {
        TreeTarget.position = this.transform.position;
        SearchButtonClicked = true;
        SearchButton.SetActive(false);
        ReturnButton.SetActive(true);
        SearchBar.SetActive(true);
        OSK.SetActive(true);
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
        ReturnButton.SetActive(false);
        SearchButton.SetActive(true);
        SearchBar.SetActive(false);
        OSK.SetActive(false);
    }

    
}
