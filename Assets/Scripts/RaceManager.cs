using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }

    GameObject playerList;
    PlayerJoinManager joinManager;

    private GameObject returnToMainMenuButton;
    private GameObject enableInfiniteModeButton;

    private bool infiniteMode = false;

    public void Init(GameObject _playerList, PlayerJoinManager _joinManager, GameObject _returnButton, GameObject _infiniteButton)
    {
        Instance = this;
        playerList = _playerList;
        joinManager = _joinManager;
        returnToMainMenuButton = _returnButton;
        enableInfiniteModeButton = _infiniteButton;
    }

    public void EndRace(CheckPointManager _winner)
    {
        _winner.score++;
        Debug.Log($"{_winner.name} won the race, their current score: {_winner.score}");

        if (_winner.score == 3 && !infiniteMode)
        {
            ShowButtons();
        }
        else
        {
            ResetRace();
        }
    }

    public void SetInfiniteMode(bool value)
    {
        infiniteMode = value;
        HideButtons();
        ResetRace();
    }

    public void ResetRace()
    {
        foreach (var player in playerList.GetComponentsInChildren<Player>())
        {
            player.GetComponent<CheckPointManager>().ResetCheckPoints();
            joinManager.SetPosition(player.gameObject);
            joinManager.SetRotation(player.gameObject);
        }
    }

    private void ShowButtons()
    {
        if (returnToMainMenuButton != null) returnToMainMenuButton.SetActive(true);
        if (enableInfiniteModeButton != null) enableInfiniteModeButton.SetActive(true);
    }

    private void HideButtons()
    {
        if (returnToMainMenuButton != null) returnToMainMenuButton.SetActive(false);
        if (enableInfiniteModeButton != null) enableInfiniteModeButton.SetActive(false);
    }
}
