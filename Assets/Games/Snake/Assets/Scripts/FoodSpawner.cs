using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;  // Assign the FoodPrefab in the Inspector
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    void Start()
    {
        SpawnFood();
    }

    public void SpawnFood()
    {
        int gridSize = 1;  // Adjust this if your grid size is different
        Vector2 spawnPosition = new Vector2(
            Mathf.RoundToInt(Random.Range(spawnAreaMin.x, spawnAreaMax.x) / gridSize) * gridSize,
            Mathf.RoundToInt(Random.Range(spawnAreaMin.y, spawnAreaMax.y) / gridSize) * gridSize
        );
        Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
    }
}
