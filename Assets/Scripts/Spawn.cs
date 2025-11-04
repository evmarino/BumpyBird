using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject prefab;

    [Header("Spawn Timing")]
    public float spawnRate = 1.0f;       // base on Easy
    public float hardSpawnMultiplier = 0.7f; // faster on Hard (0.7 = 30% faster)

    [Header("Vertical Randomization")]
    public float minHeight = -1.0f;
    public float maxHeight = 1.0f;

    [Header("Optional Gap Control")]
    public bool setGapFromManager = false; // requires GapController on prefab

    private Coroutine loop;

    private void OnEnable()
    {
        loop = StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        if (loop != null) StopCoroutine(loop);
        loop = null;
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            float interval = spawnRate;
            if (GameManager.I != null && GameManager.I.difficulty == Difficulty.Hard)
                interval *= hardSpawnMultiplier;

            yield return new WaitForSeconds(interval);
            SpawnOnce();
        }
    }

    private void SpawnOnce()
    {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);

        if (setGapFromManager && GameManager.I != null)
        {
            var gapCtrl = pipes.GetComponentInChildren<GapController>();
            if (gapCtrl != null)
            {
                gapCtrl.SetGap(GameManager.I.PipeGap());
            }
        }
    }
}
