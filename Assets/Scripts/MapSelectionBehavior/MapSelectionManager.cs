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
    int testNum = 0;
    int currentYPosition = 500;
    public int resolution = Screen.height;
    int itemTargetLocation = 0;
    int slideAmountPerSecond = Screen.height;

    Button votedMap;

    TMP_Text timerText;
    public float timeToVote = 10.0f;
    bool startTimer = false;
    bool removeButtons = false;
    bool mapsRemoved = false;
    void Start()
    {
        ParentCanvas = GetComponent<Canvas>();
        itemTargetLocation = resolution / 2;
        timerText = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AddNewMap == true && allMapSelectButtons.Count < 7)
        {
            AddMapButtonToCanvas();
            AddNewMap = false;
            if (allMapSelectButtons.Count > 2)
            {
                startTimer = true;
            }
        }

        if (timeToVote >= 0 && startTimer == true)
        {
            timeToVote -= Time.deltaTime;
            timerText.text = Mathf.Round(timeToVote).ToString();
        }
        else if (startTimer == true && timeToVote <= 0)
        {
            Destroy(timerText);
            print("select most voted map");
            if(mapsRemoved == false)
            {
                RemoveMapButtons();
            }
        }
    }

    /// <summary>
    /// retarded maar werkt 
    /// </summary>
    void AddMapButtonToCanvas()
    {
        if (allMapSelectButtons.Count <= 2)
        {
            Button newButton = Instantiate(MapSelectTemplate, ParentCanvas.transform);
            ChooseRandomAvailableMap(newButton);
            newButton.transform.position = new Vector3(currentYPosition, 1500, 0);
            currentYPosition += 500;
            StartCoroutine(transformButton(newButton, itemTargetLocation, -7));
            allMapSelectButtons.Add(newButton);
        }
    }

    void RemoveMapButtons()
    {
        votedMap = FindHighestVotedMap();
        foreach (Button mapButton in allMapSelectButtons)
        {
            if (mapButton != votedMap)
            {
                StartCoroutine(transformButton(mapButton, -1000, -7));
            }
        }
        mapsRemoved = true;
    }


    /// <summary>
    /// chooses a random map that is not yet in the available mapList
    /// </summary>
    /// 
    //still need to create a temp list that checks what maps are still available*
    void ChooseRandomAvailableMap(Button newButton)
    {
        GameObject emptyTextObject = newButton.transform.Find("Text").gameObject;
        TMP_Text[] allText = emptyTextObject.GetComponentsInChildren<TMP_Text>();
        TMP_Text nameOfMapText = allText[0];



        string randomMap = "feeling null rn";
        string test = AllAvailableMaps[Random.Range(0, AllAvailableMaps.Count)];
        if (!AlreadyInstantiatedMaps.Contains(test))
        {
            randomMap = test;
            nameOfMapText.text = randomMap;
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
    IEnumerator transformButton(Button currentButton, int targetLocation, int Speed)
    {
        print("executing ienumerator");
        while (currentButton.transform.position.y > targetLocation)
        {
            currentButton.transform.Translate(0, Speed, 0);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
        AddNewMap = true;
    }
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

}
