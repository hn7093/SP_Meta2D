using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 movementDirection = Vector2.zero;
    Vector2 lookDirection = Vector2.zero;
    // movent
    private float moveSpeed = 3.0f;
    private float spintSpeed = 6.0f;


    // jump
    private float jumpDelay = 1.0f;
    private float jumpForce = 2.0f;
    private float jumpTimer;
    private float verticalVelocity;
    private bool onJump;

    private bool canJump = true;

    // components;
    Camera camera;
    Rigidbody2D _rigidbody;
    PlayerInputs _inputs;

    [SerializeField] GameObject mouseDebug;

    void Awake()
    {
        camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputs = GetComponent<PlayerInputs>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        // look
        lookDirection = _inputs.lookDirection;

        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }

        // move
        float sprintSpeed = _inputs.isSprint ? spintSpeed : moveSpeed;
        movementDirection = _inputs.movementDirection.normalized * sprintSpeed;
        _rigidbody.velocity = movementDirection;
    }
}
