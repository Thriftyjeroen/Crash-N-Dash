using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ObstacleScript : MonoBehaviour
{
    public GameObject[] players;
    public GameObject healthManager;
    public GameObject parentObstacle;
    public bool allowedToShoot = true;
    public bool isFlameThrowerShooting = false;
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
                        if (distance < 3)
                        {
                            shootFlameThrower(4);
                        }
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
    void shootFlameThrower(float waitForSec)
    {
        allowedToShoot = false;
        StartCoroutine(shootFlames(5));
    }
    IEnumerator shootFlames(float waitForSec)
    {
        isFlameThrowerShooting = true;
        int amountOfFlameThrowerCorrections = 20;
        for (int i = 0; i < amountOfFlameThrowerCorrections; i++)
        {
            Vector3 targetDir = (findClosestPlayer(players, parentObstacle) - parentObstacle.transform.position).normalized;

            //idk what this does 
            float angleOfZ = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

            Quaternion newRotation = Quaternion.Euler(parentObstacle.transform.rotation.x, parentObstacle.transform.rotation.y, angleOfZ-90);
            parentObstacle.transform.rotation = newRotation;
            yield return new WaitForSeconds(waitForSec / amountOfFlameThrowerCorrections);
        }
        yield return new WaitForSeconds(waitForSec);
        isFlameThrowerShooting = false;  
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

    Vector3 findClosestPlayer(GameObject[] players, GameObject thisObstacle)
    {
        Vector3 returnthing = new Vector3(0, 0, 0);
        float closestDistance = float.PositiveInfinity;

        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(thisObstacle.transform.position, p.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                returnthing = p.transform.position;
            }
        }
        return returnthing;
    }
}
