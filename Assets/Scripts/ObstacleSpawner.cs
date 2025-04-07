using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //CheckList
    //Spawn Objects on random position
    //Only spawn objects on track
    //Spawn an object every lap
    //check if there is an item avaliable in the pool
    [SerializeField] private GameObject[] obstacles;
    Vector3 spawnPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnLap()
    {
        // Generate a random position within the range of the map
        spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0);

        // Check if the position is on a track using Physics2D.OverlapPoint
        //Collider2D hitCollider = Physics2D.OverlapPoint(spawnPosition);
        //if (hitCollider != null && hitCollider.CompareTag("Track"))
        //{
        // Instantiate the obstacle at a valid position
        Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPosition, Quaternion.identity);
        //}
        //else
        //{
        //    //If it didnt find a valid position call the method again
        //    OnLap();
        //}
        
        
    }

}
