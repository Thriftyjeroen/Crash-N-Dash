using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int amountClickedOnButton = 0;
    Button thisButton;
    public TMP_Text[] allText;
    TMP_Text numberOfVotes;
    public string mapname;
    BoxCollider2D boxCollider;
    MapSelectionManager mapSelectionManager;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnClick);
        mapSelectionManager = FindFirstObjectByType<MapSelectionManager>();

        GameObject emptyTextObject = transform.Find("Text").gameObject;
        allText = emptyTextObject.GetComponentsInChildren<TMP_Text>();
        numberOfVotes = allText[1];
        mapname = allText[0].text;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfVotes.text = amountClickedOnButton.ToString();
    }

    void OnClick()
    {
        amountClickedOnButton++;
        TemporaryBiggerButton();
    }

    IEnumerator TemporaryBiggerButton()
    {
        thisButton.transform.localScale = thisButton.transform.localScale * 2;
        yield return new WaitForSeconds(0.2f);
    }

    public int TellAmountOfVotes()
    {
        int returnNum = amountClickedOnButton;
        return returnNum;
    }

    public void ActivateThisMap()
    {
        bool TESTBOOLACTIVATELOADSCENE = true;
        int mapNumber = 0; //0 for default test
        List<string> mapNames = mapSelectionManager.TellAllMapNames();

        for (int i = 0; i < mapNames.Count; i++)
        {
            string mn = mapNames[i];
            if (mn == allText[0].text)
            {
                mapNumber = i;
            }
        }

        if (mapNumber > 0 && TESTBOOLACTIVATELOADSCENE)
        {
            //         SceneManager.LoadScene(mapNames[mapNumber]);
        }
        print("mapname is" + mapNames[mapNumber] + mapNumber);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        amountClickedOnButton++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        amountClickedOnButton--;
    }
}
