using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealthBar : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.maxValue = _tower.MaxHealth;
    }

    private void OnEnable()
    {
        _tower.HealthChanged += ChangeTowerHealth;
    }

    private void OnDisable()
    {
        _tower.HealthChanged -= ChangeTowerHealth;
    }

    private void ChangeTowerHealth(int health)
    {
        _slider.maxValue = _tower.MaxHealth;
        _slider.value = health;
        _healthText.text = health.ToString();
    }
}
