using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerItemManager : MonoBehaviour
{
    PowerupManager powerupManager;
    public PowerupType? currentItem;

    private void Start()
    {
        powerupManager = GetComponent<PowerupManager>();
    }


    public void OnItemUse(InputAction.CallbackContext context)
    {
        if (context.performed)
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
