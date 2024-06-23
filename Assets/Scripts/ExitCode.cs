using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCode : MonoBehaviour
{
    [SerializeField] Transform DoorCheck;
    
    private Animator openAnimation;

    private void Start()
    {
        openAnimation = GetComponentInChildren<Animator>();
    }

    public void OnOpen()
    {
        DoorCheck.gameObject.SetActive(true);
        openAnimation.SetTrigger("OnDoorOpen");
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
