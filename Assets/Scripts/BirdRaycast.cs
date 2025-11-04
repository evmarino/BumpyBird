using UnityEngine;

public class BirdRaycast : MonoBehaviour
{
   public float rayDistance = 4f;
   public LayerMask Obstacle;
   public Vector2 localOffset = new Vector2(0.5f, 0f);
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.TransformPoint(localOffset), Vector2.right, rayDistance, Obstacle);
        if (hit.collider != null)
        {
            Debug.Log("Obstacle detected: " + hit.collider.name);
            // Implement logic to avoid the obstacle
        }
        Debug.DrawRay(transform.TransformPoint(localOffset), Vector2.right * rayDistance, Color.red);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 start = transform.TransformPoint(localOffset);
        Vector3 end = start + transform.right * rayDistance;
        Gizmos.DrawLine(start, end);
    }

}
