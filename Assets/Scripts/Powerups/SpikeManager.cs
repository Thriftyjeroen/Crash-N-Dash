using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SpikeManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Contains("Player")) return;

        Destroy(gameObject);
    }
}
