using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private HealthComponent _healthComponent;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _healthComponent = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();

        _healthComponent.OnHealthUpdated += UpdateHealth;
    }

    private void UpdateHealth()
    {
        _image.fillAmount = (float)_healthComponent.Health / (float)_healthComponent.MaxHealth;
    }

    private void OnDestroy()
    {
        _healthComponent.OnHealthUpdated -= UpdateHealth;
    }
}
