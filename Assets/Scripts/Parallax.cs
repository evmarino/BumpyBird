using UnityEngine;

public class Parallax: MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float scrollSpeed = 1f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        Vector2 offset = meshRenderer.material.mainTextureOffset;
        offset.x += scrollSpeed * Time.deltaTime;
        meshRenderer.material.mainTextureOffset = offset;
    }
}
