using UnityEngine;

public class PlayerInitialize : MonoBehaviour
{
    PlayerList pL;
    void Start()
    {
        pL = FindAnyObjectByType<PlayerList>();
        pL.AddToList(gameObject);
    }
}
