using System.Collections.Generic;
using UnityEngine;

public class DebuffFunctions : MonoBehaviour
{
    Player player;
    List<int> playerDebuffs = new List<int>();
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
            }
            if (playerDebuffs == player.debuffs)
            {
                print("skibidi");
                DebuffEffects();
            }
            
        }

        print("speed = " + player.GetMaxSpeed());
        print("accel = " + player.GetMaxAccel());
        print("rotation = " + player.GetRotationSpeed());

        print("own list count " + playerDebuffs.Count);
        print("other list count " + player.debuffs.Count);
    }


    void DebuffEffects()
    {
        print("test");
        for (int i = 0; i < playerDebuffs.Count; i++)
        {
            switch (playerDebuffs[i])
            {
                case 1: //speed nerf
                    player.AlterMaxSpeed(false,5);
                    break;
                case 2: //accel nerf
                    player.AlterMaxAccel(false,5);
                    break;
                case 3: //rotation nerf
                    player.AlterRotation(false, 5);
                    break;
            }
        }
    }
}
