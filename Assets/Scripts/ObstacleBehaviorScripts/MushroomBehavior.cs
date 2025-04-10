using NUnit.Framework;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class MushroomBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool MushroomActive = true;
    public bool TestCollission = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TestCollission == true)
        {
            ActivateCollission();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collision.gameObject.GetComponent<PlayerMovement>().                     invert player controls when allowed
        MushroomActive = false;
    }

    void ActivateCollission()
    {
        TestCollission = false;
        StartCoroutine(MushroomCollissionAnimation());
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
