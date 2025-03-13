using Unity.Mathematics;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 rotationValue;
    Rigidbody2D rb;
    float accel = 0;
    float maxSpeed = 100;
    float minSpeed = 0;
    float rotationSpeed = .5f;
    [SerializeField] GameObject target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        //rb.linearVelocity = transform.forward * accel;
        transform.Rotate(new Vector3(0,0,1),rotationValue.x * rotationSpeed);
    }

    private void FixedUpdate()
    {
        if (pushGas) rb.AddForce(transform.up * accel, ForceMode2D.Force);
        print(accel);
        if (pushGas && accel < maxSpeed)
        {
            accel += 0.5f;
        }
        else if (!pushGas && accel > minSpeed)
        {
            accel -= 0.5f;
        }

    }

    public void Rotation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rotationValue = context.ReadValue<Vector2>();      
        }
        else
        {
            rotationValue = Vector2.zero;
        }
    }

    bool pushGas = false;
    public void GoForward(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pushGas = true;
        }
        else
        {
            pushGas = false;
        }
        //while button is being pressed acceleration should go up
        //when button is not being pressed acceleration should go down
    }
}
