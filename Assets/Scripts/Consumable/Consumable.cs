using System;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public event Action<bool> OnCurrentInputChange;

    public ConsumableDataSO Data { get; set; } // Ensure the set accessor is public

    public float AnimationSpeedMultiplier = 1.0f;

    public bool CurrentInput
    {
        get => _currentInput;
        set
        {
            if (_currentInput != value)
            {
                _currentInput = value;
                OnCurrentInputChange?.Invoke(_currentInput);
            }
        }
    }

   

    public GameObject BaseGameObject { get; private set; }

    public AnimationEventHandler EventHandler { get; private set; }
    public HealthComponent User { get; set; }

    private bool _currentInput;

    public void SetData(ConsumableDataSO data) => Data = data;

    private void Awake()
    {
        BaseGameObject = transform.Find("Base")?.gameObject;
        if (BaseGameObject == null)
        {
            Debug.LogWarning("BaseGameObject is not assigned.");
        }

        User = transform.parent?.GetComponent<HealthComponent>();
        if (User == null)
        {
            Debug.LogWarning("User is not assigned.");
        }

        EventHandler = BaseGameObject?.GetComponent<AnimationEventHandler>();
        if (EventHandler == null)
        {
            Debug.LogWarning("EventHandler is not assigned.");
        }
    }

    public void Use(Player player)
    {
        if (Data.Effect != null)
        {
            Data.Effect.ApplyEffect(player);
        }
        else
        {
            Debug.LogWarning("Consumable effect is not set.");
        }
    }
}
