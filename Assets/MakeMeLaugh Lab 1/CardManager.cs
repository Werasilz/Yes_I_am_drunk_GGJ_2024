using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lab1
{
    public class CardManager : MonoBehaviour
    {
        public List<CardData> cardDeck;
        public List<CardData> currentDeck;

        private void Start()
        {
            IntializeDeck();
        }

        public void IntializeDeck()
        {
            currentDeck.AddRange(cardDeck);

            ShuffleDeck();
        }

        public void ShuffleDeck()
        {
            List<CardData> temp_deck1 = currentDeck;
            List<CardData> temp_deck2 = new List<CardData>();

            while (temp_deck1.Count == 0)
            {
                int r = Random.Range(0, temp_deck1.Count);
                temp_deck2.Add(temp_deck1[r]);
                temp_deck1.RemoveAt(r);
            }

            currentDeck = temp_deck2;

            /*

                        for (int i = 0; i < currentDeck.Count; i++)
                        {
                            if (temp_deck.Count == i)
                            {
                                temp_deck.Add(currentDeck[i]);
                            }
                            else
                            {
                                temp_deck.Add()
                            }

                            int r = Random.Range(i, currentDeck.Count);
                            texts[i] = texts[r];
                            texts[r] = tmp;
                        }
                        */
        }
    }
}