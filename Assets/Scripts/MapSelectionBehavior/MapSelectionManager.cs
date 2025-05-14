using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Canvas ParentCanvas;
    [SerializeField] Button MapSelectTemplate;
    [SerializeField] List<Button> allMapSelectButtons;
    [SerializeField] List<string> AllAvailableMaps;
    List<string> AlreadyInstantiatedMaps = new List<string>();
    public bool AddNewMap = false;
    public bool TestmapData = true;
    int currentYPosition = 500;
    public int resolution = Screen.height;
    int itemTargetLocation = 0;
    int slideAmountPerSecond = Screen.height;

    Button votedMap;

    TMP_Text timerText;
    public float timeToVote = 10.0f;
    bool startTimer = false;
    bool mapsRemoved = false;
    bool canOpenMap = true;
    void Start()
    {
        ParentCanvas = GetComponent<Canvas>();
        //target location is resolution/2 (middle of the screen)
        itemTargetLocation = resolution / 2;
        timerText = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //if a new map may be instantiated, it calls add new map button
        if (AddNewMap == true)
        {
            AddMapButtonToCanvas();
            AddNewMap = false;
        }

        //if the time to vote is higher than 0, the timer continues to count down
        if (timeToVote >= 0 && startTimer == true)
        {
            timeToVote -= Time.deltaTime;
            timerText.text = Mathf.Round(timeToVote).ToString();
        }
        //else if the timer is down to 0, all maps who were voted the least gets dragged out of screen
        else if (startTimer == true && timeToVote <= 0)
        {
            if (mapsRemoved == false)
            {
                //destroys the timer text since we dont need it anymore
                Destroy(timerText);
                print("select most voted map");
                RemoveLeastVotedMapButtons();
            }
        }
    }

    /// <summary>
    /// adds a new map button to the canvas 
    /// </summary>
    void AddMapButtonToCanvas()
    {
        //if all the current buttons on screen <=2 and all current buttons selected is less than all available maps (you cant have more maps on screen than there are in the map-pool)
        if (allMapSelectButtons.Count <= 2 && allMapSelectButtons.Count < AllAvailableMaps.Count)
        {
            //instantiates the new button on the canvas
            Button newButton = Instantiate(MapSelectTemplate, ParentCanvas.transform);
            //chooses a random map and adds it to the button
            ChooseRandomAvailableMap(newButton);

            //puts the button above the screen
            newButton.transform.position = new Vector3(currentYPosition, 1500, 0);

            //adjust the next spawn position for the next button
            currentYPosition += 500;

            //starts the sliding animation for the button
            StartCoroutine(transformButton(newButton, itemTargetLocation, -7, false));

            //this button gets added to the list for active maps
            allMapSelectButtons.Add(newButton);
        }
    }


    /// <summary>
    /// removes the map buttons who were votes the least
    /// </summary>
    void RemoveLeastVotedMapButtons()
    {
        //finds the most voted map and remembers it :O
        votedMap = FindHighestVotedMap();

        //checks for each button instantiated if it is not the voted map and starts the sliding out animation for it
        foreach (Button mapButton in allMapSelectButtons)
        {
            if (mapButton != votedMap)
            {
                StartCoroutine(transformButton(mapButton, -600, -7, true));
            }
            else if (mapButton == votedMap && canOpenMap == true)
            {
                StartCoroutine(activateMap(mapButton));
            }
        }
        mapsRemoved = true;
    }



    /// <summary>
    /// chooses a random map that has not been chosen yet
    /// </summary>
    void ChooseRandomAvailableMap(Button newButton)
    {
        //finds the children from the text gameobject (where the text is stored)
        GameObject emptyTextObject = newButton.transform.Find("Text").gameObject;
        //there are multiple tmp elements so i store it in an array 
        TMP_Text[] allText = emptyTextObject.GetComponentsInChildren<TMP_Text>();
        //the first tmp element is the text element so i store it in nameOfMapText
        TMP_Text nameOfMapText = allText[0];

        //if the mapname is null it stays like this
        string randomMap = "feeling null rn";
        //the chosenMapName string is a random map name from all available maps
        string chosenMapName = AllAvailableMaps[Random.Range(0, AllAvailableMaps.Count)];

        //here i check if the chosenMapName is already chosen, if it is the methods gets recalled
        if (!AlreadyInstantiatedMaps.Contains(chosenMapName))
        {
            //if the mapname is not yet chosen, it stores the new map in random map
            randomMap = chosenMapName;
            //sets the mapbutton text to randommap
            nameOfMapText.text = randomMap;
            //adds the map to already instantiated map
            AlreadyInstantiatedMaps.Add(randomMap);
        }
        else if (AlreadyInstantiatedMaps.Count < AllAvailableMaps.Count)
        {
            ChooseRandomAvailableMap(newButton);
        }
        else
        {
            print("we are gonna have a problem here, bud");
        }

    }

    /// <summary>
    /// ienumerator to transform the buttons up or down, used to slide the buttons in and out of screen
    /// </summary>
    IEnumerator transformButton(Button currentButton, int targetLocation, int Speed, bool deleteMapAfter)
    {
        print("executing ienumerator");
        while (currentButton.transform.position.y > targetLocation)
        {
            //transforms the y position by speed and waits for 0.01 second for the slide effect
            currentButton.transform.Translate(0, Speed, 0);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;

        if (deleteMapAfter == false)
        {
            //if all the buttons on screen is higher than 2, or there are no more maps available to choose, it starts the votingTimer
            if (allMapSelectButtons.Count > 2 || allMapSelectButtons.Count == AllAvailableMaps.Count)
            {
                startTimer = true;
            }
            //after the sliding animation is done, a new map can be instantiated
            AddNewMap = true;
        }
        else
        {
            allMapSelectButtons.Remove(currentButton);
            Destroy(currentButton.GetComponent<ButtonScript>());
        }

    }

    /// <summary>
    /// finds the button who has been voted the most
    /// </summary>
    Button FindHighestVotedMap()
    {
        Button returnButton = allMapSelectButtons[0];
        int highestVote = 0;
        foreach (Button b in allMapSelectButtons)
        {
            if (b.GetComponent<ButtonScript>().TellAmountOfVotes() > highestVote)
            {
                returnButton = b;
                highestVote = b.GetComponent<ButtonScript>().TellAmountOfVotes();
            }
        }
        return returnButton;
    }

    IEnumerator activateMap(Button votedButton)
    {
        canOpenMap = false;
        yield return new WaitForSeconds(3);
        votedButton.GetComponent<ButtonScript>().ActivateThisMap();

    }
}
