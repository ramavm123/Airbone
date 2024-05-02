using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject parent;
    PlayerController parentController;
    Rigidbody2D RB2D;
    void Start()
    {
        parent = transform.parent.gameObject;
        parentController = parent.GetComponent<PlayerController>();
        RB2D = parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
