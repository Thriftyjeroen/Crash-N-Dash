using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    GameObject[] players;
    [SerializeField] GameObject healthManager;
    [SerializeField] GameObject parentObstacle;
    bool allowedToShoot = true;
    [SerializeField]GameObject bulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            healthManager = GameObject.Find("healthManager");
            players = healthManager.GetComponent<PlayerHealthChecks>().GetAllPlayers();
        }
        catch
        {
            print("cannot find object with playerhealtcheck script attached to it");
        }
        parentObstacle = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowedToShoot == true)
        {
            switch (parentObstacle.name)
            {
                case "turret":
                    StartCoroutine(shootNormalBullet(3));
                    break;
                case "shotgun":
                    StartCoroutine(shootShotgunBullet(5));
                    break;
                case "flamethrower":
                    shootFlamethrower(5);
                    break;
                case "lazer":
                    break;
                default:
                    print("i dont know what i am cuh, pls hewp devewopeee :(");
                    break;
            }
        }
    }
    IEnumerator shootNormalBullet(float waitForSec)
    {
        InstantiateBullet(parentObstacle.transform.position, findClosestPlayer(players));
        print("i shot normal bullet");
        allowedToShoot = false;
        yield return new WaitForSeconds(waitForSec);
        allowedToShoot = true;
    }
    IEnumerator shootShotgunBullet(float waitForSec)
    {
        for (int i = 0; i < 3; i++)
        {
            InstantiateBullet(parentObstacle.transform.position, findClosestPlayer(players));
        }
        allowedToShoot = false;
        yield return new WaitForSeconds(waitForSec);
        allowedToShoot = true;
    }

    void InstantiateBullet(Vector3 thisObstaclePosition, Vector3 closestPlayerPos)
    {
        GameObject newBullet = Instantiate(bulletPrefab,parentObstacle.transform.position,Quaternion.identity);
    }

    IEnumerator throwFlamesAtPlayer(Vector3 thisObstaclePosition, Vector3 closestPlayerPos, float seconds)
    {
        allowedToShoot = true;
        yield return new WaitForSeconds(seconds);
        allowedToShoot = false;
        StartCoroutine(flameThrowerCountdown(seconds));
    }

    IEnumerator flameThrowerCountdown(float seconds)
    {
        allowedToShoot = false;
        yield return new WaitForSeconds(seconds);
        allowedToShoot = true;
    }
    void shootFlamethrower(float seconds)
    {
        throwFlamesAtPlayer(parentObstacle.transform.position, findClosestPlayer(players), seconds);
    }

    Vector3 findClosestPlayer(GameObject[] players)
    {
        Vector3 closestPosition = new Vector3(0, 0, 0);
        foreach (GameObject player in players)
        {
            if (player.transform.position.magnitude > closestPosition.magnitude)
            {
                closestPosition = player.transform.position;
            }
        }
        if (closestPosition == new Vector3(0, 0, 0))
        {
            System.Random random1 = new System.Random();
            System.Random random2 = new System.Random();
            System.Random random3 = new System.Random();
            closestPosition = new Vector3(random1.Next(0, 9), random1.Next(0, 9), random1.Next(0, 9));
        }
        return closestPosition;
    }
}
