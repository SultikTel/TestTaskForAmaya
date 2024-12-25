using Finder.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Finder.Controllers
{
    public class ConfigController : MonoBehaviour
    {
        [SerializeField] private List<Difficulties> _difficulties;
        [SerializeField] private List<CardBundleData> _cardBundleDatas;
        private int _currentDifficultIndex;

        public int DifficultyNumber => _difficulties.Count;

        public Difficulties GetCurrentDifficulty() => _difficulties[_currentDifficultIndex];

        public void NewGameStart() => _currentDifficultIndex = 0;

        public CardBundleData GetRandomBundle()
        {
            if (_cardBundleDatas?.Count == 0)
            {
                return null;
            }

            return _cardBundleDatas[new System.Random().Next(_cardBundleDatas.Count)];
        }

        public bool ImproveDifficult()
        {
            if (++_currentDifficultIndex >= _difficulties.Count)
            {
                return false;
            }
            return true;
        }
    }
}