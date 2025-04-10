using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 rotateDirection;
    public Rigidbody2D rb;
    float accel = 0;
    float maxSpeed = 0;
    float minAccel = 0;
    float accelInc = 0.1f;
    float rotationSpeed = 0f;
    //float maxSpeed = 0;
    //float forceMult = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInfo();


        if (pushGas)
        {
            transform.Rotate(new Vector3(0, 0, 1), rotateDirection.x * rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {

        if (pushGas) rb.AddForce(transform.up * accel, ForceMode2D.Force);


        //AddToClamp();


        if (pushGas && accel < maxSpeed)
        {
            accel += accelInc;
        }
        else if (!pushGas && accel > minAccel)
        {
            accel -= accelInc;
        }
        if (accel < minAccel) accel = minAccel;

    }

    public void Rotation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rotateDirection = context.ReadValue<Vector2>();
        }
        else
        {
            rotateDirection = Vector2.zero;
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
    }



    //float LimitSpeed(float accel)
    //{
    //    float currentspeed = rb.linearVelocity.magnitude;
    //    print(MathCurrentSpeed(currentspeed));
    //    float forceToGive = 0;

    //    if (currentspeed > maxSpeed - (maxSpeed / 4))
    //    {
    //        forceMult = accel * maxSpeed - (currentspeed / maxSpeed);
    //        forceToGive = forceMult;
    //        print("dit runt blijkbaar gwn nooit :sob:");
    //    }
    //    else
    //    {
    //        forceToGive = accel;
    //    }

    //    return forceToGive;
    //}

    void GetInfo()
    {
        maxSpeed = gameObject.GetComponent<Player>().GetMaxSpeed();
        minAccel = gameObject.GetComponent<Player>().GetMinAccel();
        rotationSpeed = gameObject.GetComponent<Player>().GetRotationSpeed();
        accelInc = gameObject.GetComponent<Player>().GetAccelInc();
         //maxSpeed = gameObject.GetComponent<Player>().GetMaxSpeed();
    }


    //float mathOne = 0.5714276f;
    //float MathCurrentSpeed(float dumbNumber)
    //{
    //    float actualSpeed = dumbNumber / mathOne;
    //    return actualSpeed;
    //}
}
