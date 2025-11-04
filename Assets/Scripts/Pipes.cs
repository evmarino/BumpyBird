using UnityEngine;

public class Pipes : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5.0f;   // fallback if GameManager not present

    [Header("Scoring Trigger (optional)")]
    public bool createScoreTrigger = false;
    public Vector2 triggerSize = new Vector2(0.25f, 6f);

    private void Start()
    {
        if (createScoreTrigger)
        {
            var go = new GameObject("ScoreTrigger");
            go.transform.SetParent(transform, false);
            go.transform.localPosition = Vector3.zero;

            var box = go.AddComponent<BoxCollider2D>();
            box.isTrigger = true;
            box.size = triggerSize;

            go.AddComponent<ScoreTrigger>();
        }
    }

    private void Update()
    {
        float currentSpeed = speed;
        if (GameManager.I != null) currentSpeed = GameManager.I.PipeSpeed();

        transform.position += Vector3.left * currentSpeed * Time.deltaTime;

        if (transform.position.x < -15.0f)
            Destroy(gameObject);
    }
}
