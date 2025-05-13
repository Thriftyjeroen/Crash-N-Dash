using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EvilCarFactoryBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject explosiveCarPrefab;
    public GameObject[] allPlayers;
    GameObject thisObstacle;
    GameObject activeCar;

    string playerTag = "Player";
    public float respawnTimer = 2.0f;
    bool waitingForRespawn = false;


    void Start()
    {
        thisObstacle = gameObject;
        //finds all the gameobjects with the playertag
    }

    // Update is called once per frame
    void Update()
    {
        allPlayers = GameObject.FindGameObjectsWithTag(playerTag);
        //checks if activecar is null, if active car == null it spawns a new car
        if (activeCar == null && waitingForRespawn == false)
        {
            if (allPlayers.Length > 0)
            {
                StartCoroutine(StartChase(respawnTimer));
            }
            else
            {
                print("no players found");
            }
        }
    }

    /// <summary>
    /// instantiates the car after the timer has completed
    /// </summary>
    IEnumerator StartChase(float waitForSec)
    {
        print("starting chase");
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
        if (allPlayers != null)
        {
            activeCar = Instantiate(explosiveCarPrefab, position, Quaternion.identity);
            activeCar.GetComponent<ExplosiveCarBehavior>().GivePlayersGameObjects(allPlayers);
        }
    }

}
