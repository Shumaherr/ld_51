using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField]
    private float movementSpeed;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        RotateTowardDirection();
    }
    
    void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }
    
    public void Movement()
    {
        Vector2 currentPos = _rb.position;
        Vector2 adjustedMovement = _movement * movementSpeed;
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
