using NUnit.Framework;
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
    public bool testbool = false;
    public bool TestmapData = true;
    int testNum = 0;
    int currentYPosition = 500;
    public int resolution = Screen.height;
    void Start()
    {
        ParentCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (testbool == true && allMapSelectButtons.Count < 7)
        {
            AddMapButtonToCanvas();
            testbool = false;
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
            newButton.transform.position = new Vector3(currentYPosition, (resolution / 2), 0);
            currentYPosition += 500;
            allMapSelectButtons.Add(newButton);
        }
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

}
