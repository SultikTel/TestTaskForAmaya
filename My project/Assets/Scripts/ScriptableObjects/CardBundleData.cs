using Finder.Gameplay.Items;
using UnityEngine;

namespace Finder.Configs
{
    [CreateAssetMenu(fileName = "New CardBundleData", menuName = "Card Bundle Data")]
    public class CardBundleData : ScriptableObject
    {
        [SerializeField] private CardData[] _cardData;

        public CardData[] CardData => _cardData;
    }
}