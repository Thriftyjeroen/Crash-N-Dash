using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 rotateDirection;
    Rigidbody2D rb;
    float accel = 0;
    float maxSpeed = 10;
    float minSpeed = 0;
    float rotationSpeed = 1f;
    float maxForce = 50;
    float forceMult = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //ClampRotation();

        if (pushGas)
        {
            transform.Rotate(new Vector3(0, 0, 1), rotateDirection.x * rotationSpeed);
        }
    }

    private void FixedUpdate()
    {


        if (pushGas) rb.AddForce(transform.up * LimitSpeed(accel), ForceMode2D.Force);


        //AddToClamp();


        //print(accel);
        if (pushGas && accel < maxSpeed)
        {
            accel += 0.1f;
        }
        else if (!pushGas && accel > minSpeed)
        {
            accel -= 0.5f;
        }
        if (accel < minSpeed) accel = minSpeed;

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

    float LimitSpeed(float accel)
    {
        float currentspeed = rb.linearVelocity.magnitude;
        float forceToGive = 0;

        if (currentspeed > maxForce - (maxForce / 4))
        {
            forceMult = accel * maxForce - (currentspeed / maxForce);
            forceToGive = forceMult;

        }
        else
        {
            forceToGive = accel;
        }


        return forceToGive;
    }

    //float rotationClamp = 0.382f;
    //float singleClamp = 0.008f;


    //bool addLeft = false;
    //bool addRight = false;
    //void ClampRotation()
    //{
    //    if (transform.rotation.z > rotationClamp && transform.rotation.z > rotationClamp + 0.02f)
    //    {
    //        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationClamp / singleClamp));
    //    }
    //    else if (transform.rotation.z < -rotationClamp && transform.rotation.z < -rotationClamp - 0.02f)
    //    {
    //        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -rotationClamp / singleClamp));
    //    }



    //    if (rotateDirection == new Vector2(1, 0) && pushGas && transform.rotation.z > rotationClamp && transform.rotation.z > rotationClamp + 0.02f)
    //    {
    //        addLeft = true;
    //        addRight = false;
    //    }
    //    else if (rotateDirection == new Vector2(-1, 0))
    //    {
    //        addRight = true;
    //        addLeft = false;
    //    }
    //    else if (rotateDirection == new Vector2(0, 0))
    //    {
    //        addLeft = false;
    //        addRight = false;
    //    }

    //}

    //void AddToClamp()
    //{           
    //    if (addLeft)
    //    {   
    //        if (rotationClamp < 0)
    //        {
    //            rotationClamp -= singleClamp;
    //        }
    //        else
    //        {
    //            rotationClamp += singleClamp;
    //        }
    //    }
    //    else if (addRight)
    //    {
    //        //rotationClamp -= singleClamp;
    //    }

    //    if (rotationClamp > 1)
    //    {
    //        rotationClamp = -1;
    //    }

    //}
}
