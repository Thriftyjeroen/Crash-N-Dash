using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebuffInputs : MonoBehaviour
{
    SpawnCards spawn;
    GiveDebuff gD;
    private void Start()
    {
        spawn = FindAnyObjectByType<SpawnCards>();
        gD = FindAnyObjectByType<GiveDebuff>();
    }

    public void IndexUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (spawn.index == 2)
            {
                spawn.index = 0;
            }
            else
            {
                spawn.index++;
            }
        }
    }

    public void IndexDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (spawn.index == 0)
            {
                spawn.index = 2;
            }
            else
            {
                spawn.index--;
            }
        }
    }

    public void Select(InputAction.CallbackContext context)
    {
        gD.confirmed = true;      
    }


}
