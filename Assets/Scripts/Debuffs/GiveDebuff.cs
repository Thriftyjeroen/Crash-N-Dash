using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GiveDebuff : MonoBehaviour
{
    PlayerList pL;

    Debuffs db;
    [SerializeField] GameObject debuffCardPrefab;
    GameObject cardParent;
    SpawnCards spawn;
    void Start()
    {
        pL = FindAnyObjectByType<PlayerList>();
        db = FindAnyObjectByType<Debuffs>();
        spawn = FindAnyObjectByType<SpawnCards>();

        cardParent = FindAnyObjectByType<HorizontalLayoutGroup>().gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GiveDebuffs(); //CHANGE PARAMETERS
        }

        
    }


    List<string> rarities = new List<string>();
    public List<Debuff> DebuffChoices = new List<Debuff>();
    public List<GameObject> cardPrefabs = new List<GameObject>();

    int cardIndex = 0;
    int chosenDebuffID = 0;
    void CardGiveMethod()
    {

        System.Random r = new System.Random();
        rarities.Clear();
        //grab 3 random rarities
        for (int i = 0; i < 3; i++)
        {
            int randomInt = r.Next(0, 101);
            if (randomInt > 0 && randomInt <= 75) rarities.Add("common");
            else if (randomInt > 75 && randomInt <= 95) rarities.Add("rare");
            else if (randomInt > 95) rarities.Add("epic");
        }


        //grab cards based on those rarities
        int counter = 0;
        foreach (string s in rarities)
        {
            bool found = false;
            do
            {
                int randomNum = r.Next(0, db.debuffs.Count);
                if (db.debuffs[randomNum].rarity == rarities[counter])
                {
                    DebuffChoices.Add(db.debuffs[randomNum]);
                    found = true;
                }
            } while (!found);
            counter++;
        }



        //show random cards on screen

        for (int i = 0; i < DebuffChoices.Count; i++)
        {
            spawn.SpawnAllCards();
        }
        //let player choose debuff

        //reset everything

        //return chosen debuff


    }

    public void GiveDebuffs()
    {
        CardGiveMethod();
        
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
        if (context.performed && cardPrefabs.Count != 0)
        {             
            chosenDebuffID = cardPrefabs[cardIndex].GetComponent<CardInfoFiller>().cardData.id;
            foreach (GameObject player in pL.players)
            {
                player.GetComponent<Player>().debuffs.Add(chosenDebuffID);
            }
            Invoke("ResetAll",1f);
            
        }

    }


    private void ResetAll()
    {
        rarities.Clear();
        DebuffChoices.Clear();
        foreach (var card in cardPrefabs)
        {
            Destroy(card.gameObject);
        }
        cardPrefabs.Clear();
        cardIndex = 0;
        chosenDebuffID = 0;
    }

}
