using UnityEngine;

namespace Finder.Configs
{
    [CreateAssetMenu(fileName = "New Difficulties", menuName = "Difficulties")]
    public class Difficulties : ScriptableObject
    {
        [SerializeField, Min(1)] private int _width = 1, _height = 1;

        public int Height => _height;
        public int Width => _width;
    }
}