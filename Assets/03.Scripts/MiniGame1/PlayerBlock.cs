using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    public float playerSpeed = 10;

    private Rigidbody2D _rigidBody;
    private PlayerInputs _playerInputs;
    

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerInputs = GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 force = Vector2.zero;

        // left
        if (_playerInputs.movementDirection.x < 0)
        {
            force = new Vector2(playerSpeed * -1, 0);
        }

        // right
        if (_playerInputs.movementDirection.x > 0)
        {
            force = new Vector2(playerSpeed, 0);
        }

        _rigidBody.MovePosition(_rigidBody.position + force * Time.fixedDeltaTime);
    }
}
