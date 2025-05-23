using System.Collections.Generic;
using TMPro;
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
    LeaderboardManager leaderboardManager;
    [SerializeField] TMP_Text text;

    public bool confirmed = false;


    void Start()
    {
        pL = FindAnyObjectByType<PlayerList>();
        db = FindAnyObjectByType<Debuffs>();
        spawn = FindAnyObjectByType<SpawnCards>();
        leaderboardManager = FindAnyObjectByType<LeaderboardManager>();

        cardParent = FindAnyObjectByType<HorizontalLayoutGroup>().gameObject;
        text.gameObject.SetActive(false);
    }
    int counter = 0;
    int playerID = 1;
    bool debuffRunning = false;
    void Update()
    {
        print(playerID);
        if (Input.GetKeyDown(KeyCode.P))
        {
            debuffRunning = true;
            text.gameObject.SetActive(true);
        }

        if (debuffRunning)
        {
            if (leaderboardManager.playerLeaderboard.Count != 0 && playerID < leaderboardManager.playerLeaderboard.Count)
            {
                text.color = leaderboardManager.playerLeaderboard[playerID].GetComponent<SpriteRenderer>().color;
                CardGiveMethod();
                DeactivateInputs(leaderboardManager.playerLeaderboard[playerID]);

                if (confirmed && cardPrefabs.Count != 0 && !inHere) PlayerIDUp();

            }
            else
            {
                playerID = 1;
                debuffRunning = false;
                text.gameObject.SetActive(false);
            }
        }

        //een int hebben voor welke speler aan de beurt is en dan via daar de method steeds runnen
        //int omhoog doen zodra enter gedrukt is
        //dankjewel kenneth

        //foreach player on leaderboard
        //for (i = 0; i < leaderboardManager.playerLeaderboard.Count; i++)
        //{

        //    //skip first index
        //    if (i == 0)
        //    {
        //        continue;
        //    }


        //    CardGiveMethod();



        //}
        //CHANGE PARAMETERS


        if (confirmed && cardPrefabs.Count != 0 && counter < 1)
        {
            chosenDebuffID = cardPrefabs[cardIndex].GetComponent<CardInfoFiller>().cardData.id;


            //foreach player above the current player in the leaderboard
            for (int j = 0; j < playerID; j++)
            {
                if (leaderboardManager.playerLeaderboard[j].GetComponent<SpriteRenderer>().color != text.color) leaderboardManager.playerLeaderboard[j].GetComponent<Player>().debuffs.Add(chosenDebuffID);
                //give the debuff
            }

            //foreach (GameObject player in pL.players)
            //{
            //    player.GetComponent<Player>().debuffs.Add(chosenDebuffID);
            //}
            Invoke("ResetAll", 1f);
            counter++;
        }

    }

    bool inHere = false;
    void PlayerIDUp()
    {
        inHere = true;
        playerID++;
        Invoke("InHereFalse", 1);
    }

    void InHereFalse()
    {
        inHere = false;
    }

    void DeactivateInputs(Player current)
    {
        foreach (Player player in leaderboardManager.playerLeaderboard)
        {
            if (player != current)
            {
                player.GetComponent<PlayerInput>().DeactivateInput();
            }
        }
    }

    void ActivateInputs()
    {
        foreach(Player player in leaderboardManager.playerLeaderboard)
        {
            player.GetComponent<PlayerInput>().ActivateInput();
        }
    }

    List<string> rarities = new List<string>();
    public List<Debuff> DebuffChoices = new List<Debuff>();
    public List<GameObject> cardPrefabs = new List<GameObject>();

    int cardIndex = 0;
    int chosenDebuffID = 0;
    public void CardGiveMethod()
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


        // hard code what debuff shows for testing purposes DELETE WHEN DONE TESTING
        //DebuffChoices[0] = db.debuffs[0];


        //show random cards on screen

        for (int i = 0; i < DebuffChoices.Count; i++)
        {
            spawn.SpawnAllCards();
        }
        //let player choose debuff

        //reset everything

        //return chosen debuff
    }


    //private IEnumerator WaitForKeyPress(KeyCode key)
    //{
    //    while (!Input.GetKeyDown(key))
    //    {
    //        yield return null;
    //    }
    //}

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
        confirmed = false;
        counter = 0;
        ActivateInputs();
    }

}
