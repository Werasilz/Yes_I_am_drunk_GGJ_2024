using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lab1
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        public CardType cardType;
        private Player player => GetComponentInParent<Player>();
        public bool selected = false;
        [SerializeField] GameObject highLightColor;
        public void OnPointerClick(PointerEventData eventData)
        {
            ToggleSelect();
        }

        private void ToggleSelect()
        {
            SelectThisCard(!selected);
        }

        private void SelectThisCard(bool isSelect)
        {
            if (player.CanSelectThisCard(this) == false) return;

            selected = isSelect;

            if (selected)
            {
                player.SelectCard(this);
            }
            else
            {
                player.DeselectCard(this);
            }

            // Display
            highLightColor.SetActive(selected);
        }

        public void ForceClose()
        {
            selected = false;
            highLightColor.SetActive(false);
        }
    }

    public enum CardType
    {
        Red,
        Green,
        Blue,
        Yellow
    }
}