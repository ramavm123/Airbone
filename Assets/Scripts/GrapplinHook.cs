using UnityEngine;
using UnityEngine.Events;

public class GrapplinHook : MonoBehaviour
{

    public UnityEvent<bool> GrappleState;

    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private Transform grappleStart;
    //visual de la cabeza del grappling hook
    [SerializeField] private GameObject grappleHead;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    private GameObject player;

    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
        player = GameObject.FindWithTag("Player");
    }

    //si la cuerda está activa, se actualiza la posicion iniciál cada frame
    private void Update()
    {
        if (rope.enabled)
        {
            rope.SetPosition(0, grappleStart.position);
        }
    }

    // dibuja una linea hacia el mouse que se utilizará en el raycast
    //para debugging
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(grappleStart.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - grappleStart.position);
    }

    public void EngageGrapple(Vector2 target)
    {
        
        RaycastHit2D hit = Physics2D.Raycast(
            origin: (Vector2)grappleStart.position,
            direction: (target - (Vector2)transform.position).normalized,
            distance: Mathf.Infinity,
            layerMask: grappleLayer
        );


        print(target - (Vector2)transform.position);

        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            grapplePoint.z = 0;
            joint.connectedAnchor = grapplePoint;
            joint.enabled = true;
            joint.distance = grappleLength;
            rope.SetPosition(0, grappleStart.position);
            rope.SetPosition(1, grapplePoint);
            rope.enabled = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableGrapple();
        }
        GrappleState.Invoke(true);
    }


    public void DisableGrapple()
    {
        joint.enabled = false;
        rope.enabled = false;
        GrappleState.Invoke(false);
    }
}