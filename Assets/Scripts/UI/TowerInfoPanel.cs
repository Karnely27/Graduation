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
        _text.text = $"����������� �������� � ���� �����. ���������� ����� - {_tower.LevelUpDamage}\n" +
            $"���������� �������� - {_tower.LevelUpHealth}\n��������� - {_tower.PriceLevelUp} �������\n" +
            $"������� ���� - {_tower.Damage}";
        _panel.SetActive(isHold);
    }
}
