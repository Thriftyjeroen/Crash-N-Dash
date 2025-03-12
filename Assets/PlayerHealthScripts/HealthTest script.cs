using Unity.VisualScripting;
using UnityEngine;

public class HealthTestscript : MonoBehaviour
{
    public GameObject thisObject;
    float moveSpeed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        thisObject.transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime);
    }
}
