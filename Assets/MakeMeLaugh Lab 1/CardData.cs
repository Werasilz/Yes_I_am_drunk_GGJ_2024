using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lab1
{
    [CreateAssetMenu(fileName = "Card Data", menuName = "Custom/Card/Data")]
    public class CardData : ScriptableObject
    {
        [field: SerializeField] public string CardName { get; private set; }
        [field: SerializeField] public string CardDescription { get; private set; }
        [field: SerializeField] public CardType CardType { get; private set; }
        [field: SerializeField] public Sprite CardImage { get; private set; }
    }
}
