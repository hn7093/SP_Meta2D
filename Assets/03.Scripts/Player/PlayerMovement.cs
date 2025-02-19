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

    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] SpriteRenderer vehicleSprite;

    void Awake()
    {
        camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputs = GetComponent<PlayerInputs>();
    }
    // Start is called before the first frame update
    void Start()
    {
        int idx = PlayerPrefs.GetInt("playerSprite", 0);
        ChangeSprite(idx);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ChangeSprite(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSprite(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSprite(2);
        }
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
        if (_inputs.movementDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if(_inputs.movementDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        // sprint
        float sprintSpeed = _inputs.isSprint ? spintSpeed : moveSpeed;
        if (_inputs.isSprint)
        {
            playerSprite.enabled = false;
            vehicleSprite.enabled = true;
        }
        else
        {
            playerSprite.enabled = true;
            vehicleSprite.enabled = false;
        }
        movementDirection = _inputs.movementDirection.normalized * sprintSpeed;
        _rigidbody.velocity = movementDirection;
    }

    // 플레이어 스프라이트 변경
    public void ChangeSprite(int idx)
    {
        Sprite playerImage = Resources.Load<Sprite>($"player{idx}");
        if (playerImage != null)
        {
            playerSprite.sprite = playerImage;
            PlayerPrefs.SetInt("playerSprite", idx);
        }
    }
}
