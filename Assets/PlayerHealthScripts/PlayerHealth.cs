using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
//this script needs to be attached to a player (gets automatically done by the PlayerHealthChecks script
public class PlayerHealth : MonoBehaviour
{
    string EnemyDamageTag = "Enemy";
    GameObject playerObject;
    public float playerHealth;
    public float maxPossibleHealth = 100.0f;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = this.gameObject;
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

    void PlayerDead()
    {
        this.gameObject.SetActive(false);
        //zet hier eventueel effects ofzo bij wanneer de player dood gaat
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
            print(playerObject.name + "collided with" + collision.gameObject.name);
            //error message when gameobject is disabled but still collides(?)
        }
    }
}
