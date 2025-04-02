using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;

public class PlayerSpawning : MonoBehaviour
{
    public Quaternion spawnRotation;
    int checkingSpace = 1;
   

    Vector2[] directions;
    private void Awake()
    {
        directions = new Vector2[]
        {
        transform.right,
        -transform.right,
        -transform.up
        };
    }
    public void PlayerJoin(PlayerInput playerInput)
    {
        LayerMask mask = LayerMask.GetMask("CheckPoint");
        playerInput.transform.SetParent(transform);
        playerInput.transform.rotation = spawnRotation;

        foreach (Transform player in GetComponentInChildren<Transform>())
        {
            foreach (Vector2 dir in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(player.transform.position, dir, checkingSpace, ~mask);
                Debug.DrawRay(player.transform.position, dir * checkingSpace, Color.red, 10f);
                Debug.Break();
            }
        }
    }
}
