using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lab1
{
    public class History : MonoBehaviour
    {
        public List<CardData> historyCardDatas;
        public Image[] historyImages;
        public void SetHistory(List<CardData> historyData)
        {
            historyCardDatas = historyData;

            for (int i = 0; i < historyImages.Length; i++)
            {
                if (i < historyCardDatas.Count)
                {
                    historyImages[i].color = historyCardDatas[i].CardColor;
                }
                else
                {
                    historyImages[i].color = new Color(0, 0, 0, 0);
                }
            }

        }
    }
}
