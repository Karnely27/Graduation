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
        _text.text = $"Название - {_creature.CreatureName}\nМакс. здоровье - {_creature.MaxHealth}\n" +
            $"Урон - {_creature.Damage}\nСкорость атаки - {_creature.Delay}\nДальность атаки - {_creature.TransitionRange}\n" +
            $"Скорость движения - {_creature.Speed}\nЦена - {_creature.Price}";
        _panel.SetActive(isHold);
    }
}

