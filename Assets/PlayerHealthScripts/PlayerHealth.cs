using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    GameObject playerObject;
    float playerHealth;
    float maxPossibleHealth = 100.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = this.transform.parent.GetComponent<GameObject>();
        ResetPlayerHealth();
    }

    public bool CheckIfPlayerDead() => playerHealth <= 0 ? true : false;

    public void RemovePlayerHealth(float AmountToBeRemoved)
    {
        playerHealth += AmountToBeRemoved;
    }

    public void AddPlayerHealth(float AmountToBeAdded)
    {
        playerHealth += AmountToBeAdded;
    }

    public void ResetPlayerHealth()
    {
        playerHealth = maxPossibleHealth;
    }
}
