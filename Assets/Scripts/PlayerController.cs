using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxSpeed = 10f;
    public float jumpForce = 10f;
    public bool isInAir = false;

    public UnityEvent<bool> GroundState;
    public UnityEvent Jumped;

    private Rigidbody2D rb;
    private bool isGrounded;
    private GrapplinHook grapplinHook;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Evita que el personaje rote debido a físicas
        grapplinHook = GetComponent<GrapplinHook>();
    }

    void Update()
    {
        float moveInputX = Input.GetAxis("Horizontal");

        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            Vector2 moveForce = new Vector2(moveInputX * moveSpeed, 0);
            rb.AddForce(moveForce);
        }

        if (moveInputX < 0)
        {
            if (grapplinHook.IsHookActivated())
            {
                grapplinHook.SetPlayerInMovement(true);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else if (moveInputX > 0)
        {
            if (grapplinHook.IsHookActivated())
            {
                grapplinHook.SetPlayerInMovement(true);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            grapplinHook.EngageGrapple(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (Input.GetMouseButtonUp(0))
        {
            grapplinHook.DisableGrapple();
        }
    }

    void Jump()
    {
        Jumped.Invoke();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isInAir = true;
        GroundState.Invoke(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isInAir = false;
            GroundState.Invoke(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            isInAir = true;
            GroundState.Invoke(false);
        }
    }



    public void EngageGrapple(Vector2 target)
    {
        grapplinHook.EngageGrapple(target);
    }
}
