using System;
using UnityEngine;

namespace Finder.Gameplay.Items
{
    [Serializable]
    public class CardData
    {
        [SerializeField] private string _identifier;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Rotation _neededRotation;

        public Rotation NeededRotation => _neededRotation;
        public Sprite Sprite => _sprite;
        public string Identifier => _identifier;
    }

    public enum Rotation
    {
        Static,
        Left,
        Right,
        UpsideDown
    }
}