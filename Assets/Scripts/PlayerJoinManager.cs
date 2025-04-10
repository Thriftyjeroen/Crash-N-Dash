using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] private Transform checkPointsTransform;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private Transform playerFolder;
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
        if (playerFolder.childCount != 0)
        {
            playerInput.gameObject.transform.position = new Vector2(1000, 1000);
            SetPosition(playerInput.gameObject);
        }

        SetRotation(playerInput.gameObject);


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
    

    void SetRotation(GameObject player)
    {
        player.transform.rotation = rotation;
    }

    void SetPosition(GameObject player)
    {
        foreach (Player listPlayer in playerFolder.GetComponentsInChildren<Player>())
        {
            Vector2[] directions =
            {
                listPlayer.transform.right,
                -listPlayer.transform.right,
                -listPlayer.transform.up
            };
            foreach (Vector2 direction in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(listPlayer.transform.position, direction, 1, ~mask);

                if (hit.collider == null)
                {
                    player.transform.position = listPlayer.transform.position + (Vector3)direction;
                    return;
                }
            }
        }
    }
}
