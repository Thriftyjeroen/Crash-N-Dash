using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EvilCarFactoryBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject explosiveCarPrefab;
    GameObject[] allPlayers;
    GameObject thisObstacle;
    GameObject activeCar;

    string playerTag = "Player";
    float respawnTimer = 2.0f;
    bool waitingForRespawn = false;


    void Start()
    {
        thisObstacle = gameObject;
        //finds all the gameobjects with the playertag
        allPlayers = GameObject.FindGameObjectsWithTag(playerTag);
    }

    // Update is called once per frame
    void Update()
    {
        //checks if activecar is null, if active car == null it spawns a new car
        if (activeCar == null && waitingForRespawn == false)
        {
            StartCoroutine(StartChase(respawnTimer));
        }
    }

    /// <summary>
    /// instantiates the car after the timer has completed
    /// </summary>
    IEnumerator StartChase(float waitForSec)
    {
        waitingForRespawn = true;
        yield return new WaitForSeconds(waitForSec);
        CreateExplosiveCar(gameObject.transform.position);
        waitingForRespawn = false;
    }

    /// <summary>
    /// creates the explosive car at the position of the factory
    /// </summary>
    void CreateExplosiveCar(Vector3 position)
    {
        activeCar = Instantiate(explosiveCarPrefab, position, Quaternion.identity);
        activeCar.GetComponent<ExplosiveCarBehavior>().GivePlayersGameObjects(allPlayers);
    }
}
