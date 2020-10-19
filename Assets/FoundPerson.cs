using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoundPerson : MonoBehaviour
{
    public string personId;

    public DbCommunication dbCommunication;

    // Start is called before the first frame update
    void Start()
    {
        dbCommunication = GameObject.FindGameObjectWithTag("dbHandler").GetComponent<DbCommunication>();
    }

    // Update is called once per frame
    public void SelectButton()
    {
        dbCommunication.LoadPerson(personId);
    }
}
