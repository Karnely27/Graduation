using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreatureInfoPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Creature _creature;
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
        _text.text = $"�������� - {_creature.CreatureName}\n����. �������� - {_creature.MaxHealth}\n" +
            $"���� - {_creature.Damage}\n�������� ����� - {_creature.Delay}\n��������� ����� - {_creature.TransitionRange}\n" +
            $"�������� �������� - {_creature.Speed}\n���� - {_creature.Price}";
        _panel.SetActive(isHold);
    }
}

