using UnityEngine;

public class Debuff : MonoBehaviour
{
    public int id = 0;
    public string name = "placeholder";
    public string description = "Description";
    public string rarity = "common";

    public Debuff(int pId, string pName, string pDescription, string pRarity)
    {
        id = pId;
        name = pName;
        description = pDescription;
        rarity = pRarity;
    }
}
