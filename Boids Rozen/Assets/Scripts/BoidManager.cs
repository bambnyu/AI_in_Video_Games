using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public GameObject sheepPrefab;
    public int flockSize = 20;
    public Vector2 spawnAreaSize = new Vector2(10, 10);

    void Start()
    {
        for (int i = 0; i < flockSize; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
            );
            Instantiate(sheepPrefab, spawnPosition, Quaternion.identity, transform);
        }
    }
}
