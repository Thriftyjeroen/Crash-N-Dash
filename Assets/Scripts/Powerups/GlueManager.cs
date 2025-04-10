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
        Player playerMovement = collision.gameObject.GetComponent<Player>();
        float originalAccel = playerMovement.GetMaxAccel();
        float originalSpeed = playerMovement.GetMaxSpeed();
        playerMovement.AlterMaxAccel(false, 8f);
        playerMovement.AlterMaxSpeed(false, originalSpeed - 1f);
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        Vector2 currentVelocity = rb.linearVelocity;
        rb.linearVelocity = new Vector2(0, 0);

        yield return new WaitForSecondsRealtime(500f);

        playerMovement.AlterMaxAccel(true, 8f);
        playerMovement.AlterMaxSpeed(true, originalSpeed - 1f);

        Destroy(gameObject);
    }
}
