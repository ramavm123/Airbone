using UnityEngine;

public class GrapplinHook : MonoBehaviour
{
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;

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

    public void EngageGrapple(Vector2 target)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            origin: (Vector2)transform.position,
            direction: target - (Vector2)transform.position,
            distance: Mathf.Infinity,
            layerMask: grappleLayer
        );

        if (hit.collider != null)
        {
            grapplePoint = hit.point;
            grapplePoint.z = 0;
            joint.connectedAnchor = grapplePoint;
            joint.enabled = true;
            joint.distance = grappleLength;
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, grapplePoint);
            rope.enabled = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableGrapple();
        }

        if (rope.enabled)
        {
            rope.SetPosition(1, transform.position);
        }
    }


    public void DisableGrapple()
    {
        joint.enabled = false;
        rope.enabled = false;
    }
}