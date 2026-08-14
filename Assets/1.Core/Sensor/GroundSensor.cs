using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider2D bodyCollider;

    [Header("Detection")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float castDistance = 0.05f;
    [SerializeField] private float sensorHeight = 0.02f;
    [SerializeField] private float skin = 0.01f;

    public bool IsGrounded { get; private set; }
    
    [SerializeField] private Collider2D groundCollider;
    public Collider2D GroundCollider => groundCollider;

    public void Probe()
    {
        IsGrounded = false;
        groundCollider = null;

        if (bodyCollider == null)
        {
            Debug.LogError("GroundSensor: BodyCollider is not assigned.");
            return;
        }

        Bounds bounds = bodyCollider.bounds;

        Vector2 origin = new Vector2(
            bounds.center.x,
            bounds.min.y + skin + sensorHeight * 0.5f
        );

        Vector2 size = new Vector2(
            bounds.size.x,
            sensorHeight
        );

        RaycastHit2D hit = Physics2D.BoxCast(
            origin,
            size,
            0f,
            Vector2.down,
            castDistance,
            groundLayer
        );

        if (hit.collider == null)
            return;

        IsGrounded = true;
        groundCollider = hit.collider;
    }

    private void OnDrawGizmosSelected()
    {
        if (bodyCollider == null)
            return;

        Bounds bounds = bodyCollider.bounds;

        Vector2 origin = new Vector2(
            bounds.center.x,
            bounds.min.y + skin + sensorHeight * 0.5f
        );

        Vector2 size = new Vector2(
            bounds.size.x,
            sensorHeight
        );

        Gizmos.DrawWireCube(
            origin + Vector2.down * (castDistance * 0.5f),
            new Vector2(size.x, size.y + castDistance)
        );
    }
}