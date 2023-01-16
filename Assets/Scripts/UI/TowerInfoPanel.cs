using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerInfoPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Tower _tower;
    [SerializeField] private TMP_Text _text;

    private bool isHold = false;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isHold = true;
        OnPanel(isHold);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isHold = false;
        OnPanel(isHold);
    }

    private void OnPanel(bool isHold)
    {
        _text.text = $"Увеличивает здоровье и урон башни. Увеличение урона - {_tower.LevelUpDamage}\n" +
            $"Увеличение здоровья - {_tower.LevelUpHealth}\nСтоимость - {_tower.PriceLevelUp} алмазов\n" +
            $"Текущий урон - {_tower.Damage}";
        _panel.SetActive(isHold);
    }
}
