using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class ElectricFenceBehavior : MonoBehaviour
{
    bool electricFenceActive = true;
    float stunPlayerForSeconds = 1.5f;
    string playerTag = "Player";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collission)
    {
        if (electricFenceActive == true && collission.tag.Contains(playerTag))
        {
            electricFenceActive = false;
            StartCoroutine(setPlayerSpeedZero(collission.gameObject, stunPlayerForSeconds));
            StartCoroutine(setPlayerColor(Color.blue, Color.cyan, collission.gameObject, stunPlayerForSeconds));
        }
    }


    IEnumerator setPlayerSpeedZero(GameObject player, float amountOfSec)
    {
        int checkSpeedAmountOfTime = 30;
        for (int i = 0; i < checkSpeedAmountOfTime; i++)
        {
            player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            yield return new WaitForSeconds(amountOfSec / checkSpeedAmountOfTime);
        }
    }

    IEnumerator setPlayerColor(Color changeToColor, Color secondaryColor, GameObject player, float amountOfSec)
    {
        int loopAmountOfTimes = 30;
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        for (int i = 0; i < loopAmountOfTimes; i++)
        {
            if (i % 2 == 0)
            {
                spriteRenderer.color = changeToColor;
            }
            else
            {
                spriteRenderer.color = secondaryColor;
            }
            yield return new WaitForSeconds(amountOfSec / loopAmountOfTimes);
        }
        spriteRenderer.color = originalColor;
        electricFenceActive = true;
    }
}
