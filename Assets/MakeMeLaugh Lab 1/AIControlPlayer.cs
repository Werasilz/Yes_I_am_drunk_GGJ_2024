using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lab1
{
    public class AIControlPlayer : MonoBehaviour
    {
        public enum AiSelectCard
        {
            RandomCardType, UseTheMostType
        }

        [Header("Ai Setting")]
        [SerializeField] AiSelectCard selectCardType;
        [SerializeField][Range(0, 100)] float select1CardWeight;
        [SerializeField][Range(0, 100)] float select2CardsWeight;
        [SerializeField][Range(0, 100)] float select3CardsWeight;
        [SerializeField][Range(0, 100)] float select4CardsWeight;
        [SerializeField] Player Ai;
        Coroutine StartAiTurnIE;

        private void Awake()
        {
            Player.OnPlayerEndTurn += OnPlayerEndTurnCallback;
        }

        private void OnPlayerEndTurnCallback(bool isPlayer)
        {
            if (isPlayer)
            {
                if (StartAiTurnIE != null)
                    StopCoroutine(StartAiTurnIE);

                StartAiTurnIE = StartCoroutine(StartAiTurn());
            }
        }

        private void OnDestroy()
        {
            Player.OnPlayerEndTurn -= OnPlayerEndTurnCallback;
        }

        private IEnumerator StartAiTurn()
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            CalculateAndPlayCard();
        }

        private void CalculateAndPlayCard()
        {
            switch (selectCardType)
            {
                case AiSelectCard.RandomCardType:
                    SelectByRandomCardType();
                    break;
                case AiSelectCard.UseTheMostType:
                    // SelectByUseTheMostType();
                    break;
            }
        }

        private void SelectByRandomCardType()
        {
            List<CardType> allTypesInHand = new List<CardType>();
            for (int i = 0; i < Ai.CardHandleOnHand.Count; i++)
            {
                if (allTypesInHand.Count == 0)
                {
                    allTypesInHand.Add(Ai.CardHandleOnHand[i].cardData.CardType);
                    continue;
                }

                CardType newType = Ai.CardHandleOnHand[i].cardData.CardType;
                if (!allTypesInHand.Contains(newType))
                {
                    allTypesInHand.Add(newType);
                    continue;
                }
            }

            int _random = Random.Range(0, allTypesInHand.Count);

            CardType _type = allTypesInHand[_random];

            for (int i = 0; i < Ai.CardHandleOnHand.Count; i++)
            {
                if (Ai.CardHandleOnHand[i].cardData.CardType == _type)
                {
                    Ai.SelectCard(Ai.CardHandleOnHand[i]);
                }
            }

            Ai.PlayCard();
        }

        /*
                private void SelectByUseTheMostType()
                {
                    void Random4()
                    {

                    }

                    void Random3()
                    {

                    }

                    void Random2()
                    {

                    }

                    void Random1()
                    {

                    }

                    GetMostType();

                    int conditionCase = GetSelectCardConditionCase();

                    switch (conditionCase)
                    {
                        case 4:

                            break;
                        case 3:
                            break;
                        case 2:
                            break;
                        case 1:
                            break;
                    }
                }

                private int GetMostType()
                {
                    int[] cardCount = new int[4];

                    for (int i = 0; i < Ai.CardHandleOnHand.Count; i++)
                    {
                        switch (Ai.CardHandleOnHand[i].cardData.CardType)
                        {
                            case CardType.Red:
                                cardCount[0]++;
                                break;
                            case CardType.Green:
                                cardCount[1]++;
                                break;
                            case CardType.Blue:
                                cardCount[2]++;
                                break;
                            case CardType.Yellow:
                                cardCount[3]++;
                                break;
                        }
                    }

                }
        */
        private int GetSelectCardConditionCase()
        {
            float totalWeight = select1CardWeight + select2CardsWeight + select3CardsWeight + select4CardsWeight;
            float randomValue = Random.Range(0f, totalWeight);

            if (randomValue < select1CardWeight)
            {
                return 1;
            }
            else if (randomValue < select1CardWeight + select2CardsWeight)
            {
                return 2;
            }
            else if (randomValue < select1CardWeight + select2CardsWeight + select3CardsWeight)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
    }
}