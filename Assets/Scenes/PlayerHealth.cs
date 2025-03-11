using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    GameObject playerObject;
    float playerHealth;
    float maxPossibleHealth = 100.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = this.transform.parent.GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RemovePlayerHealth(float AmountToBeRemoved)
    {
        playerHealth += AmountToBeRemoved;
    }

    void AddPlayerHealth(float AmountToBeAdded)
    {
        playerHealth += AmountToBeAdded;
    }

    private void ResetPlayerHealth()
    {
        playerHealth = maxPossibleHealth;
    }


}
