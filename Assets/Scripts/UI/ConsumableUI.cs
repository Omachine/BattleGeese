using UnityEngine;
using UnityEngine.UI;

public class ConsumableInventoryUI : MonoBehaviour
{
    [SerializeField] private Image consumableSlot; // UI slot for the consumable

    private void Awake()
    {
        if (consumableSlot == null)
        {
            Debug.LogError("Consumable slot is not assigned.");
        }
    }

    public void UpdateConsumableSlot(Sprite consumableSprite)
    {
        if (consumableSlot != null)
        {
            consumableSlot.sprite = consumableSprite;
            consumableSlot.enabled = consumableSprite != null; // Enable or disable the slot based on whether there is a sprite
        }
        else
        {
            Debug.LogError("Image component is missing on the consumable slot");
        }
    }
}
