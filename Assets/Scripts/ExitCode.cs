using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCode : MonoBehaviour
{
    [SerializeField] Transform DoorCheck;
    


    public void OnOpen()
    {
        DoorCheck.GetComponent<BoxCollider2D>().enabled = true;
        GetComponentInChildren<Animator>().SetTrigger("OnDoorOpen");
        GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log("OnFinishedOpening");
    }
}
