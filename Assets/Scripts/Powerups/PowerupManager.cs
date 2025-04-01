using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] GameObject spikePrefab;
    [SerializeField] GameObject minePrefab;
    [SerializeField] GameObject oilPrefab;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] GameObject grapplePrefab;
    [SerializeField] GameObject fakePowerupBoxPrefab;
    [SerializeField] GameObject freezeRayPrefab;


    int powerupAmount = 1;
    private void Update()
    {
        Physics2D.OverlapCircle(new Vector2(0, 0), 5);
    }
    private void Start()
    {
        powerupAmount = Enum.GetValues(typeof(PowerupType)).Length;
    }
    public PowerupType GetRandomItem()
    {
        return PowerupType.FreezeRay;
        return (PowerupType)UnityEngine.Random.Range(0, powerupAmount);
    }

    public void Activate(PowerupType? powerUp, GameObject pPlayer)
    {
        switch (powerUp)
        {
            case null:
                print("Honk Honk");
                break;

            case PowerupType.SpeedBoost:
                SpeedBoost(pPlayer);
                break;

            case PowerupType.Spike:
                DropItem(pPlayer, spikePrefab);
                break;

            case PowerupType.Mine:
                DropItem(pPlayer, minePrefab);
                break;

            case PowerupType.OilSlick:
                DropItem(pPlayer, oilPrefab);
                break;

            case PowerupType.FakePowerupBox:
                DropItem(pPlayer, fakePowerupBoxPrefab);
                break;

            case PowerupType.Shield:
                Shield(pPlayer);
                break;

            case PowerupType.GrappleHook:
                GrappleHook(pPlayer);
                break;

            case PowerupType.FreezeRay:
                FreezeRay(pPlayer);
                break;
        }
    }

    void SpeedBoost(GameObject pPlayer)
    {
        PlayerMovement playerMovement = pPlayer.GetComponent<PlayerMovement>();
        playerMovement.rb.AddForce(transform.up * 200, ForceMode2D.Force);
    }
    void DropItem(GameObject pPlayer, GameObject pPrefab)
    {
        Vector3 spawnPoint = pPlayer.transform.GetChild(0).position;
        Instantiate(pPrefab, spawnPoint, new());
    }

    void Shield(GameObject pPlayer)
    {
        Instantiate(shieldPrefab, pPlayer.transform);
    }
    void GrappleHook(GameObject pPlayer)
    {
        LayerMask mask = LayerMask.GetMask("CheckPoint");

        RaycastHit2D hit = Physics2D.Raycast(pPlayer.transform.position, transform.up, 100, ~mask);

        StartCoroutine(GotoGrapplePoint(hit.point, pPlayer));
        
        //Debug.DrawRay(transform.position, transform.up * 20, Color.green);
    }

    IEnumerator GotoGrapplePoint(Vector2 goal, GameObject pPlayer)
    {
        GameObject grappleHook = Instantiate(grapplePrefab, pPlayer.transform, new());
        LineRenderer lineRenderer = grappleHook.GetComponent<LineRenderer>();

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * 5f; 
            Vector2 newPos = Vector2.Lerp(pPlayer.transform.position, goal, time);
            grappleHook.transform.position = newPos;
            lineRenderer.SetPosition(0, pPlayer.transform.position);
            lineRenderer.SetPosition(1, newPos);
            yield return null;
        }

        time = 0f;
        while (Vector2.Distance(pPlayer.transform.position, goal) > 0.5f)
        {
            time += Time.deltaTime / 5; 
            Vector2 newPos = Vector2.Lerp(pPlayer.transform.position, goal, time);
            pPlayer.transform.position = newPos;
            lineRenderer.SetPosition(0, pPlayer.transform.position);
            yield return null;
        }

        // This is a nice comment

        Destroy(grappleHook);
    }

    void FreezeRay(GameObject pPlayer)
    {
        LayerMask mask = LayerMask.GetMask("Wall");

        RaycastHit2D hit = Physics2D.Raycast(pPlayer.transform.position, transform.up, 100, mask);

        StartCoroutine(ShootFreezeRay(hit.point, pPlayer));

    }

    IEnumerator ShootFreezeRay(Vector2 goal, GameObject pPlayer)
    {
        GameObject freezeRay = Instantiate(freezeRayPrefab, pPlayer.transform, new());
        FreezeRayEffect freezeDetector = freezeRay.GetComponentInChildren<FreezeRayEffect>();
        
        LineRenderer lineRenderer = freezeRay.GetComponent<LineRenderer>();

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * 5f;
            Vector2 newPos = Vector2.Lerp(pPlayer.transform.position, goal, time);
            freezeRay.transform.position = newPos;
            lineRenderer.SetPosition(0, pPlayer.transform.position);
            lineRenderer.SetPosition(1, newPos);
            freezeDetector.transform.position = newPos;
            yield return null;
        }

        if (!freezeDetector.activated)
        {
            Destroy(freezeRay);
        }
    }
}

public enum PowerupType
{
    SpeedBoost,
    Spike,
    Mine,
    OilSlick,
    Shield,
    GrappleHook,
    FakePowerupBox,
    FreezeRay
}