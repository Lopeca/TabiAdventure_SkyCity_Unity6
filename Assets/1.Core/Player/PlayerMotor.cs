using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundSensor groundSensor;

    [Header("이동")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float jumpForce = 50f;

    public void Move(PlayerCommand pc)
    {
        float targetSpeed = moveSpeed * pc.Horizontal;

        float newSpeed = Mathf.MoveTowards(
            rb.linearVelocityX,
            targetSpeed,
            acceleration * Time.fixedDeltaTime
        );

        rb.linearVelocityX = newSpeed;
    }

    public void Jump()
    {
        groundSensor.Probe();
        
        if (!groundSensor.IsGrounded) return;
        
        rb.linearVelocityY = jumpForce;
    }
}