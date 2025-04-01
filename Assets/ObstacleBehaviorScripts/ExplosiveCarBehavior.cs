using System.Collections.Generic;
using UnityEngine;

public class ExplosiveCarBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float carSpeed = 2;    //change this to playercarspeed + small difference
    bool ExplodeCar = false;
    Vector3 targetPosition = Vector3.zero;
    GameObject thisCar;
    Rigidbody2D rb;

    void Start()
    {
        thisCar = GetComponent<GameObject>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LookAtPlayer(targetPosition);
    }

    ///begin tracking player

    public void GetTargetPosition(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
    }
    public void CheckRangeFromPlayer(Vector3 thisObjectPosition, Vector3 playerPosition, GameObject[] players)
    {
        float distance = Vector3.Distance(thisObjectPosition, playerPosition);
        if (distance < 1)
        {
            MakeCarGoBoom(players);
        }
    }
    void MakeCarGoBoom(GameObject[] players)
    {
        GetCarsInRange(players);
        //animatie
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

    ///end tracking player
    void LookAtPlayer(Vector3 targetPosition)
    {
        float angleOfZ = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg;

        Quaternion newRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angleOfZ);
        transform.rotation = newRotation;
    }
}
