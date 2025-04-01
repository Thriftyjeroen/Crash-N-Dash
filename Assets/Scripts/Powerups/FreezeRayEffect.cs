using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class FreezeRayEffect : MonoBehaviour
{
    public bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponentInChildren<FreezeRayEffect>() != null) return;
        }
        else { return; }
        if (activated) return;

        GetComponent<Renderer>().enabled = false;
        GetComponentInParent<Renderer>().enabled = false;

        activated = true;

        Player playerMovement = collision.gameObject.GetComponent<Player>();
        StartCoroutine(FreezePlayer(playerMovement));
    }

    IEnumerator FreezePlayer(Player playerMovement)
    {
        transform.parent.GetComponent<Renderer>().enabled = false;
        SpriteRenderer spriteRenderer = playerMovement.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.blue;
        playerMovement.rotationSpeed = 0f;
        playerMovement.maxAccel = 0.01f;
        yield return new WaitForSecondsRealtime(1);
        playerMovement.rotationSpeed = 50;
        playerMovement.maxAccel = 10f;
        spriteRenderer.color = originalColor;
        Destroy(gameObject.transform.parent.gameObject);
    }
}
