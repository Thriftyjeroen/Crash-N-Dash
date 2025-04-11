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
        Debuff speednerf = new Debuff(1, "Speed nerf", "decreases speed","common");
        debuffs.Add(speednerf);
        Debuff accelnerf = new Debuff(2, "Accel nerf", "decreases acceleration","common");
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
    }
}
