using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }
    TextMeshProUGUI winMessageText;
    GameObject playerList;
    PlayerJoinManager joinManager;

    private GameObject returnToMainMenuButton;
    private GameObject enableInfiniteModeButton;

    private bool infiniteMode = false;

    public void Init(GameObject _playerList, PlayerJoinManager _joinManager, GameObject _returnButton, GameObject _infiniteButton, TextMeshProUGUI _winMessageText)
    {
        Instance = this;
        playerList = _playerList;
        joinManager = _joinManager;
        returnToMainMenuButton = _returnButton;
        enableInfiniteModeButton = _infiniteButton;
        winMessageText = _winMessageText;

    }

    public void EndRace(CheckPointManager _winner)
    {
        _winner.score++;
        Debug.Log($"{_winner.name} won the race, their current score: {_winner.score}");

        if (_winner.score == 2 && !infiniteMode)
        {
            ShowButtons();
            if (winMessageText != null)
            {
                var winnerColor = _winner.GetComponent<SpriteRenderer>().color;
                winMessageText.color = winnerColor;
                winMessageText.gameObject.SetActive(true);
            }
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
        StartRaceCountdown(3f);
    }
    public void StartRaceCountdown(float duration)
    {
        StartCoroutine(FreezeGameForSeconds(duration));
    }

    private IEnumerator FreezeGameForSeconds(float duration)
    {
        Time.timeScale = 0f;

        if (CountdownManager.Instance != null)
            CountdownManager.Instance.ShowCountdown((int)duration);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f;
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
