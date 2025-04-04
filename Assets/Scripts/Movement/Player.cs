using NUnit.Framework;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float maxAccel = 10f;
    private float minAccel = 0f;
    private float rotationSpeed = 75f;
    private float maxSpeed = 50f;


    public List<int> debuffs = new List<int>();


    public float GetMaxAccel() { return maxAccel; }
    public float GetMinAccel() { return minAccel; }
    public float GetRotationSpeed() { return rotationSpeed; }
    public float GetMaxSpeed() { return maxSpeed; }


    /// <summary>
    /// first parameter is for addition(true) or substraction(false) 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    public void AlterMaxAccel(bool type,float amount)
    {
        if (type) maxAccel += amount;
        else if (!type) maxAccel -= amount;
    }
    /// <summary>
    /// first parameter is for addition(true) or substraction(false) 
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
    /// first parameter is for addition(true) or substraction(false) 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="amount"></param>
    public void AlterRotation(bool type, float amount)
    {
        if (type) rotationSpeed += amount;
        else if (!type) rotationSpeed -= amount;
    }


}
