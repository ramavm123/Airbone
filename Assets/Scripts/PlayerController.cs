using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isInAir = false; // Variable para rastrear si el jugador está en el aire

    public UnityEvent<bool> GroundState;
    public UnityEvent Jumped;

    private Rigidbody2D rb;
    private bool isGrounded;
    private GrapplinHook grapplinHook; // Referencia al script del gancho

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        grapplinHook = GetComponent<GrapplinHook>(); // Add this line to get a reference to the GrapplinHook component
    }

    void Update()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        Vector2 moveVelocity = new Vector2(moveInputX * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

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

        //Debug.Log("Grapple state " +  grapplinHook.IsHookActivated());
    }

    void Jump()
    {
        Jumped.Invoke();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isInAir = true; // El jugador está en el aire después de saltar
        GroundState.Invoke(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isInAir = false; // El jugador ya no está en el aire después de tocar el suelo
            GroundState.Invoke(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            isInAir = true; // El jugador está en el aire después de dejar el suelo
            GroundState.Invoke(false);
        }
    }

    public void TakeDamage(int damage)
    {
        // Aquí puedes implementar la lógica para el daño al jugador, si es necesario
    }

    public void ApplyExpulsionForce(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    public void EngageGrapple(Vector2 target)
    {
        grapplinHook.EngageGrapple(target);
    }

}