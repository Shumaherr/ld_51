using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField]
    private float movementSpeed;
    public float SpeedMultiplier { get; set; } = 1f;
    public int ControlInverter { get; set; } = 1;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    public Light2D Light { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Light = GetComponentInChildren<Light2D>();
        Light.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        RotateTowardDirection();
    }
    
    void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>() * ControlInverter;
    }
    
    public void Movement()
    {
        Vector2 currentPos = _rb.position;
        Vector2 adjustedMovement = _movement * (movementSpeed * SpeedMultiplier);
        Vector2 newPos = currentPos + adjustedMovement * Time.fixedDeltaTime;
        _rb.MovePosition(newPos);
    }
    
    public void RotateTowardDirection()
    {
        if(_movement != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back,_movement);
        }
    }
    
}
