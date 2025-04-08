using UnityEngine;
using UnityEngine.UI;

public class SpawnCards : MonoBehaviour
{
    [SerializeField] GameObject debuffCardPrefab;
    GameObject cardParent;
    GiveDebuff gD;
    public int index;
    void Start()
    {
        cardParent = FindAnyObjectByType<HorizontalLayoutGroup>().gameObject;
    }

    private void Update()
    {
        gD = FindAnyObjectByType<GiveDebuff>();
        if (gD != null)
        {
            if (gD.cardPrefabs.Count != 0)
            {
                gD.cardPrefabs[index].GetComponent<Image>().color = Color.red;
                for (int i = 0; i < gD.cardPrefabs.Count; i++)
                {
                    if (i == index)
                    {
                        gD.cardPrefabs[i].GetComponent<Image>().color = Color.red;
                    }
                    else
                    {
                        gD.cardPrefabs[i].GetComponent<Image>().color = Color.gray;
                    }
                }
            }
        }
    }

    public void SpawnAllCards()
    {
        for (int i = 0; i < gD.DebuffChoices.Count; i++)
        {
            if(gD.cardPrefabs.Count != 3)
            {
                GameObject current = Instantiate(debuffCardPrefab, cardParent.transform);
                current.GetComponent<CardInfoFiller>().cardData = gD.DebuffChoices[i];
                gD.cardPrefabs.Add(current);
            }
        }
    }
}
