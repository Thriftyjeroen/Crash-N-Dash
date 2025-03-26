using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script needs to be attached to an (empty) gameobject that doesnt get destroyed
public class PlayerHealthChecks : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string Playertag = "Player";
    string CheckpointTag = "CheckPoint";
    string ObstacleTag = "Enemy";

    public GameObject[] objectsWithPlayerTag;
    public List<GameObject> currentlyDeadPlayers;
    float PlayerTimeOutWhenDead = 2.5f;

    void Start()
    {
        //finds all objects with player tag and adds a healthscript
        objectsWithPlayerTag = GameObject.FindGameObjectsWithTag(Playertag);
        foreach (GameObject obj in objectsWithPlayerTag)
        {
            if (obj.GetComponent<PlayerHealth>() == null)
            {
                obj.AddComponent<PlayerHealth>();
            }
            obj.GetComponent<PlayerHealth>().ResetPlayerHealth();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //checks if each player still has more than 0 hp each frame
        foreach (GameObject obj in CheckAllPlayerLives(objectsWithPlayerTag))
        {
            if (!currentlyDeadPlayers.Contains(obj))
            {
                StartCoroutine(PlayerRespawnTimer(PlayerTimeOutWhenDead, obj));
                currentlyDeadPlayers.Add(obj);
            }
        }
    }
    /// <summary>
    /// returns a list of players who have died (hp lower than 0)
    /// </summary>
    public List<GameObject> CheckAllPlayerLives(GameObject[] gameObjectList)
    {
        List<GameObject> returnList = new List<GameObject>();
        foreach (GameObject gameObject in gameObjectList)
        {
            if (gameObject.GetComponent<PlayerHealth>().CheckIfPlayerDead() == true) returnList.Add(gameObject);
        }
        return returnList;
    }

    public IEnumerator PlayerRespawnTimer(float seconds, GameObject obj)
    {
        MakePlayerPrefabAnObstacle(obj, obj.transform.position);
        obj.gameObject.SetActive(false);
        yield return new WaitForSeconds(seconds);
        currentlyDeadPlayers.Remove(obj);
        obj.GetComponent<PlayerHealth>().ResetPlayerHealth();
        obj.gameObject.SetActive(true);
        GameObject closestSpawn = FindClosestCheckpoint(obj);
        obj.transform.position = closestSpawn.transform.position;
        obj.transform.rotation = closestSpawn.transform.rotation;
    }
    /// <summary>
    /// method makes a copy of the player, removes the scripts and adds an enemyscript so it can deal damage to players
    /// </summary>
    void MakePlayerPrefabAnObstacle(GameObject toCopyObject, Vector3 location)
    {
        GameObject newGameObject;
        newGameObject = Instantiate(toCopyObject);
        newGameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        Destroy(newGameObject.GetComponent<PlayerHealth>());
        Destroy(newGameObject.GetComponent<HealthTestscript>());
        newGameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        newGameObject.GetComponent<Rigidbody2D>().gravityScale = 0;


        //newGameObject.AddComponent<EnemyTestScript>();
        //newGameObject.tag = ObstacleTag;
        //when more scripts are attached to this, add them also to the delete list ^
    }

    /// <summary>
    /// find nearest checkpoint (gameObject with checkpoint tag)
    /// </summary>
    GameObject FindClosestCheckpoint(GameObject obj)
    {
        Vector3 playerPosition = obj.transform.position;
        GameObject closestPossibleSpawn = obj;
        GameObject[] CheckPoints = GameObject.FindGameObjectsWithTag(CheckpointTag);
        foreach (GameObject checkPoint in CheckPoints)
        {
            if (checkPoint.transform.position.magnitude > closestPossibleSpawn.transform.position.magnitude)
            {
                closestPossibleSpawn = checkPoint;
            }
        }
        return closestPossibleSpawn;
    }

    public GameObject[] GetAllPlayers() => objectsWithPlayerTag;
}
