using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public bool isInAir = false; // Variable para rastrear si el jugador est� en el aire

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
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInputX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
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
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isInAir = true; // El jugador est� en el aire despu�s de saltar
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isInAir = false; // El jugador ya no est� en el aire despu�s de tocar el suelo
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            isInAir = true; // El jugador est� en el aire despu�s de dejar el suelo
        }
    }

    public void TakeDamage(int damage)
    {
        // Aqu� puedes implementar la l�gica para el da�o al jugador, si es necesario
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