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
    TMP_Text[] allText;
    TMP_Text numberOfVotes;
    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnClick);

        GameObject emptyTextObject = thisButton.transform.Find("Text").gameObject;
        allText = emptyTextObject.GetComponentsInChildren<TMP_Text>();
        numberOfVotes = allText[1];
    }

    // Update is called once per frame
    void Update()
    {
        numberOfVotes.text = amountClickedOnButton.ToString();
    }

    void OnClick()
    {
        amountClickedOnButton++;
    }

    IEnumerator TemporaryBiggerButton()
    {
        yield return new WaitForSeconds(0.2f);
    }
}
