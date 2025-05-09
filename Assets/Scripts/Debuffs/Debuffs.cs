using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Debuffs : MonoBehaviour
{
    PlayerList pL;
    public List<Debuff> debuffs = new List<Debuff>();
    private void Start()
    {
        pL = FindAnyObjectByType<PlayerList>();
        InitializeDebuffs();
    }


    private void InitializeDebuffs()
    {
        //PLACEHOLDERS
        //NOT PLACEHOLDERS
        Debuff speednerf = new Debuff(1, "Flat tire", "decreases speed","common");
        debuffs.Add(speednerf);
        Debuff accelnerf = new Debuff(2, "Rusted Engine", "decreases acceleration","common");
        debuffs.Add(accelnerf);
        Debuff rotationnerf = new Debuff(3, "Rotation nerf", "decreases rotation","common");
        debuffs.Add(rotationnerf);
        Debuff keepsliding = new Debuff(4, "Slip N Slide", "car slides more", "rare");
        debuffs.Add(keepsliding);
        Debuff steerDelay = new Debuff(5, "Steer delay", "small delay before steering", "rare");
        debuffs.Add(steerDelay);
        Debuff invertControls = new Debuff(6, "invert controls", "inverts controls", "epic");
        debuffs.Add(invertControls);
        Debuff overSteering = new Debuff(7, "Oversteering", "way too much steering", "epic");
        debuffs.Add(overSteering);
        Debuff underSteering = new Debuff(8, "Understeering", "almost no steering", "epic");
        debuffs.Add(underSteering);
        Debuff randomAcceleration = new Debuff(9, "random acceleration", "random acceleration", "epic");
        debuffs.Add(randomAcceleration);
        Debuff heavyCar = new Debuff(10, "heavy car", "slower speed and rotation", "rare");
        debuffs.Add(heavyCar);
        Debuff ghostBrakes = new Debuff(11, "ghost brakes", "brakes randomly activate", "epic");
        debuffs.Add(ghostBrakes);
    }
}
