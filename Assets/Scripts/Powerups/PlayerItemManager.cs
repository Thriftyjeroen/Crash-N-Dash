using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerItemManager : MonoBehaviour
{
    PowerupManager powerupManager;
    public PowerupType? currentItem;
    TMP_Text itemText;

    private void Start()
    {
        powerupManager = GetComponent<PowerupManager>();
        itemText = GetComponentInChildren<TMP_Text>();
    }


    public void OnItemUse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            powerupManager.Activate(currentItem, gameObject);
            currentItem = null;
            itemText.text = null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ItemBox>() == null) return;
        if (currentItem != null) return;

        currentItem = powerupManager.GetRandomItem();
        itemText.text = currentItem.ToString();
    }
}
