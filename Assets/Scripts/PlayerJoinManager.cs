using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] private Transform checkPointsTransform;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private Transform playerFolder;
    List<Color> colorsInScene = new();
    Color[] colorsAvailable = { Color.magenta, Color.blue, Color.red, Color.cyan, Color.green, Color.yellow};
    public void OnJoin(PlayerInput playerInput)
    {
        playerInput.gameObject.transform.SetParent(playerFolder);

        if (playerInput.gameObject.TryGetComponent<CheckPointManager>(out var t)) return;
        CheckPointManager c = playerInput.gameObject.AddComponent<CheckPointManager>();
        c.Init(checkPointsTransform, obstacleSpawner);

        SetColor(playerInput.gameObject.GetComponent<SpriteRenderer>());
    }

    private void SetColor(SpriteRenderer player)
    {
        foreach (Color colorChoice in colorsAvailable)
        {
            if (!colorsInScene.Contains(colorChoice))
            {
                colorsInScene.Add(colorChoice);
                player.color = colorChoice;
                return;
            }
        }
        player.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    
}
