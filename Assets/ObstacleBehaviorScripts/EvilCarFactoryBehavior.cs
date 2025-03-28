using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EvilCarFactoryBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject thisObstacle;
    [SerializeField] GameObject explosiveCarPrefab;
    GameObject[] players;
    string playerTag = "Player";
    void Start()
    {
        thisObstacle = this.gameObject;
        players = players = GameObject.FindGameObjectsWithTag(playerTag);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateExplosiveCar()
    {

    }

    Vector3 GetClosestPlayer()
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
