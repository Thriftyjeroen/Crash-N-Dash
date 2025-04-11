using NUnit.Framework;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float maxSpeed = 10f;
    private float accelInc = 0.1f;
    private float accelDec = 0.1f;
    private float minAccel = 0f;
    private float rotationSpeed = 125f;
    private float steerDelay = 0f;
    private bool invertControls = false;


    public List<int> debuffs = new List<int>();


    public float GetAccelInc() { return accelInc; }
    public float GetAccelDec() { return accelDec; }
    public float GetMinAccel() { return minAccel; }
    public float GetRotationSpeed() { return rotationSpeed; }
    public float GetMaxSpeed() { return maxSpeed; }
    public float GetSteerDelay() { return steerDelay; }
    public bool GetInvertControls() { return invertControls; }


    /// <summary>
    /// first parameter is for addition(true) or substraction(false)
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    public void AlterAccelInc(bool type,float amount)
    {
        if (type) accelInc += amount;
        else if (!type) accelInc -= amount;
    }
    /// <summary>
    /// first parameter is for addition(true) or substraction(false) ONLY CHANGE WITH EVEN NUMBERS
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    public void AlterMinAccel(bool type,float amount)
    {
        if (type) minAccel += amount;
        else if (!type) minAccel -= amount;
    }
    /// <summary>
    /// first parameter is for addition(true) or substraction(false)
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    public void AlterMaxSpeed(bool type, float amount)
    {
        if (type) maxSpeed += amount;
        else if (!type) maxSpeed -= amount;
    }
    /// <summary>
    /// first parameter is for addition(true) or substraction(false) FOR LONG LASTING CHANGES ONLY CHANGE IN INCREMENTS OF 5
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    public void AlterRotation(bool type, float amount)
    {
        if (type) rotationSpeed += amount;
        else if (!type) rotationSpeed -= amount;
    }
    public void AlterAccelDec(bool type , float amount)
    {
        if (type) accelDec += amount;
        
    }
    public void AlterSteerDelay(bool type,float amount)
    {
        if (type) steerDelay += amount;
        else if (!type) steerDelay -= amount;
    }
    /// <summary>
    /// just put true to set it to true and put false to set to false
    /// </summary>
    /// <param name="type"></param>
    public void AlterInvertControls(bool type)
    {
        invertControls = type;
    }

}
