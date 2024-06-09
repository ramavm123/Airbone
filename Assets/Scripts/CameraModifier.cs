using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModifier : MonoBehaviour
{
    private PlayerFollower CurrentPlayerFollower;
    [SerializeField] private Transform Player;
    [SerializeField] private float SpeedMultiplier;
    [SerializeField] private float playerVelocityOffset;
    [SerializeField] private Vector2 CameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayerFollower = GameObject.FindGameObjectWithTag("CameraPoint").GetComponent<PlayerFollower>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //configurateSettings
    }
}
