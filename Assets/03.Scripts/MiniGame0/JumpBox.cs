using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpBox : MonoBehaviour
{
    public float flapForce = 6f;
    public float forwardSpeed = 6f;
    public bool isDead = false;
    float deathCooldown = 0f;
    bool isFlap = false;
    int score;

    // component
    Animator _animator;
    Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_animator == null)
            Debug.LogError("Not Founded Animator");
        if (_rigidbody == null)
            Debug.LogError("Not Founded Rigidbody2D");
    }

    void FixedUpdate()
    {
        // 실행 중일 떄만
        if(!(GameManager.Instance.gameState == GameState.Playing)) 
        {
            _rigidbody.gravityScale = 0f;
            return;
        }

        if (isDead) return;
        _rigidbody.gravityScale = 1.5f;
        Vector3 velocity = _rigidbody.velocity;
        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void Update()
    {
        // 실행 중일 떄만
        if(!(GameManager.Instance.gameState == GameState.Playing)) return;

        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                //게임 재시작
                GameManager.Instance.RestartGame();
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                // UI 클릭은 무시
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                isFlap = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        // Game Over
        isDead = true;
        deathCooldown = 1f;
        _animator.SetBool("IsDie", true);
        GameManager.Instance.GameStop();
    }

    public void Reset()
    {
        deathCooldown = 0f;
        isDead = false;
        _animator.SetBool("IsDie", false);
        transform.position = Vector3.zero;
    }
}
