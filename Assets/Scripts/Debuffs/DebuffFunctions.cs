using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (CompareLists())
        {
            playerDebuffs.Clear();
            foreach (int id in player.debuffs)
            {
                playerDebuffs.Add(id);
                debuffUsedManager.Add(false);
            }

            DebuffEffects();

        }
        print(player.GetRotationSpeed());
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
                        if (player.GetMaxSpeed() > 2) player.AlterMaxSpeed(false, 1);
                        break;
                    case 2: //accel nerf
                        if (player.GetAccelInc() > 0.01f) player.AlterAccelInc(false, 0.01f);
                        break;
                    case 3: //rotation nerf
                        if (player.GetRotationSpeed() > 20) player.AlterRotation(false, 5);
                        break;
                    case 4: //slip n slide
                        if (player.GetAccelDec() > 0.01f) player.AlterAccelDec(false, 0.01f);
                        break;
                    case 5: //steer delay
                        player.AlterSteerDelay(true, 0.15f);
                        break;
                    case 6: // invert controls
                        player.AlterInvertControls(true);
                        break;
                    case 7:
                        player.AlterRotation(true,100);
                        break;
                }
                debuffUsedManager[i] = true;
            }  
        }
    }

    bool CompareLists()
    {
        bool isSame = true;

        for (int i = 0; i < playerDebuffs.Count; i++)
        {
            if (playerDebuffs[i] != player.debuffs[i] && isSame) isSame = false;
        }

        return isSame;
    }
}
