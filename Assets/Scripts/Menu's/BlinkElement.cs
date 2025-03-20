using System.Collections;
using UnityEngine;

public class BlinkElement : MonoBehaviour
{
    [SerializeField] private GameObject objectToBlink;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        bool keepBlinking = true;
        while (keepBlinking)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            objectToBlink.SetActive(!objectToBlink.activeSelf);
        }
    }
}
