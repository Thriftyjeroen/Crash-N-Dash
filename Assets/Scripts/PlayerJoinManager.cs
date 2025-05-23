using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] private Transform checkPointsTransform;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private Transform playerFolder;
    [SerializeField] private Vector2 joinLocation;
    [SerializeField] private GameObject returnButton;
    [SerializeField] private GameObject infiniteModeButton;
    [SerializeField] private TextMeshProUGUI winMessageText;

    List<Color> colorsInScene = new();
    Color[] colorsAvailable = { Color.magenta, Color.blue, Color.red, Color.cyan, Color.green, Color.yellow};

    LayerMask mask;
    [SerializeField] Quaternion rotation;

    private void Start()
    {
         mask = LayerMask.GetMask("CheckPoint");
    }

    public void OnJoin(PlayerInput playerInput)
    {
        SetPosition(playerInput.gameObject);
        SetRotation(playerInput.gameObject);

        playerInput.gameObject.transform.SetParent(playerFolder);

        if (playerInput.gameObject.TryGetComponent<CheckPointManager>(out var t)) return;
        CheckPointManager c = playerInput.gameObject.AddComponent<CheckPointManager>();
        RaceManager r = playerInput.gameObject.AddComponent<RaceManager>();
        r.Init(playerFolder.gameObject, this, returnButton, infiniteModeButton, winMessageText);
        c.Init(checkPointsTransform, obstacleSpawner, r);

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
    
    public void SetPosition(GameObject player)
    {
        player.transform.position = joinLocation;
    }

    public void SetRotation(GameObject player)
    {
        player.transform.rotation = rotation;
    }
}
