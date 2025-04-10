using System.Collections.Generic;
using UnityEngine;

public class DebuffFunctions : MonoBehaviour
{
    Player player;
    List<int> playerDebuffs = new List<int>();
    List<bool> debuffUsedManager = new List<bool>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        foreach (int id in player.debuffs)
        {
            playerDebuffs.Add(id);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerDebuffs != player.debuffs)
        {
            playerDebuffs.Clear();
            foreach (int id in player.debuffs)
            {
                playerDebuffs.Add(id);
                debuffUsedManager.Add(false);
            }

            DebuffEffects();


        }
    }


    void DebuffEffects()
    {
        for (int i = 0; i < playerDebuffs.Count; i++)
        {
            if (debuffUsedManager[i] != true)
            {
                switch (playerDebuffs[i])
                {
                    case 1: //speed nerf
                        if (player.GetMaxSpeed() > 5) player.AlterMaxSpeed(false, 5);
                        break;
                    case 2: //accel nerf
                        if (player.GetAccelInc() > 0.01f) player.AlterAccelInc(false, 0.01f);
                        break;
                    case 3: //rotation nerf
                        if (player.GetRotationSpeed() > 20) player.AlterRotation(false, 5);
                        break;
                }
                debuffUsedManager[i] = true;
            }  
        }
    }

    //bool CompareLists()
    //{
    //    bool isSame = true;

    //    for (int i = 0; i < playerDebuffs.Count; i++)
    //    {
    //        if (playerDebuffs[i] != player.debuffs[i] && isSame) isSame = false;
    //    }

    //    return isSame;
    //}
}
