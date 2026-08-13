using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool leftHeld;
    private bool rightHeld;

    private int horizontalDirection;

    [Header("참조")] 
    [SerializeField] private Rigidbody2D rb;
    
    
    [SerializeField] private bool controllable;
    [SerializeField] private float moveSpeed;
    private void FixedUpdate()
    {
        // 상태를 확정짓는 작업 한번 하고 들어가야할 수 있음
        HandleInput();
    }

    private void HandleInput()
    {
        if (!controllable) return;

        rb.linearVelocityX = moveSpeed * horizontalDirection;
        SpriteFlipToLookDirection();
    }

    private void SpriteFlipToLookDirection()
    {
        if(horizontalDirection > 0) transform.localScale = new Vector3(1, 1, 1);
        if(horizontalDirection < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    public void OnLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            leftHeld = true;
            horizontalDirection = -1;
        }
        else if (ctx.canceled)
        {
            leftHeld = false;

            if (rightHeld)
                horizontalDirection = 1;
            else
                horizontalDirection = 0;
        }
    }

    public void OnRight(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            rightHeld = true;
            horizontalDirection = 1;
        }
        else if (ctx.canceled)
        {
            rightHeld = false;

            if (leftHeld)
                horizontalDirection = -1;
            else
                horizontalDirection = 0;
        }
    }
}
