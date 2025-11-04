using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Obstacle"))
        {
            if (GameManager.I != null) GameManager.I.GameOver();
            Debug.Log("Game Over (Obstacle)");
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            if (GameManager.I != null) GameManager.I.GameOver();
            Debug.Log("Game Over (Ground)");
        }
    }
}
