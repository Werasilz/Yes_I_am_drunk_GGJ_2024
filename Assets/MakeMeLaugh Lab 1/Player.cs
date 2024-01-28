using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

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

        public VfxParticle vfxPrefabs;
        public Transform vfxSpawnPosition;
        public Button playButton;
        Coroutine createVfxIE;
        // ! Debuggg
        [HideInInspector] public float startPersistantValue = 1f;
        private float eachCardScore = 1f;
        private float addPersistanceValue = 1f;

        public static System.Action<bool> OnPlayerEndTurn = delegate { };

        // ! Debuggg

        public void Initialize()
        {
            if (createVfxIE != null)
                StopCoroutine(createVfxIE);

            playButton.gameObject.SetActive(true);

            playedCardDatas = new List<PlayedCardData>();
            DeselectAllCard();

            deck.IntializeDeck();

            for (int i = 0; i < CardHandleOnHand.Count; i++)
            {
                CardHandleOnHand[i].highLightColor.SetActive(false);
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

            float hintPersist = playedData.persistantValue;
            float hintStack = 0f;
            float hintSummary = 0f;

            if (NeedToAddPersistantValue())
            {
                // Show Hint persist score
                hintPersist += addPersistanceValue;
                UIGameplayManager.Instance.valueTexts[0].persistantValueText.text = $"{playedData.persistantValue}(+{addPersistanceValue})";
            }
            else
            {
                UIGameplayManager.Instance.valueTexts[0].persistantValueText.text = (playedCardDatas.Count == 0) ? startPersistantValue.ToString() : playedCardDatas[playedCardDatas.Count - 1].persistantValue.ToString();
            }

            // Show Hint stack score
            hintStack = GetStackBonus();
            UIGameplayManager.Instance.valueTexts[0].stackBonusValueText.text = hintStack.ToString();

            // Setup Calculate Value
            hintSummary = hintPersist + hintStack;
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

            // 
            CreateParticle(cardDatas);

        }

        private void CreateParticle(CardData[] cardDatas)
        {
            createVfxIE = StartCoroutine(CreateParticleCoroutine(cardDatas));
        }

        private IEnumerator CreateParticleCoroutine(CardData[] cardDatas)
        {
            playButton.gameObject.SetActive(false);

            for (int i = 0; i < cardDatas.Length; i++)
            {
                VfxParticle _vfx = Instantiate(vfxPrefabs, vfxSpawnPosition.position + GetRandomCirclePosition(0.2f), Quaternion.identity);
                _vfx.vfxImage.sprite = cardDatas[i].CardImage;

                Destroy(_vfx.gameObject, 1f);

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.6f);

            playButton.gameObject.SetActive(true);


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

        Vector3 GetRandomCirclePosition(float circleRadius)
        {
            // Generate a random angle
            float angle = Random.Range(0f, 2f * Mathf.PI);

            // Convert polar coordinates to Cartesian coordinates
            float x = circleRadius * Mathf.Cos(angle);
            float y = circleRadius * Mathf.Sin(angle);

            return new Vector3(x, y);
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