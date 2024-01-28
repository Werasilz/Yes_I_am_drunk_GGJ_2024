using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lab1
{
    public class History : MonoBehaviour
    {
        public TMP_Text lastRoundText;
        public List<CardData> historyCardDatas;
        public Image[] historyImages;

        private void Initialize()
        {
            lastRoundText.gameObject.SetActive(false);

            for (int i = 0; i < historyImages.Length; i++)
            {
                historyImages[i].color = new Color(0, 0, 0, 0);
                historyImages[i].gameObject.SetActive(false);
            }
        }

        public void SetHistory(List<CardData> historyData)
        {
            lastRoundText.gameObject.SetActive(true);
            historyCardDatas = historyData;

            for (int i = 0; i < historyImages.Length; i++)
            {
                if (i < historyCardDatas.Count)
                {
                    historyImages[i].color = historyCardDatas[i].CardColor;
                    historyImages[i].gameObject.SetActive(true);
                }
                else
                {
                    historyImages[i].color = new Color(0, 0, 0, 0);
                    historyImages[i].gameObject.SetActive(false);
                }
            }

        }
    }
}
