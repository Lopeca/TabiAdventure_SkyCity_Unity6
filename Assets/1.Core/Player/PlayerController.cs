using UnityEngine;
using UnityEngine.InputSystem;

public struct PlayerCommand
{
    // 지속되는 입력
    public float Horizontal;
    public bool Run;
    public bool DownKeyHeld;
    public bool DashKeyHeld;

    // 이번 틱에 발생한 입력
    public bool JumpPressed;
}
public class PlayerController : MonoBehaviour
{
    private bool leftHeld;
    private bool rightHeld;

    private PlayerCommand playerCommand;
    
    [Header("참조")] 
    [SerializeField] PlayerMotor motor;
    
    
    [SerializeField] private bool controllable;
    private void FixedUpdate()
    {
        if (!controllable) return;
        
        motor.Move(playerCommand);

        if (playerCommand.JumpPressed)
        {
            motor.Jump();
        }
        playerCommand.JumpPressed = false;

        SpriteFlipToLookDirection();
    }
    

    private void SpriteFlipToLookDirection()
    {
        if(playerCommand.Horizontal > 0) transform.localScale = new Vector3(1, 1, 1);
        if(playerCommand.Horizontal < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    public void OnLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            leftHeld = true;
            playerCommand.Horizontal = -1;
        }
        else if (ctx.canceled)
        {
            leftHeld = false;

            if (rightHeld)
                playerCommand.Horizontal = 1;
            else
                playerCommand.Horizontal = 0;
        }
    }

    public void OnRight(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            rightHeld = true;
            playerCommand.Horizontal = 1;
        }
        else if (ctx.canceled)
        {
            rightHeld = false;

            if (leftHeld)
                playerCommand.Horizontal = -1;
            else
                playerCommand.Horizontal = 0;
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerCommand.JumpPressed = true;
        }
    }
}
