using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] private Transform checkPointsTransform;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private Transform playerFolder;
    public void OnJoin(PlayerInput playerInput)
    {
        playerInput.gameObject.transform.SetParent(playerFolder);

        if (playerInput.gameObject.TryGetComponent<CheckPointManager>(out var t)) return;
        CheckPointManager c = playerInput.gameObject.AddComponent<CheckPointManager>();
        c.Init(checkPointsTransform, obstacleSpawner);
    }
}
