using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    public void SpawnPlayer()
    {
        Instantiate(player);
    }
}
