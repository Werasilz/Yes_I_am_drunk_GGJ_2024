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
        [SerializeField] Image currentColor;
        [SerializeField] GameObject highLightColor;

        bool isDelay1;
        bool isDelay2;

        public void InitalizeNewCardData(CardData cardData)
        {
            this.cardData = cardData;
            currentImage.sprite = cardData.CardImage;
            currentColor.color = cardData.CardColor;
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
            if (isDelay1 || isDelay2) return;

            isDelay1 = true;
            isDelay2 = true;

            DOVirtual.DelayedCall(0.05f, () =>
            {
                isDelay1 = false;

                DOVirtual.DelayedCall(0.05f, () =>
                {
                    isDelay2 = false;
                });

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