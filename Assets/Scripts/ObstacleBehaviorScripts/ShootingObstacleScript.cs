using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ObstacleScript : MonoBehaviour
{
    public GameObject[] players;
    public GameObject healthManager;
    public GameObject turretGameObject;
    public bool allowedToShoot = true;
    [SerializeField] GameObject bulletPrefab;
    GameObject flame = null;
    string Playertag = "Player";
    float bulletSpeed = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets the gameobject where this script is attached to
        turretGameObject = gameObject;

        //find every player (a player is a gameobject with the player tag)
        players = GameObject.FindGameObjectsWithTag(Playertag);
        try
        {
            healthManager = GameObject.Find("healthManager");
        }
        catch
        {
            //if not found print this string
            print("cannot find object with playerhealtcheck script attached to it");
        }

        //if the gameObject name is flamethrower it turns off the flame
        if(turretGameObject.name.Contains("flamethrower"))
        {
            flame = turretGameObject.transform.GetChild(0).gameObject;
            flame.SetActive(false);
        }
    }

    // Update is called once per frame
    //in the update it chooses an action to perform based on the name of the gameobject
    void Update()
    {
        if (allowedToShoot == true)
        {
            //finds the distance between this turret and the closest player
            float distance = Vector3.Distance(turretGameObject.transform.position, findClosestPlayer(players, turretGameObject));

            //if the distance is less than 5, it can shoot the bullet
            if (distance < 5)
            {
                switch (turretGameObject.name)
                {
                    // with the "when" keyword another condition can be added, basically an if statement kinda
                    case string name when name.Contains("TurretGun"):
                        StartCoroutine(shootNormalBullet(1));
                        break;
                    case string name when name.Contains("TurretShotgun"):
                        StartCoroutine(shootShotgunBullet(3));
                        break;
                    case string name when name.Contains("flamethrower"):
                        if (distance < 3)
                        {
                            StartCoroutine(shootFlames(3));
                        }
                        break;
                    case string name when name.Contains("lazer"):
                        {
                            //shooting logic here if needed
                        }
                        break;
                    case string name when name.Contains("DartTrap"):
                        {
                            StartCoroutine(shootNormalBullet(1));
                        }
                        break;
                    default:
                        print("i dont know what i am cuh, pls hewp devewopeee :(");
                        break;
                }

            }

        }
    }

    /// <summary>
    /// method shoots 1 bullet to the exact position of the player
    /// </summary>
    IEnumerator shootNormalBullet(float waitForSec)
    {
        StartCoroutine(InstantiateBullet(turretGameObject.transform.position, findClosestPlayer(players, turretGameObject)));
        allowedToShoot = false;
        yield return new WaitForSeconds(waitForSec);
        allowedToShoot = true;
    }

    /// <summary>
    /// method shoots 3 bullets in the general direction of the closest player
    /// </summary>
    IEnumerator shootShotgunBullet(float waitForSec)
    {
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(InstantiateBullet(turretGameObject.transform.position, findClosestPlayer(players, turretGameObject) * i));
        }
        allowedToShoot = false;
        yield return new WaitForSeconds(waitForSec);
        allowedToShoot = true;
    }

    /// <summary>
    /// method starts the flameshooting 
    /// </summary>
    IEnumerator shootFlames(float waitForSec)
    {
        //bools to keep track if the flamethower is shooting
        flame.gameObject.SetActive(true);
        allowedToShoot = false;
        int amountOfFlameThrowerCorrections = 20;

        //tracks where the flamethrower needs to shoot
        for (int i = 0; i < amountOfFlameThrowerCorrections; i++)
        {
            //finds where the flamethrower should be shooting to hit the closest player
            Vector3 targetDir = (findClosestPlayer(players, turretGameObject) - turretGameObject.transform.position).normalized;
            float angleOfZ = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

            //turns the flamethrower to the calculated angle
            Quaternion newRotation = Quaternion.Euler(turretGameObject.transform.rotation.x, turretGameObject.transform.rotation.y, angleOfZ - 90);
            turretGameObject.transform.rotation = newRotation;

            //wait for amount of secs / amountOfCorrections
            yield return new WaitForSeconds(waitForSec / amountOfFlameThrowerCorrections);
        }
        //sets the flame to not active
        flame.gameObject.SetActive(false);

        //waits for secs before it is allowed to shoots
        yield return new WaitForSeconds(waitForSec);
        allowedToShoot = true;
    }



    /// <summary>
    /// method instantiates a bullet, after no hit it deletes itself
    /// </summary>
    IEnumerator InstantiateBullet(Vector3 thisObstaclePosition, Vector3 closestPlayerPos)
    {
        Vector3 targetDir;
        Vector3 downwardDirection = -transform.up;
        // If it's a DartTrap, shoot straight forward
        if (turretGameObject.CompareTag("dartTrap"))
        {
            // Assuming forward is right (x-axis). Adjust based on prefab rotation if needed.
            targetDir = downwardDirection;
        }
        else
        {
            // Normal turrets shoot at the player
            targetDir = (closestPlayerPos - thisObstaclePosition).normalized;
        }

        //instantiates a new bullet
        GameObject newBullet = Instantiate(bulletPrefab, turretGameObject.transform.position, Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();

        //add force to direction of player
        rb.AddRelativeForce(targetDir * bulletSpeed);

        //bullet has a lifetime of 4 seconds, if it didnt hit anything it is removed
        yield return new WaitForSeconds(4);
        GameObject.Destroy(newBullet);
    }


    /// <summary>
    /// method finds the closest player position to the gameobject provided 
    /// </summary>
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
