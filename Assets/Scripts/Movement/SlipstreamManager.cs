using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlipstreamManager : MonoBehaviour
{
    bool activated;
    float originalSpeed;
    float inc = 2;
    Player playerMovement;
    bool running = false;
    LayerMask mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<Player>();
        originalSpeed = playerMovement.GetMaxSpeed();
        mask = LayerMask.GetMask("CheckPoint");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1, ~mask);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            activated = true;
        } else activated = false;

        if (activated && playerMovement.GetMaxSpeed() < originalSpeed + inc)
        {
            StartCoroutine(changeSpeed());
        }
        else if (!activated && playerMovement.GetMaxSpeed() > originalSpeed)
        {
            StartCoroutine(changeSpeed());
        }

    }

    IEnumerator changeSpeed()
    {
        if (running) yield break;
        running = true;
        for (int i = 0; i < 10; i++)
        {
            playerMovement.AlterMaxSpeed(activated, inc / 10);
            yield return new WaitForEndOfFrame();
        }
        running = false;
    }
}
