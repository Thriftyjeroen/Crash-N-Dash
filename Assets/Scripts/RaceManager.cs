using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    GameObject playerList;
    PlayerJoinManager joinManager;

    public void EndRace()
    {
        Debug.Log("Race finished!");
        //give winning player a point
        ResetRace();
    }

    public void ResetRace()
    {
        // Reset player position
        foreach (var player in playerList.GetComponentsInChildren<Player>())
        {
            player.GetComponent<CheckPointManager>().ResetCheckPoints();
            joinManager.SetPosition(player.gameObject);
            joinManager.SetRotation(player.gameObject);
        }
        
    }
    public void Init(GameObject _playerList, PlayerJoinManager _joinManager)
    {
        playerList = _playerList;
        joinManager = _joinManager;
    }

}

