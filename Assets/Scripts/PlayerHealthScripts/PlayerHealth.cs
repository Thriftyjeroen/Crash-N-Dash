using System;
using System.Collections;
using UnityEngine;
//this script needs to be attached to a player (gets automatically done by the PlayerHealthChecks script
public class PlayerHealth : MonoBehaviour
{
    string EnemyDamageTag = "Enemy";
    GameObject playerObject;
    public float playerHealth;
    public float maxPossibleHealth = 100.0f;

    SpriteRenderer spriteRenderer;
    bool canBeHit = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = this.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetPlayerHealth();
    }

    public bool CheckIfPlayerDead() => playerHealth <= 0 ? true : false;

    public void RemovePlayerHealth(float AmountToBeRemoved)
    {
        if (!canBeHit) return;
        playerHealth -= AmountToBeRemoved;
        StartCoroutine(StartIFrames(1));
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
            if (collision.gameObject.name.Contains("bullet"))
            {
                GameObject.Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == EnemyDamageTag)
        {
            RemovePlayerHealth(collision.gameObject.GetComponent<EnemyTestScript>().ToBeGivenDamage());
            if (collision.gameObject.name.Contains("bullet"))
            {
                GameObject.Destroy(collision.gameObject);
            }
        }
    }

    IEnumerator StartIFrames(float pTime)
    {
        canBeHit = false;
        float flickerTime = pTime / 5;
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSecondsRealtime(flickerTime);
        }
        spriteRenderer.enabled = true;
        canBeHit = true;
    }
}