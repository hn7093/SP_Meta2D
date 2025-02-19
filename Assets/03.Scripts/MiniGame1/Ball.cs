using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    public float speedX = 10;
    public float speedY = 10;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(BlockGameManager.Instance.gameState != GameState.Playing)
        {
            _rigidBody.velocity = Vector2.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 시작시 대각선으로
        Vector2 force = new Vector2(speedX, speedY);
        _rigidBody.velocity = force;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 게임 오버
        BlockGameManager.Instance.GameStop();
    }
}
