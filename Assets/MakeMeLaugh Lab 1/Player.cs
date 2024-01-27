using System;
using System.Collections;
using System.Collections.Generic;
using Lab1;
using UnityEngine;

namespace Lab1
{
    public class Player : MonoBehaviour
    {
        public List<Card> CurrentSelectedCard;

        public List<PlayedCardData> playedCardDatas;


        // ! Debuggg
        private float startPersistantValue = 1f;
        private float eachCardScore = 0.5f;
        private float addPersistanceValue = 0.5f;

        // ! Debuggg


        public bool CanSelectThisCard(Card card)
        {
            if (CurrentSelectedCard.Count == 0)
                return true;

            if (CurrentSelectedCard[0].cardType == card.cardType)
                return true;

            return false;

        }

        public void SelectCard(Card card)
        {
            CurrentSelectedCard.Add(card);
        }

        public void DeselectCard(Card card)
        {
            CurrentSelectedCard.Remove(card);
        }
        public void DeselectAllCard()
        {
            for (int i = 0; i < CurrentSelectedCard.Count; i++)
            {
                CurrentSelectedCard[i].ForceClose();
            }

            CurrentSelectedCard = new List<Card>();
        }

        public void PlayCard()
        {
            if (CurrentSelectedCard.Count == 0) return;

            PlayedCardData playedData = new PlayedCardData();

            int cardDataIndex;

            if (playedCardDatas.Count == 0)
            {
                cardDataIndex = -1;

            }
            else
            {
                cardDataIndex = playedCardDatas.Count - 1;
            }

            // Setup Persistant Value
            playedData.persistantValue = (cardDataIndex == -1) ? startPersistantValue : playedCardDatas[cardDataIndex].persistantValue;

            if (NeedToAddPersistantValue())
            {
                playedData.persistantValue += addPersistanceValue;
            }

            // Setup Last Card Type Value
            playedData.lastCardType = CurrentSelectedCard[0].cardType;

            // Setup Stack Bonus Value
            playedData.stackBonusValue = GetStackBonus(cardDataIndex);

            // Setup Calculate Value
            playedData.calculateValue = playedData.persistantValue + playedData.stackBonusValue;

            // Set Total Value
            playedData.totalValue = GetTotal(playedData, cardDataIndex);

            playedCardDatas.Add(playedData);

            DeselectAllCard();
        }

        private float GetTotal(PlayedCardData playedData, int cardDataIndex)
        {
            if (cardDataIndex == -1)
                return playedData.calculateValue;
            else
                return playedCardDatas[cardDataIndex].totalValue + playedData.calculateValue;
        }

        private float GetStackBonus(int cardDataIndex)
        {
            if (cardDataIndex == -1)
                return 0;

            if (CurrentSelectedCard.Count >= 2)
                return eachCardScore * CurrentSelectedCard.Count;
            else
                return 0;
        }

        private bool NeedToAddPersistantValue()
        {
            if (playedCardDatas.Count == 0)
                return false;

            if (CurrentSelectedCard[0].cardType == playedCardDatas[playedCardDatas.Count - 1].lastCardType)
                return true;

            return false;
        }
    }

    [System.Serializable]
    public class PlayedCardData
    {
        public CardType lastCardType;
        public float persistantValue;
        public float stackBonusValue;
        public float calculateValue;
        public float totalValue;
    }
}