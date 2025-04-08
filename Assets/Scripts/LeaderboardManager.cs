using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using System.Drawing;

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
            Color32 color32 = playerLeaderboard[rank].GetComponent<SpriteRenderer>().color;
            string color = $"#{color32.r:X2}{color32.g:X2}{color32.b:X2}{color32.a:X2}";
            
            leaderboard.text += $"<color={color}>{rank + 1} - {playerLeaderboard[rank].GetComponent<PlayerItemManager>().currentItem} \n";
        }
    }
}
