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
    public const float DefaultSpeedMultiplier = 1f;
    public int ControlInverter { get; set; } = 1;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _anim;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    public Light2D Light { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Light = GetComponentInChildren<Light2D>();
        _anim = GetComponentInChildren<Animator>();
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
        if (_movement == Vector2.zero)
        {
            _anim.SetBool(IsMoving, false);
            return;
        }
        _anim.SetBool(IsMoving, true);
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
