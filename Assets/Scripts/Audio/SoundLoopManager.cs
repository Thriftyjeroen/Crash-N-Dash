using System.Collections;
using UnityEngine;

public class SoundLoopManager : MonoBehaviour
{
    float introLength;

    [SerializeField] AudioSource intro;
    [SerializeField] AudioSource loop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        introLength = intro.clip.length - 0.1f;
        StartCoroutine(StartLoop());
        intro.Play();
    }

    IEnumerator StartLoop()
    {
        yield return new WaitForSecondsRealtime(introLength);
        loop.Play();
    }
}
