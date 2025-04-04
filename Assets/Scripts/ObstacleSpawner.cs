using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private string poolKey = "Obstacle";
    [SerializeField] private int poolSize = 10;

    Vector3 spawnPosition;

    void Start()
    {
        ObjectPooler.SetupPool(obstaclePrefab.GetComponent<Component>(), poolSize, poolKey);
    }

    public void OnLap()
    {
        spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0);

        Collider2D hitCollider = Physics2D.OverlapPoint(spawnPosition);
        if (hitCollider != null && hitCollider.CompareTag("Track"))
        {
            // Get an object from the pool OR create a new one if necessary
            GameObject obstacle = ObjectPooler.DequeueObject(poolKey, obstaclePrefab.GetComponent<Component>()).gameObject;

            if (obstacle != null)
            {
                obstacle.transform.position = spawnPosition;
                obstacle.transform.rotation = Quaternion.identity;
                obstacle.SetActive(true);
            }
        }
        else
        {
            OnLap(); // Retry if not a valid position
        }
    }
}
