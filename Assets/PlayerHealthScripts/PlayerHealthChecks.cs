using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthChecks : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    string Playertag = "Player";
    public GameObject[] objectsWithTag;
    public List<GameObject> currentlyDeadPlayers;
    void Start()
    {
        objectsWithTag = GameObject.FindGameObjectsWithTag(Playertag);
        foreach (GameObject obj in objectsWithTag)
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
        foreach (GameObject obj in CheckAllPlayerLives(objectsWithTag))
        {
            if (!currentlyDeadPlayers.Contains(obj))
            {
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
}
