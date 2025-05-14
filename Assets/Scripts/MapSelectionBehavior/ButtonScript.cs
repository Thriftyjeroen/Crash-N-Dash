using System.Collections;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnClick);

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
        switch (allText[0].text)
        {
            case "test1":
                print("test1 in switch statement");
                break;
            case "map1":
                print("open map 1");
                break;
            default:
                print("couldnt find a matching name, name input was" + allText[0].text);
                break;
        }
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
