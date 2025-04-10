using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private int poolSize = 10;

    private void Start()
    {
        // Setup the pool for each obstacle
        foreach (GameObject obstacle in obstacles)
        {
            ObjectPooler.SetupPool(obstacle, poolSize, obstacle.name);
        }
    }

    public void OnLap()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0);

        // Make sure we only spawn on track
        Collider2D hitCollider = Physics2D.OverlapPoint(spawnPosition);
        if (hitCollider != null && hitCollider.CompareTag("Track"))
        {
            GameObject selectedPrefab = obstacles[Random.Range(0, obstacles.Length)];
            GameObject obstacle = ObjectPooler.DequeueObject(selectedPrefab.name, selectedPrefab);

            obstacle.transform.position = spawnPosition;
            obstacle.transform.rotation = Quaternion.identity;
            obstacle.SetActive(true);
        }
        else
        {
            // Retry if spawn position isn't valid
            OnLap();
        }
    }
}
