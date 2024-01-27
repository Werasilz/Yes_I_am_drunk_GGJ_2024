using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lab1
{
    public class Player : MonoBehaviour
    {
        public List<Card> CardHandleOnHand;
        public List<Card> CurrentSelectedCard;
        public List<PlayedCardData> playedCardDatas;

        public Deck deck;
        public History history;

        public bool isPlayer;

        // ! Debuggg
        private float startPersistantValue = 1f;
        private float eachCardScore = 1f;
        private float addPersistanceValue = 1f;

        public static System.Action<bool> OnPlayerEndTurn = delegate { };

        // ! Debuggg

        private void Start()
        {
            deck.IntializeDeck();

            Initialize();

        }

        public void Initialize()
        {
            for (int i = 0; i < CardHandleOnHand.Count; i++)
            {
                CardData data = deck.DrawCard();
                CardHandleOnHand[i].InitalizeNewCardData(data);
            }
        }

        public bool CanSelectThisCard(Card card)
        {
            if (CurrentSelectedCard.Count == 0)
                return true;

            if (CurrentSelectedCard[0].cardData.CardType == card.cardData.CardType)
                return true;

            return false;

        }

        public void SelectCard(Card card)
        {
            if (!CurrentSelectedCard.Contains(card))
            {
                CurrentSelectedCard.Add(card);
                ShowHintScore();
            }
        }

        public void DeselectCard(Card card)
        {
            if (CurrentSelectedCard.Contains(card))
            {
                CurrentSelectedCard.Remove(card);

                if (CurrentSelectedCard.Count <= 0)
                {
                    UIGameplayManager.Instance.ClearHintText();
                }
                else
                {
                    ShowHintScore();
                }
            }
        }
        public void DeselectAllCard()
        {
            for (int i = 0; i < CurrentSelectedCard.Count; i++)
            {
                CurrentSelectedCard[i].ForceClose();
            }

            UIGameplayManager.Instance.ClearHintText();
            CurrentSelectedCard = new List<Card>();
        }

        private void ShowHintScore()
        {
            if (CurrentSelectedCard.Count == 0) return;
            print($"Show Hint Score CurrentSelectedCard:{CurrentSelectedCard.Count}");
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

            float hintPersist = 0f;
            float hintStack = 0f;
            float hintSummary = 0f;

            if (NeedToAddPersistantValue())
            {
                // Show Hint persist score
                hintPersist = playedData.persistantValue + 1;
                UIGameplayManager.Instance.valueTexts[0].persistantValueText.text = $"+{hintPersist}";
            }
            else
            {
                UIGameplayManager.Instance.valueTexts[0].persistantValueText.text = string.Empty;
            }

            // Show Hint stack score
            hintStack = GetStackBonus();
            UIGameplayManager.Instance.valueTexts[0].stackBonusValueText.text = hintStack.ToString();

            // Setup Calculate Value
            hintSummary = playedData.persistantValue + hintStack;
            UIGameplayManager.Instance.valueTexts[0].calculateValueText.text = hintSummary.ToString();
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
            playedData.lastCardType = CurrentSelectedCard[0].cardData.CardType;

            // Setup Stack Bonus Value
            playedData.stackBonusValue = GetStackBonus();

            // Setup Calculate Value
            playedData.calculateValue = playedData.persistantValue + playedData.stackBonusValue;

            // Set Total Value
            playedData.totalValue = GetTotal(playedData, cardDataIndex);

            playedCardDatas.Add(playedData);

            CardData[] cardDatas = CurrentSelectedCard.Select(t => t.cardData).ToArray();

            history.SetHistory(cardDatas.ToList());

            UIGameplayManager.OnDisplayValue?.Invoke(isPlayer ? 0 : 1,
                                                     playedData.persistantValue.ToString(),
                                                     playedData.stackBonusValue.ToString(),
                                                     playedData.calculateValue.ToString(),
                                                     playedData.totalValue.ToString());

            // Initialize new Card with Used card
            for (int i = 0; i < CurrentSelectedCard.Count; i++)
            {
                CurrentSelectedCard[i].InitalizeNewCardData(deck.DrawCard());
            }

            DeselectAllCard();

            OnPlayerEndTurn?.Invoke(isPlayer);

            UIGameplayManager.Instance.turnCounter.TurnUpdate();

            UIGameplayManager.Instance.ClearHintText();
        }

        private float GetTotal(PlayedCardData playedData, int cardDataIndex)
        {
            if (cardDataIndex == -1)
                return playedData.calculateValue;
            else
                return playedCardDatas[cardDataIndex].totalValue + playedData.calculateValue;
        }

        private float GetStackBonus()
        {
            if (CurrentSelectedCard.Count >= 2)
                return eachCardScore * CurrentSelectedCard.Count;
            else
                return 0;
        }

        private bool NeedToAddPersistantValue()
        {
            if (playedCardDatas.Count == 0)
                return false;

            if (CurrentSelectedCard[0].cardData.CardType == playedCardDatas[playedCardDatas.Count - 1].lastCardType)
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