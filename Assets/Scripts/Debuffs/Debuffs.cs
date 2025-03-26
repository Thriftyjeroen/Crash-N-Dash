using UnityEngine;

public class Debuffs : MonoBehaviour
{
    PlayerList pL;
    private void Start()
    {
        pL = FindAnyObjectByType<PlayerList>();
    }

    public void GiveDebuff(int id, GameObject target)
    {

    }
}
