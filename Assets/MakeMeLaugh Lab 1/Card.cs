using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lab1
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        public CardData cardData;
        private Player player => GetComponentInParent<Player>();
        public bool selected = false;
        [SerializeField] Image currentImage;
        [SerializeField] GameObject highLightColor;

        bool isDelay;

        public void InitalizeNewCardData(CardData cardData)
        {
            this.cardData = cardData;
            currentImage.color = cardData.CardColor;
        }

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
            if (isDelay) return;

            isDelay = true;

            DOVirtual.DelayedCall(0.1f, () =>
            {
                isDelay = false;

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
            });


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