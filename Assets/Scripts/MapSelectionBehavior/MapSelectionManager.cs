using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectionManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Canvas ParentCanvas;
    [SerializeField] Button MapSelectTemplate;
    [SerializeField] List<Button> allMapSelectButtons;
    [SerializeField] List<string> AllAvailableMaps;
    public bool testbool = false;
    int currentYPosition = 500;
    void Start()
    {
        ParentCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (testbool == true && allMapSelectButtons.Count <= 3)
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
        Button newButton = Instantiate(MapSelectTemplate, ParentCanvas.transform);
        newButton.transform.position = new Vector3(currentYPosition, 475, 0);
        currentYPosition += 550;
        allMapSelectButtons.Add(newButton);
    }
}
