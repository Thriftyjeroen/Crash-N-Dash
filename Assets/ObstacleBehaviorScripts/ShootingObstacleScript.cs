using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ObstacleScript : MonoBehaviour
{
    GameObject[] players;
    GameObject healthManager;
    GameObject parentObstacle;
    bool allowedToShoot = true;
    [SerializeField] GameObject bulletPrefab;
    string Playertag = "Player";
    string CheckpointTag = "CheckPoint";
    string ObstacleTag = "Enemy";

    //  [SerializeField]GameObject testPlayerObject;

    float bulletSpeed = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag(Playertag);
        try
        {
            healthManager = GameObject.Find("healthManager");
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
            findClosestPlayer(players, parentObstacle);
            float distance = Vector3.Distance(parentObstacle.transform.position, findClosestPlayer(players, parentObstacle));
            if (distance < 5)
            {
                switch (parentObstacle.name)
                {
                    case "turret":
                        StartCoroutine(shootNormalBullet(1));
                        break;
                    case "shotgun":
                        StartCoroutine(shootShotgunBullet(3));
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
    }
    IEnumerator shootNormalBullet(float waitForSec)
    {
        StartCoroutine(InstantiateBullet(parentObstacle.transform.position, findClosestPlayer(players, parentObstacle)));
        print("i shot normal bullet");
        allowedToShoot = false;
        yield return new WaitForSeconds(waitForSec);
        allowedToShoot = true;
    }
    IEnumerator shootShotgunBullet(float waitForSec)
    {
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(InstantiateBullet(parentObstacle.transform.position, findClosestPlayer(players, parentObstacle) * i));
        }
        allowedToShoot = false;
        yield return new WaitForSeconds(waitForSec);
        allowedToShoot = true;
    }
    IEnumerator InstantiateBullet(Vector3 thisObstaclePosition, Vector3 closestPlayerPos)
    {
        //get direction of player as vector 3
        Vector3 targetDir = (closestPlayerPos - thisObstaclePosition).normalized;

        GameObject newBullet = Instantiate(bulletPrefab, parentObstacle.transform.position, Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        //add force to direction of player
        rb.AddRelativeForce(targetDir * bulletSpeed);
        yield return new WaitForSeconds(4);
        GameObject.Destroy(newBullet);
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
        throwFlamesAtPlayer(parentObstacle.transform.position, findClosestPlayer(players, parentObstacle), seconds);
    }

    Vector3 findClosestPlayer(GameObject[] players, GameObject thisObstacle)
    {
        Vector3 returnthing = new Vector3(0, 0, 0);
        float closestDistance = 100;
        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(thisObstacle.transform.position, p.transform.position);
            if (closestDistance < distance)
            {
                returnthing = p.transform.position;
            }
            print(distance);
        }
        return returnthing;
    }
}
