using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine;

    public PlayerInputHandler Input { get; private set; }

    [field: SerializeField] public PlayerSO Data { get; private set; }
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private AudioClip _honkSound;

    public Rigidbody RigidBody { get; private set; }
    public CapsuleCollider Collider { get; private set; }

    [HideInInspector] public Weapon[] Weapons = new Weapon[2];
    [HideInInspector] public Consumable CurrentConsumable; // Store the current consumable
    [HideInInspector] public int EquipedWeapon;
    [HideInInspector] public float AttackSpeed = 1f;

    public Animator Animator { get; private set; }

    [HideInInspector] public SpriteRenderer SpriteRenderer;

    private CinemachineImpulseSource _impulseSource;
    private AnimationEventHandler _eventHandler;

    private GameObject deadImage;
    private InventoryUI inventoryUI; // Reference to the InventoryUI script for weapons
    private ConsumableInventoryUI consumableInventoryUI; // Reference to the InventoryUI script for consumables

    public bool IsDashing;

    private void Awake()
    {
        GameObject hud = GameObject.FindGameObjectWithTag("HUD");

        inventoryUI = hud.transform.Find("InventoryWeapons").GetComponent<InventoryUI>();
        consumableInventoryUI = hud.transform.Find("InventoryConsumable").GetComponent<ConsumableInventoryUI>();
        deadImage = hud.transform.Find("dead").gameObject;

        Input = GetComponent<PlayerInputHandler>();

        RigidBody = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();

        SpriteRenderer = transform.Find("Sprite").gameObject.GetComponent<SpriteRenderer>();
        _eventHandler = SpriteRenderer.GetComponent<AnimationEventHandler>();
        Animator = SpriteRenderer.GetComponent<Animator>();

        Weapons[0] = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        Weapons[1] = transform.Find("SecondaryWeapon").GetComponent<Weapon>();

        StateMachine = new PlayerStateMachine(this);

        _impulseSource = GetComponent<CinemachineImpulseSource>();

        GetComponent<HealthComponent>().OnDamageReceived += HandleDamage;
        GetComponent<HealthComponent>().OnDeath += () =>
        {
            StartCoroutine(HandleDeath());
        };

        _eventHandler.OnStep += Step;
        Input.PlayerActions.Honk.started += Honk;
        Input.PlayerActions.UseConsumable.started += UseConsumable; // Add input handling for using consumable
        Input.PlayerActions.Interact.started += Interact; // Add input handling for interacting
    }

    private void Start()
    {
        Weapons[EquipedWeapon].Equip();
        UpdateInventoryUI(); // Update the inventory UI at the start
    }

    private void Update()
    {
        StateMachine.HandleInput();
        StateMachine.Update();
        UpdateInventoryUI();
    }

    private void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }

    private void HandleDamage(float amount, DamageType type)
    {
        DamageFeedback();
        if (type == DamageType.Stun)
        {
            StateMachine.ChangeState(StateMachine.StunnedState);
        }
    }

    private void DamageFeedback()
    {
        CameraShakeManager.instance.CameraShake(_impulseSource);
        // DamageOverlay.instance.CallDamageFlash();
    }

    private IEnumerator HandleDeath()
    {
        Debug.Log("Player died lmao");
        deadImage.SetActive(true);
        gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Honk(InputAction.CallbackContext context)
    {
        Animator.SetTrigger("honk");

        SoundManager.instance.PlayClip(_honkSound, transform, 0.5f);
    }

    private void Step()
    {
        SoundManager.instance.PlayClipWithRandomPitch(_stepSound, transform, 0.4f, 0.9f, 1.1f);
    }

    public void LoadWeapons(WeaponDataSO newWeapon1, WeaponDataSO newWeapon2)
    {
        Weapons[0].Data = newWeapon1;
        Weapons[0]._generator._data = newWeapon1;
        Weapons[1].Data = newWeapon2;
        Weapons[1]._generator._data = newWeapon2;
        Weapons[0].GenerateWeapon();
        Weapons[1].GenerateWeapon();
        EquipedWeapon = 0;
        Weapons[1].Unequip();
        Weapons[0].Equip();

        StateMachine.ChangeState(StateMachine.IdlingState);
    }

    public void SwitchWeapon(InputAction.CallbackContext context)
    {
        Weapons[EquipedWeapon].Unequip();

        EquipedWeapon = EquipedWeapon == 0 ? 1 : 0;

        Weapons[EquipedWeapon].Equip();
        UpdateInventoryUI(); // Update the inventory UI when switching weapons
        inventoryUI.UpdateBackgroundColors(EquipedWeapon); // Update the background colors when switching weapons
    }

    public void PickUpWeapon(WeaponDataSO newWeapon)
    {
        if (Weapons[EquipedWeapon] != null)
        {
            Weapons[EquipedWeapon].Data = newWeapon;
            Weapons[EquipedWeapon].GenerateWeapon();
            Weapons[EquipedWeapon].Equip();
            StateMachine.ChangeState(StateMachine.IdlingState);
        }
        UpdateInventoryUI(); // Update the inventory UI when picking up a new weapon
    }

    public void FlipSprite(bool isFlipped)
    {
        SpriteRenderer.transform.localScale = new Vector3(isFlipped ? -1f : 1f, 1f, 1f);

        Weapons[0]?.FlipWeaponSprite(isFlipped ? Facing.left : Facing.right);
        Weapons[1]?.FlipWeaponSprite(isFlipped ? Facing.left : Facing.right);
    }

    public void PickUpHat(HatSO hat)
    {
        foreach (Transform child in SpriteRenderer.transform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(hat.Prefab, SpriteRenderer.transform);
    }

    public void PickUpConsumable(ConsumableDataSO consumable)
    {
        // Create a new GameObject for the consumable
        GameObject consumableObject = new GameObject("Consumable");
        consumableObject.transform.SetParent(transform);

        // Add the Consumable component to the new GameObject
        CurrentConsumable = consumableObject.AddComponent<Consumable>();
        CurrentConsumable.SetData(consumable);

        // Update the consumable UI
        consumableInventoryUI.UpdateConsumableSlot(consumable.Sprite);
    }

    private void UseConsumable(InputAction.CallbackContext context)
    {
        Debug.Log("UseConsumable action triggered.");
        if (CurrentConsumable != null)
        {
            Debug.Log("Using consumable: " + CurrentConsumable.Data.Name);
            CurrentConsumable.Use(this);
            Destroy(CurrentConsumable.gameObject); // Remove the consumable after use
            CurrentConsumable = null;
            consumableInventoryUI.UpdateConsumableSlot(null); // Clear the UI slot
        }
        else
        {
            Debug.LogWarning("No consumable to use.");
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interact action triggered.");
        // Handle interaction logic here, such as picking up items
    }

    private void OnDestroy()
    {
        _eventHandler.OnStep -= Step;
        Input.PlayerActions.Honk.started -= Honk;
        Input.PlayerActions.UseConsumable.started -= UseConsumable; // Remove input handling for using consumable
        Input.PlayerActions.SwitchWeapon.performed -= SwitchWeapon; // Remove input handling for switching weapon
        Input.PlayerActions.Interact.started -= Interact; // Remove input handling for interacting
    }

    private void UpdateInventoryUI() // Method to update the inventory UI
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            if (Weapons[i].Data != null)
            {
                // Update the weapon slot with the weapon's sprite and pass the weapon instance
                inventoryUI.UpdateWeaponSlot(i, Weapons[i].Data.WeaponSprite, Weapons[i]);
            }
            else
            {
                inventoryUI.UpdateWeaponSlot(i, null, null);
            }
        }

        // Update the consumable slot
        if (CurrentConsumable != null && CurrentConsumable.Data != null)
        {
            consumableInventoryUI.UpdateConsumableSlot(CurrentConsumable.Data.Sprite);
        }
        else
        {
            consumableInventoryUI.UpdateConsumableSlot(null);
        }
    }
}
