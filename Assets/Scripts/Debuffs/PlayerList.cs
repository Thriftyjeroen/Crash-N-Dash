using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();

    public void AddToList(GameObject user)
    {
        players.Add(user);
    }
}
