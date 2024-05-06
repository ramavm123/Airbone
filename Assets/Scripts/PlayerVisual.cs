using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject parent;
    Rigidbody2D RB2D;
    [SerializeField]
    private Animator animator;
    bool JumpState = false;

    void Start()
    {
        parent = transform.parent.gameObject;
        PlayerController parentController = parent.GetComponent<PlayerController>();
        RB2D = parent.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        parentController.GroundState.AddListener(groundedEvent);
        parent.GetComponent<GrapplinHook>().GrappleState.AddListener(grappleEvent);
        parentController.Jumped.AddListener(jumpTrigger);

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("WalkingSpeed", RB2D.velocity.x);
    }

    void groundedEvent(bool groundedState)
    {
        animator.SetBool("IsGrounded", groundedState);
    }
    void grappleEvent(bool grappleState)
    {
        animator.SetBool("IsGrapling", grappleState);
    }
    void jumpTrigger()
    {
        animator.SetTrigger("StartJump");
    }
}
