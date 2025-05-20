using System.Collections;
using System.Linq;
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
    public GameObject turretGameObject;
    public bool allowedToShoot = true;
    [SerializeField] GameObject bulletPrefab;
    GameObject flame = null;
    string Playertag = "Player";
    float bulletSpeed = 10;
    bool canCheckForPlayers = true;
    float maxDistanceFromPlayer = 5;
    float maxDistanceForFlameThrower = 3;
    float closestPlayerDistance = 10;
    bool canUpdatePlayerPosition = true;
    bool canRotateTurret = true;

    //vector 3 for the predicted future position
    Vector3 futurePosition = Vector3.zero;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gets the gameobject where this script is attached to
        turretGameObject = gameObject;

        //find every player (a player is a gameobject with the player tag)
        players = GameObject.FindGameObjectsWithTag(Playertag);

        //if the gameObject name is flamethrower it turns off the flame
        if (turretGameObject.name.Contains("flamethrower"))
        {
            flame = turretGameObject.transform.GetChild(0).gameObject;
            flame.SetActive(false);
        }
    }

    // Update is called once per frame
    //in the update it chooses an action to perform based on the name of the gameobject
    void Update()
    {
        if (canCheckForPlayers && players.Length > 0)
        {
            StartCoroutine(checkForDistance(0.2f));
        }

        //if the nearest player is in range
        if (players.Length > 0 && closestPlayerDistance < maxDistanceFromPlayer)
        {
            //dart trap does not need to rotate to the player
            if (!turretGameObject.name.Contains("DartTrap"))
            {
                if (canRotateTurret == true)
                {
                    StartCoroutine(RotateToAnAngle(turretGameObject));
                }
            }


            //if the turret is allowed to shoot, only then it calculate the things it needs
            if (allowedToShoot == true)
            {

                //if the distance is less than 5, it can shoot the bullet
                if (closestPlayerDistance < maxDistanceFromPlayer)
                {
                    switch (turretGameObject.name)
                    {
                        // with the "when" keyword another condition can be added, basically an if statement kinda
                        case string name when name.Contains("TurretGun"):
                            StartCoroutine(shootNormalBullet(1.5f));
                            break;
                        case string name when name.Contains("TurretShotgun"):
                            StartCoroutine(shootShotgunBullet(3));
                            break;
                        case string name when name.Contains("flamethrower"):
                            if (closestPlayerDistance < maxDistanceForFlameThrower)
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
                                StartCoroutine(shootNormalBullet(1.5f));
                            }
                            break;
                        default:
                            print("i dont know what i am cuh, pls hewp devewopeee :(" + name);
                            break;
                    }

                }
            }
        }
        //if the turret cant shoot, but it can check for players
        else if (canCheckForPlayers == true)
        {
            //checks for players that have not been added to the list
            print("checking for players");
            StartCoroutine(checkForPlayers());
        }
    }

    /// <summary>
    /// method shoots 1 bullet to the exact position of the player
    /// </summary>
    IEnumerator shootNormalBullet(float waitForSec)
    {
        StartCoroutine(InstantiateBullet(turretGameObject.transform.position, findClosestPlayer(players, turretGameObject), true));
        allowedToShoot = false;
        yield return new WaitForSeconds(waitForSec);
        allowedToShoot = true;
    }

    /// <summary>
    /// method shoots 3 bullets in the general direction of the closest player
    /// </summary>
    IEnumerator shootShotgunBullet(float waitForSec)
    {
        allowedToShoot = false;
        for (int i = 0; i < 3; i++)
        {
            //instantiates a bullet with spread factor (i * 0.3) 
            StartCoroutine(InstantiateBullet(turretGameObject.transform.position, findClosestPlayer(players, turretGameObject), false));
            yield return new WaitForSeconds(0.03f);
        }
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
        int amountOfFlameThrowerCorrections = 6;

        //tracks where the flamethrower needs to shoot
        for (int i = 0; i < amountOfFlameThrowerCorrections; i++)
        {
            //finds where the flamethrower should be shooting to hit the closest player
            Vector3 targetDir = (findClosestPlayer(players, turretGameObject).transform.position - turretGameObject.transform.position).normalized;
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
    IEnumerator InstantiateBullet(Vector3 thisObstaclePosition, GameObject closestPlayerPos, bool UsePrediction)
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
            if (UsePrediction == true)
            {
                targetDir = (CalculatePlayerPositionAccordingToSpeed(closestPlayerPos) - thisObstaclePosition).normalized;
            }
            else
            {
                targetDir = (closestPlayerPos.transform.position - thisObstaclePosition).normalized;
            }

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
    GameObject findClosestPlayer(GameObject[] players, GameObject thisObstacle)
    {
        Vector3 returnthing = new Vector3(0, 0, 0);
        GameObject playerSelected = players[0];
        float closestDistance = float.PositiveInfinity;

        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(thisObstacle.transform.position, p.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                returnthing = p.transform.position;
                playerSelected = p;
            }
        }
        return playerSelected;
    }

    IEnumerator RotateToAnAngle(GameObject gameObjectToRotate)
    {
        canRotateTurret = false;
        //finds the position of the closest player to this gameobject
        Vector3 targetDir = (CalculatePlayerPositionAccordingToSpeed(findClosestPlayer(players, turretGameObject)) - turretGameObject.transform.position).normalized;

        //finds the z angle for the turret to look at the player (z rotates the gameobject)
        float angleOfZ = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

        //turns the gameobject to the rotation in the quaternion thing(angleOfZ - 90 because bug of the rotation being +90)
        Quaternion newRotation = Quaternion.Euler(turretGameObject.transform.rotation.x, turretGameObject.transform.rotation.y, angleOfZ - 90);

        //actually rotates the gameobject in the parameter
        gameObjectToRotate.transform.rotation = newRotation;
        yield return new WaitForSeconds(0.03f);
        canRotateTurret = true;
    }

    Vector3 CalculatePlayerPositionAccordingToSpeed(GameObject targetedPlayer)
    {
        //finds the rigidbody from the target player
        Rigidbody2D targetPlayerRigidBody = targetedPlayer.GetComponent<Rigidbody2D>();

        //few vectors for velocity, currentposition and predicted position
        Vector3 currentPlayerPosition;
        Vector3 playerVelocity;
        Vector3 futurePosition = Vector3.zero;

        //floats for keeping track of distance, timebefore impact and bullet velocity
        float distanceFromTarget = 10;
        float timeBeforeImpact = 10;
        float bulletVelocity = 10;

        //predicts the amount of seconds in the future, higher is less accurate
        float amountOfSecondsInTheFuture = 2.1f;

        distanceFromTarget = Vector3.Distance(turretGameObject.transform.position, targetedPlayer.transform.position);
        //get targeted player positon 
        currentPlayerPosition = targetedPlayer.transform.position;
        //get targeted player velocity
        playerVelocity = targetPlayerRigidBody.linearVelocity;
        //bulletspeed variable 
        bulletVelocity = bulletSpeed;

        //time before impact is distance / speed of the bullet
        timeBeforeImpact = distanceFromTarget / bulletSpeed;
        //the future calculated position is the current position + playervelocity * amount of sec in the future * time before impact
        futurePosition = currentPlayerPosition + (playerVelocity * amountOfSecondsInTheFuture) * timeBeforeImpact;
        return futurePosition;
    }
    /// <summary>
    /// checks for new players every 2 seconds
    /// </summary>
    IEnumerator checkForPlayers()
    {
        canCheckForPlayers = false;
        players = GameObject.FindGameObjectsWithTag(Playertag);
        yield return new WaitForSeconds(2);
        canCheckForPlayers = true;
    }


    /// <summary>
    /// checks for closest player distance
    /// </summary>
    IEnumerator checkForDistance(float waitForSec)
    {
        //finds the distance between this turret and the closest player
        closestPlayerDistance = Vector3.Distance(turretGameObject.transform.position, findClosestPlayer(players, turretGameObject).transform.position);
        canCheckForPlayers = false;
        yield return new WaitForSeconds(waitForSec);
        canCheckForPlayers = true;
    }

}
