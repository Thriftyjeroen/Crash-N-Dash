using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCarBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //change this to playercarspeed + small difference
    float carSpeed = 2;
    //current angle of car, public for debug reasons
    public float carAngle = 0;
    //damage the car can deal, can set it to random but im busy rn
    float carMaxExplosiveDamage = 50;

    bool canUpdatePlayerLocation = true;
    float carSteeringSmoothness = 4f;

    string playerTag = "Player";
    Vector3 deltaPositionCarAndPlayer = Vector3.zero;
    public Vector3 closestFoundPlayer = Vector3.zero;

    public GameObject target;
    public GameObject thisCar;
    GameObject[] Activeplayers;

    //turns car behavior on or off
    bool CarBehaviorSwitch = true;


    void Start()
    {
        thisCar = gameObject;
        //get the closest player near the car
        target = GetClosestPlayer();
        closestFoundPlayer = target.transform.position;
    }

    private void Update()
    {
        //if the car behavior is enabled
        if (CarBehaviorSwitch == true)
        {
            //if the car can update its target position
            if (canUpdatePlayerLocation == true)
            {
                target = GetClosestPlayer();
                StartCoroutine(updateTargetLocation(target.transform.position, 0.7f));
            }
            //rotate towards closest player
            LookAtPlayer(closestFoundPlayer);
            //let car move forward
            GoForward();
            if (GetComponentInChildren<MineManager>() == null) Destroy(thisCar);
        }
    }

    /// <summary>
    /// get the all players from evil car factory script
    /// </summary>
    public void GivePlayersGameObjects(GameObject[] players)
    {
        Activeplayers = players;
    }

    /// <summary>
    /// finds the closest player from this gameObject (explosivecar)
    /// </summary>
    GameObject GetClosestPlayer()
    {
        GameObject closestObject = Activeplayers[0];
        float closestDistance = float.PositiveInfinity;

        foreach (GameObject p in Activeplayers)
        {
            float distance = Vector3.Distance(gameObject.transform.position, p.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = p;
            }
        }
        return closestObject;
    }


    /// <summary>
    /// method rotates the explosive car to the target location
    /// </summary>
    void LookAtPlayer(Vector3 targetPosition)
    {
        deltaPositionCarAndPlayer = targetPosition - thisCar.transform.position;
        float radians = Mathf.Atan2(deltaPositionCarAndPlayer.y, deltaPositionCarAndPlayer.x);
        carAngle = (radians * Mathf.Rad2Deg) - 90;
        Quaternion buh = Quaternion.Euler(0f, 0f, carAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, buh, Time.deltaTime * carSteeringSmoothness);

    }

    /// <summary>
    /// moves the explosive car forward
    /// </summary>
    void GoForward()
    {
        thisCar.transform.Translate(Vector3.up * carSpeed * Time.deltaTime);
    }


    /// <summary>
    /// updates the location of the target
    /// </summary>
    IEnumerator updateTargetLocation(Vector3 position, float seconds)
    {
        print("updating location");
        canUpdatePlayerLocation = false;
        closestFoundPlayer = position;
        yield return new WaitForSeconds(seconds);
        canUpdatePlayerLocation = true;
    }

    /// <summary>
    /// get cars in range of the explosive car
    /// </summary>
    List<GameObject> GetCarsInRange(GameObject[] players)
    {
        List<GameObject> returnList = new List<GameObject>();
        foreach (GameObject p in players)
        {
            if (Vector3.Distance(p.transform.position, thisCar.transform.position) <= 1.5f) returnList.Add(p);
        }
        return returnList;
    }

    /// <summary>
    /// explodes the car and calls damage players
    /// </summary>
    void ExplodeCar()
    {
        damagePlayers(GetCarsInRange(Activeplayers));
        //voeg hier nog effect toe
        Destroy(gameObject);
    }

    /// <summary>
    /// removes health from the players in range
    /// </summary>
    void damagePlayers(List<GameObject> players)
    {
        foreach (GameObject p in players)
        {
            try
            {
                p.GetComponent<PlayerHealth>().RemovePlayerHealth(carMaxExplosiveDamage);
            }
            catch
            {
                print("kan geen playerhealth vinden");
            }
        }
    }


    /// <summary>
    /// if the car collides with something it sets off the explosion
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            ExplodeCar();
        }
    }
}
