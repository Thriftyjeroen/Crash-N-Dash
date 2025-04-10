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
    public List<GameObject> playerObstacles;
    float PlayerTimeOutWhenDead = 1.5f;

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
        objectsWithPlayerTag = GameObject.FindGameObjectsWithTag(Playertag);
        foreach (GameObject obj in objectsWithPlayerTag)
        {
            if (obj.GetComponent<PlayerHealth>() == null)
            {
                obj.AddComponent<PlayerHealth>();
                obj.GetComponent<PlayerHealth>().ResetPlayerHealth();
            }
        }
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
        MakePlayerPrefabAnObstacle(obj);
        obj.transform.position = new Vector2(1000, 1000);
        yield return new WaitForSeconds(seconds);
        currentlyDeadPlayers.Remove(obj);
        obj.GetComponent<PlayerHealth>().ResetPlayerHealth();
        GameObject closestSpawn = obj.GetComponent<CheckPointManager>().GetLastPassedCheckpoint();

        obj.transform.position = closestSpawn.transform.position;
    }
    /// <summary>
    /// method makes a copy of the player, removes the scripts and adds an enemyscript so it can deal damage to players
    /// </summary>
    void MakePlayerPrefabAnObstacle(GameObject toCopyObject)
    {
        return;
        GameObject newGameObject = Instantiate(toCopyObject);
        newGameObject.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        Destroy(newGameObject.GetComponent<PlayerHealth>());
        Destroy(newGameObject.GetComponent<HealthTestscript>());
        Destroy(newGameObject.GetComponent<PlayerItemManager>());
        Destroy(newGameObject.GetComponent<PowerupManager>());
        newGameObject.tag = "Untagged";
        newGameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        newGameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        playerObstacles.Add(newGameObject);


        //newGameObject.AddComponent<EnemyTestScript>();
        //newGameObject.tag = ObstacleTag;
        //when more scripts are attached to this, add them also to the delete list ^
    }

    public GameObject[] GetAllPlayers() => objectsWithPlayerTag;
}
