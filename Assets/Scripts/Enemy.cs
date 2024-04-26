using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float fuerzaGrappling = 10f;
    public float fuerzaExpulsion = 5f;
    public int danoAlJugador = 10;
    public float distanciaSuelo = 0.2f;
    public float distanciaObstaculo = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool jugadorEnganchado = false;
    private GameObject jugador;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Comprobar si hay suelo
        RaycastHit2D sueloHit = Physics2D.Raycast(transform.position - Vector3.right * 0.3f, Vector2.down, distanciaSuelo, groundLayer);
        bool haySuelo = sueloHit.collider != null;

        // Comprobar si hay obstáculo
        RaycastHit2D obstaculoHit = Physics2D.Raycast(transform.position + Vector3.up * 0.2f, transform.right * Mathf.Sign(rb.velocity.x), distanciaObstaculo);
        bool hayObstaculo = obstaculoHit.collider != null && !obstaculoHit.collider.CompareTag("Player");

        // Cambiar dirección si no hay suelo o hay un obstáculo
        if (!haySuelo || hayObstaculo)
        {
            CambiarDireccion();
        }

        // Movimiento
        if (!jugadorEnganchado)
        {
            rb.velocity = new Vector2(-velocidadMovimiento, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null && !playerController.isInAir)
            {
                playerController.TakeDamage(danoAlJugador);
                // Aplicar fuerza de expulsión al jugador
                Vector2 direccionExpulsion = (collision.transform.position - transform.position).normalized;
                playerController.ApplyExpulsionForce(direccionExpulsion, fuerzaExpulsion);
            }
            else
            {
                // El jugador está enganchado, por lo tanto, el enemigo muere
                Destroy(gameObject);
            }
        }
    }

    void CambiarDireccion()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        velocidadMovimiento *= -1; // Invertir la dirección de movimiento
    }

    public void EngancharJugador()
    {
        jugadorEnganchado = true;
    }

    public void DesengancharJugador()
    {
        jugadorEnganchado = false;
    }
}
