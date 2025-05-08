using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 rotateDirection;
    public Rigidbody2D rb;
    float accel = 0;
    float maxSpeed = 0;
    float minAccel = 0;
    float accelInc = 0;
    float accelDec = 0;
    float rotationSpeed = 0;
    float steerDelay = 0;
    bool invertControls = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInfo();

        if (MathCurrentSpeed(rb.linearVelocity.magnitude) > 0.5f || pushGas)
        {
            if (invertControls) transform.Rotate(new Vector3(0, 0, 1), -rotateDirection.x * rotationSpeed * Time.deltaTime);
            else transform.Rotate(new Vector3(0, 0, 1), rotateDirection.x * rotationSpeed * Time.deltaTime);
        }


        SpeedLimitCheck(MathCurrentSpeed(rb.linearVelocity.magnitude));




        
    }

    private void FixedUpdate()
    {

        if (pushGas) rb.AddForce(transform.up * accel, ForceMode2D.Force);

        if (pushBrake)
        {
            rb.AddForce(-transform.up * MathCurrentSpeed(rb.linearVelocity.magnitude), ForceMode2D.Force);
        }


        if (pushGas && accel < maxSpeed)
        {
            accel += accelInc;
        }
        else if (!pushGas && accel > minAccel)
        {
            accel -= accelDec;
        }
        if (accel < minAccel) accel = minAccel;

    }

    public void Rotation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(ChangeRotateValue(context));
        }
        else
        {
            rotateDirection = Vector2.zero;
        }
    }

    IEnumerator ChangeRotateValue(InputAction.CallbackContext context)
    {
        yield return new WaitForSeconds(steerDelay);
        rotateDirection = context.ReadValue<Vector2>();
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
    bool pushBrake = false;
    public void Brake(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pushBrake = true;
        }
        else
        {
            pushBrake = false;
        }
    }



    void GetInfo()
    {
        maxSpeed = gameObject.GetComponent<Player>().GetMaxSpeed();
        minAccel = gameObject.GetComponent<Player>().GetMinAccel();
        rotationSpeed = gameObject.GetComponent<Player>().GetRotationSpeed();
        accelInc = gameObject.GetComponent<Player>().GetAccelInc();
        accelDec = gameObject.GetComponent<Player>().GetAccelDec();
        steerDelay = gameObject.GetComponent<Player>().GetSteerDelay();
        invertControls = gameObject.GetComponent<Player>().GetInvertControls();
    }


    float mathOne = 0.5714276f;
    float MathCurrentSpeed(float dumbNumber)
    {
        float actualSpeed = dumbNumber / mathOne;
        return actualSpeed;
    }

    void SpeedLimitCheck(float speed)
    {
        if (speed > maxSpeed + 1)
        {
            rb.AddForce(-transform.up * (speed - maxSpeed), ForceMode2D.Force);
        }
    }
}
