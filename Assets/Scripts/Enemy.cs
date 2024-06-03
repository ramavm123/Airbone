using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidadMovimiento = 5f; // Esta variable ya no se usará.
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
        // Movimiento eliminado: el enemigo permanece estático
        rb.velocity = Vector2.zero;  // Asegurar que el enemigo no se mueva

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            // Calculate Angle Between the collision point and the player
            Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
            Vector3 dir = enemyPosition - collision.contacts[0].point;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            playerController.GetComponent<Rigidbody2D>().AddForce(dir * 2500);
            Destroy(gameObject);
            //PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            //if (playerController != null && !playerController.isInAir)
            //{
            //    playerController.TakeDamage(danoAlJugador);
            //    // Aplicar fuerza de expulsión al jugador
            //    Vector2 direccionExpulsion = (collision.transform.position - transform.position).normalized;
            //    playerController.ApplyExpulsionForce(direccionExpulsion, fuerzaExpulsion);
            //}
            //else
            //{

            //}
        }
    }

    // void CambiarDireccion()
    // {
    //     transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    //     velocidadMovimiento *= -1; // Invertir la dirección de movimiento
    // }

    public void EngancharJugador()
    {
        jugadorEnganchado = true;
    }

    public void DesengancharJugador()
    {
        jugadorEnganchado = false;
    }
}
