using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class PlayerInputs : MonoBehaviour
{
    public Vector2 movementDirection = Vector2.zero;
    public Vector2 lookDirection = Vector2.zero;
    public bool isAttacking = false;
    public bool isJumping = false;
    public bool isSprint = false;
    Camera camera;
    void Start()
    {
        camera = Camera.main;
    }
    void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

    void OnLook(InputValue inputValue)
    {
        Vector2 mousePosition = inputValue.Get<Vector2>();
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    void OnAttack(InputValue inputValue)
    {
        // UI 클릭은 무시
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        isAttacking = inputValue.isPressed;
        
    }
    void OnSprint(InputValue inputValue)
    {
        isSprint = inputValue.isPressed;
    }
    void OnJump(InputValue inputValue)
    {
        isJumping = inputValue.isPressed;
    }
}
