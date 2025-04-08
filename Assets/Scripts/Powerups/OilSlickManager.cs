using UnityEngine;
using System.Collections;

public class OilSlickManager : MonoBehaviour
{
    bool activated = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || activated) return;

        activated = true;

        StartCoroutine(RemovePlayerSteering(collision));

        GetComponent<Renderer>().enabled = false;
    }

    public IEnumerator RemovePlayerSteering(Collider2D collision)
    {
        Player playerMovement = collision.gameObject.GetComponent<Player>();
        float originalSpeed = playerMovement.GetRotationSpeed();
        playerMovement.AlterRotation(false, originalSpeed);
        yield return new WaitForSecondsRealtime(1);
        playerMovement.AlterRotation(true, originalSpeed);
        Destroy(gameObject);
    }
}
