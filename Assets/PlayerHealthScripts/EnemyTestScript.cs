using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    float ThisObjectDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        System.Random random = new System.Random();
        ThisObjectDamage = random.Next(20, 60);
    }

    //give damage to player according to how much damage this object can give
    public float ToBeGivenDamage() => ThisObjectDamage;
}
