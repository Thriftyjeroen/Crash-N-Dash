using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject playerList;
    [SerializeField] private TMP_Text leaderboard;

    List<Player> playerLeaderboard;

    public void Update()
    {
        playerLeaderboard = new();
        foreach (CheckPointManager playerCheckPointManager in playerList.GetComponentsInChildren<CheckPointManager>())
        {
            
            Player player = playerCheckPointManager.GetComponent<Player>();

            playerLeaderboard.Add(player);
        }


        playerLeaderboard = playerLeaderboard
            .OrderByDescending(p => p.GetComponent<CheckPointManager>().lap)
            .ThenByDescending(p => p.GetComponent<CheckPointManager>().GetLapProgress())
            .ToList();

        leaderboard.text = "";
        
        for (int rank = 0; rank < playerLeaderboard.Count; rank++)
        {
            leaderboard.text += $"{rank + 1} - {playerLeaderboard[rank].GetComponent<PlayerInput>().currentControlScheme} - {playerLeaderboard[rank].GetComponent<PlayerItemManager>().currentItem} \n";
        }
    }
}
