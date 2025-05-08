using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public CheckPointManager checkpointManager;
    [SerializeField] private GameObject playerList;
    public void EndRace()
    {
        Debug.Log("Race finished!");
        //give winning player a point
        Invoke(nameof(ResetRace), 3f); // wait a bit before resetting, if needed
    }

    public void ResetRace()
    {
        checkpointManager.ResetCheckPoints();
        // Reset player position
        foreach (var player in playerList.GetComponentsInChildren<PlayerJoinManager>())
        {
            player.SetPosition();
            player.SetRotation();
        }
        
    }

}

