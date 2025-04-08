using UnityEngine;
using System.Collections;

public class GlueManager : MonoBehaviour
{
    bool activated = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || activated) return;

        activated = true;

        StartCoroutine(RemovePlayerSpeed(collision));

        GetComponent<Renderer>().enabled = false;
    }

    public IEnumerator RemovePlayerSpeed(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        player.maxAccel = 1f;

        Vector2 currentVelocity = rb.linearVelocity;
        rb.linearVelocity = currentVelocity * 0.25f;

        yield return new WaitForSecondsRealtime(2f);

        player.maxAccel = 10;

        Destroy(gameObject);
    }
}
