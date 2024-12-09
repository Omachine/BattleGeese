using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image primaryWeaponSlot; // UI slot for the primary weapon
    [SerializeField] private Image secondaryWeaponSlot; // UI slot for the secondary weapon
    [SerializeField] private Image background1; // Background for the primary weapon
    [SerializeField] private Image background2; // Background for the secondary weapon
    [SerializeField] private Text primaryWeaponAmmoText; // Text for the primary weapon ammo
    [SerializeField] private Text secondaryWeaponAmmoText; // Text for the secondary weapon ammo

    [SerializeField] private Color selectedColor = new Color(0.047f, 0.388f, 0.569f); // Default #0C6291
    [SerializeField] private Color unselectedColor = new Color(0.678f, 0.306f, 0.169f); // Default #AD4E2B

    private bool isPrimaryWeaponSelected = true;

    private void Awake()
    {
        if (primaryWeaponSlot == null)
        {
            Debug.LogError("Primary weapon slot is not assigned.");
        }

        if (secondaryWeaponSlot == null)
        {
            Debug.LogError("Secondary weapon slot is not assigned.");
        }

        if (background1 == null)
        {
            Debug.LogError("Background1 is not assigned.");
        }

        if (background2 == null)
        {
            Debug.LogError("Background2 is not assigned.");
        }

        if (primaryWeaponAmmoText == null)
        {
            Debug.LogError("Primary weapon ammo text is not assigned.");
        }

        if (secondaryWeaponAmmoText == null)
        {
            Debug.LogError("Secondary weapon ammo text is not assigned.");
        }

        // Set initial colors
        UpdateBackgroundColors(0);
    }

    public void UpdateBackgroundColors(int selectedWeaponIndex)
    {
        if (selectedWeaponIndex == 0)
        {
            background1.color = selectedColor;
            background2.color = unselectedColor;
        }
        else
        {
            background1.color = unselectedColor;
            background2.color = selectedColor;
        }
    }

    public void UpdateWeaponSlot(int slotIndex, Sprite weaponSprite, Weapon weapon)
    {
        Image slot = null;
        Text ammoText = null;

        if (slotIndex == 0)
        {
            slot = primaryWeaponSlot;
            ammoText = primaryWeaponAmmoText;
        }
        else if (slotIndex == 1)
        {
            slot = secondaryWeaponSlot;
            ammoText = secondaryWeaponAmmoText;
        }
        else
        {
            Debug.LogError("Invalid slot index");
            return;
        }

        if (slot != null)
        {
            slot.sprite = weaponSprite;
            slot.enabled = weaponSprite != null; // Enable or disable the slot based on whether there is a sprite
        }
        else
        {
            Debug.LogError("Image component is missing on the weapon slot");
        }

        if (ammoText != null && weapon != null)
        {
            Hammo hammo = weapon.GetComponent<Hammo>();
            if (hammo != null)
            {
                ammoText.text = hammo.GetCurrentHammo().ToString();
                ammoText.enabled = true;
            }
            else
            {
                ammoText.enabled = false;
            }
        }
    }
}
