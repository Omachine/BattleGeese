using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public enum ItemType
{
    weapon,
    hat,
    consumable
}

public class ItemPickup : MonoBehaviour
{
    private bool _isPlayerInRange = false;
    private GameObject _eToInteractUI;
    public GameObject eToInteractPrefab; // Reference to the prefab
    [SerializeField] public ScriptableObject _item;
    [SerializeField] public ItemType _itemType;

    private void Start()
    {
        if (eToInteractPrefab == null)
        {
            Debug.LogError("eToInteractPrefab is not assigned in the Inspector.");
            return;
        }

        // Instantiate the prefab half a unit higher and -1z from the object and set it to inactive
        _eToInteractUI = Instantiate(eToInteractPrefab, transform.position + Vector3.up * 0.02f + Vector3.back * 1f, Quaternion.identity, transform);
        _eToInteractUI.SetActive(false);
    }

    private void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            PickUpItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
            if (_eToInteractUI != null)
            {
                _eToInteractUI.SetActive(true);
                UpdateUIPosition();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
            if (_eToInteractUI != null)
            {
                _eToInteractUI.SetActive(false);
            }
        }
    }

    private void UpdateUIPosition()
    {
        // Set the position of the UI element in world space, rotate it 45 degrees on the x-axis, and move it 1 unit back
        _eToInteractUI.transform.position = transform.position + Vector3.up * 0.5f - Vector3.back * 1f;
        _eToInteractUI.transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    private void PickUpItem()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        switch (_itemType)
        {
            case ItemType.weapon:
                SwapWeapon(player);
                break;
            case ItemType.hat:
                player.PickUpHat(_item as HatSO);
                Destroy(gameObject);
                break;
            case ItemType.consumable:
                player.PickUpConsumable(_item as ConsumableDataSO);
                Destroy(gameObject);
                break;
            default:
                Debug.LogError("You don't have an item type");
                break;
        }
    }

    private void SwapWeapon(Player player)
    {
        // Pick up the new weapon
        WeaponDataSO newWeaponData = _item as WeaponDataSO;

        // Get the current weapon the player is holding
        WeaponDataSO currentWeaponData = player.Weapons[player.EquipedWeapon].Data;
        Debug.Log(currentWeaponData);
        if (currentWeaponData != null)
        {
            GameObject droppedWeapon = Instantiate(currentWeaponData.WeaponPrefab, transform.position, Quaternion.identity);
            ItemPickup itemPickup = droppedWeapon.GetComponent<ItemPickup>();
            itemPickup._item = currentWeaponData;
            itemPickup._itemType = ItemType.weapon;

            // Ensure the UI element is properly managed
            if (itemPickup._eToInteractUI != null)
            {
                Destroy(itemPickup._eToInteractUI);
                itemPickup._eToInteractUI = Instantiate(eToInteractPrefab, droppedWeapon.transform.position + Vector3.up * 0.02f + Vector3.back * 1f, Quaternion.identity, droppedWeapon.transform);
                itemPickup._eToInteractUI.SetActive(false);
            }
        }

        // Pick up the new weapon
        player.PickUpWeapon(newWeaponData);

        // Destroy the old item on the ground
        Destroy(gameObject);
    }
}

