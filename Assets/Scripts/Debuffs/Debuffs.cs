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
        Debuff accelnerf = new Debuff(2, "Accel nerf", "decreases acceleration","rare");
        debuffs.Add(accelnerf);
        Debuff rotationnerf = new Debuff(3, "Rotation nerf", "decreases rotation","epic");
        debuffs.Add(rotationnerf);
    }
}
