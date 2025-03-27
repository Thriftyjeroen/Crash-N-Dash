using UnityEngine;
using TMPro;

public class PlayerItemManager : MonoBehaviour
{
    PowerupManager powerupManager;
    public PowerupType? currentItem;
    TMP_Text debugText;

    private void Start()
    {
        powerupManager = GetComponent<PowerupManager>();
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.E)) currentItem = powerupManager.GetRandomItem();
            debugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();
            debugText.text = currentItem.ToString();
        #endif
        if (Input.GetKeyDown(KeyCode.Q))
        {
            powerupManager.Activate(currentItem, gameObject);
            currentItem = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ItemBox>() == null) return;
        if (currentItem != null) return;

        currentItem = powerupManager.GetRandomItem();
    }
}
