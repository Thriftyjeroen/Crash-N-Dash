using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MineManager : MonoBehaviour
{
    ParticleSystem particles;
    float timeToWait;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        timeToWait = particles.main.duration;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Contains("Player")) return;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(DestroySelf());
        particles.Play();

        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        playerMovement.rb.AddForce(-transform.up * 200, ForceMode2D.Force);
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeToWait);
        Destroy(gameObject);
    }
}
