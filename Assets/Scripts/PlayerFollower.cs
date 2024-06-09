using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private float SpeedMultiplier;
    [SerializeField] private float playerVelocityOffset;
    [SerializeField] private Vector2 CameraOffset;
    
    private Rigidbody2D PlayerRB2D;

    // Start is called before the first frame update
    public void SetCamera(GameObject _player)
    {
        Player = _player.transform;
        PlayerRB2D = Player.GetComponent<Rigidbody2D>();
        transform.position = Player.transform.position + (Vector3)CameraOffset;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 Objective = (Vector2)Player.transform.position + PlayerRB2D.velocity * playerVelocityOffset + CameraOffset;
        transform.position = Vector3.Lerp(transform.position, Objective, SpeedMultiplier);
        //transform.position = new Vector3(0, Mathf.Lerp(transform.position.y + CameraOffset.y, Player.transform.position.y, Time.deltaTime * SpeedMultiplier));

    }
    public void configurateSettings(float _newSpeedMultiplier, float _newplayerVelocityOffset, Vector2 _newCameraOffset)
    {
        SpeedMultiplier = _newSpeedMultiplier;
        playerVelocityOffset = _newplayerVelocityOffset;    
        CameraOffset = _newCameraOffset;
    }
}
