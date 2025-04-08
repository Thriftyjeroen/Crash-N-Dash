using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ExplosiveCarBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float carSpeed = 2;    //change this to playercarspeed + small difference
    bool ExplodeCar = false;
    Vector3 targetPosition = Vector3.zero;
    public GameObject thisCar;
    Rigidbody2D rb;
    public float carAngle = 0;
    float carSteeringSmoothness = 4f;
    Vector3 deltaPositionCarAndPlayer = Vector3.zero;
    float carMaxExplosiveDamage = 50;

    void Start()
    {
        thisCar = this.gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LookAtPlayer(targetPosition);
        GoForward();
        if (GetComponentInChildren<MineManager>() == null) Destroy(thisCar);
    }

    ///begin tracking player

    public void GetTargetPosition(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
    }
    public bool CheckRangeFromPlayer(Vector3 thisObjectPosition, Vector3 playerPosition, GameObject[] players)
    {
        float distance = Vector3.Distance(thisObjectPosition, playerPosition);
        if (distance < 1)
        {
            MakeCarGoBoom(players);
            GoForward();
            return true;
        }
        return false;
    }
    void MakeCarGoBoom(GameObject[] players)
    {
        /*
        damagePlayers(GetCarsInRange(players));
        GameObject.Destroy(thisCar);
        */
    }
    List<GameObject> GetCarsInRange(GameObject[] players)
    {
        List<GameObject> returnList = new List<GameObject>();
        foreach (GameObject p in players)
        {
            if (Vector3.Distance(p.transform.position, thisCar.transform.position) <= 1.5f) returnList.Add(p);
        }
        return returnList;
    }

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

    ///end tracking player
    void LookAtPlayer(Vector3 targetPosition)
    {
        deltaPositionCarAndPlayer = targetPosition - thisCar.transform.position;
        float radians = Mathf.Atan2(deltaPositionCarAndPlayer.y, deltaPositionCarAndPlayer.x);
        carAngle = (radians * Mathf.Rad2Deg) - 90;
        Quaternion buh = Quaternion.Euler(0f, 0f, carAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, buh, Time.deltaTime * carSteeringSmoothness);

    }

    void GoForward()
    {
        thisCar.transform.Translate(Vector3.up * carSpeed * Time.deltaTime);
    }
}
