using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLogic : MonoBehaviour
{
    Animator checkAnimation;

    private void Start()
    {
        checkAnimation = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            GameObject.FindGameObjectWithTag("GM").GetComponent<Gamemanager>().CheckpointSet(GetComponent<AutoSetIndex>().index);
            checkAnimation.SetTrigger("SetCheckpoint");
        }
       
    }

    public void OnSetup()
    {
        checkAnimation = GetComponentInChildren<Animator>();
        checkAnimation.SetTrigger("SetCheckpoint");
        Debug.Log("Onsetup");
    }

    public void OnReset()
    {
        checkAnimation.SetTrigger("UnSetCheckpoint");
        Debug.Log("OnReset");
    }
}
