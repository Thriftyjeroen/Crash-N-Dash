using NUnit.Framework;
using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MushroomBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool MushroomActive = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (MushroomActive == true && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(revertPlayerControls(collision.gameObject));
            StartCoroutine(MushroomCollissionAnimation());
        }
    }

    IEnumerator revertPlayerControls(GameObject player)
    {
        player.GetComponent<Player>().AlterInvertControls(true);
        MushroomActive = false;
        yield return new WaitForSeconds(2);
        player.GetComponent<Player>().AlterInvertControls(false);
        MushroomActive = true;
    }

    IEnumerator MushroomCollissionAnimation()
    {
        Vector3 mushroomScaling = gameObject.transform.localScale;
        float amountOfLoops = 40;
        float currentYScale = mushroomScaling.y;
        bool lowerScale = true;

        for (int i = 0; i < amountOfLoops; i++)
        {
            if (i % 4 == 0)
            {
                if (lowerScale == true)
                {
                    lowerScale = false;
                }
                else
                {
                    lowerScale = true;
                }
            }
            if (lowerScale == true)
            {
                currentYScale -= 0.25f;
            }
            else
            {
                currentYScale += 0.25f;
            }
            gameObject.transform.localScale = new Vector3(mushroomScaling.x, currentYScale, mushroomScaling.z);
            yield return new WaitForSeconds(0.02f);
        }
        gameObject.transform.localScale = mushroomScaling;
        MushroomActive = true;
    }
}
