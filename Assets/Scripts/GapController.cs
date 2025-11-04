using UnityEngine;

public class GapController : MonoBehaviour
{
    public Transform topPipe;
    public Transform bottomPipe;

    
    public void SetGap(float gap)
    {
        if (topPipe) topPipe.localPosition = new Vector3(topPipe.localPosition.x, gap * 0.5f, topPipe.localPosition.z);
        if (bottomPipe) bottomPipe.localPosition = new Vector3(bottomPipe.localPosition.x, -gap * 0.5f, bottomPipe.localPosition.z);
    }
}
