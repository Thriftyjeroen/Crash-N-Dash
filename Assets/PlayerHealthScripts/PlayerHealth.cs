using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    string EnemyDamageTag = "Enemy";          
    GameObject playerObject;
    public float playerHealth;
    public float maxPossibleHealth = 100.0f;

    bool testboel = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = this.gameObject;
        print(playerObject.name);
        ResetPlayerHealth();
    }

    public bool CheckIfPlayerDead() => playerHealth <= 0 ? true : false;

    public void RemovePlayerHealth(float AmountToBeRemoved)
    {
        playerHealth -= AmountToBeRemoved;
    }

    public void AddPlayerHealth(float AmountToBeAdded)
    {
        playerHealth += AmountToBeAdded;
    }

    public void ResetPlayerHealth()
    {
        playerHealth = maxPossibleHealth;
    }


    //for oncollission2d both the playerobject and the collider need to be on fully kinematic settings 
    //and add tag to the enemy gameobject
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == EnemyDamageTag)
        {
            RemovePlayerHealth(collision.gameObject.GetComponent<EnemyTestScript>().ToBeGivenDamage());
            print("collided with" + collision.gameObject.tag);
        }
        else
        {
            print("found collision but no tag?");
        }
    }
}
