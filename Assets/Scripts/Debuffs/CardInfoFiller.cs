using UnityEngine;
using TMPro;
public class CardInfoFiller : MonoBehaviour
{
    [SerializeField] TMP_Text name; 
    [SerializeField] TMP_Text description; 
    [SerializeField] TMP_Text rarity;
    
    public Debuff cardData;
    void Start()
    {
        name.text = cardData.name;
        description.text = cardData.description;
        rarity.text = cardData.rarity;
    }
}
