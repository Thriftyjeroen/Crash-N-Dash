using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EvilCarFactoryBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject thisObstacle;
    [SerializeField] GameObject explosiveCarPrefab;
    GameObject[] players;
    string playerTag = "Player";

    bool canUpdatePlayerLocation = true;
    bool carDead = false;

    GameObject activeCar;
    ExplosiveCarBehavior activeCarBehavior;
    GameObject targetObject;

    void Start()
    {
        thisObstacle = gameObject;
        players = players = GameObject.FindGameObjectsWithTag(playerTag);
    }

    // Update is called once per frame
    void Update()
    {
        if (carDead == false && canUpdatePlayerLocation == true)
        {
            if (targetObject != null && activeCar != null)
            {
                StartCoroutine(updateTargetLocation(targetObject.transform.position, 0.5f));
                activeCar.GetComponent<ExplosiveCarBehavior>().CheckRangeFromPlayer(activeCar.transform.position, targetObject.transform.position, players);
            }
            else
            {
                startChase();
            }
        }
    }
    public void startChase()
    {
        targetObject = GetClosestPlayer();
        CreateExplosiveCar(gameObject.transform.position);
    }


    IEnumerator updateTargetLocation(Vector3 position, float seconds)
    {
        canUpdatePlayerLocation = false;
        activeCar.GetComponent<ExplosiveCarBehavior>().GetTargetPosition(position);
        yield return new WaitForSeconds(seconds);
        canUpdatePlayerLocation = true;
    }
    void CreateExplosiveCar(Vector3 position)
    {
        activeCar = Instantiate(explosiveCarPrefab, position, Quaternion.identity);
    }

    GameObject GetClosestPlayer()
    {
        GameObject closestObject = players[0];
        float closestDistance = float.PositiveInfinity;

        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(thisObstacle.transform.position, p.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = p;
            }
        }
        return closestObject;
    }
}
