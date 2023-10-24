using UnityEngine;

namespace _Game.Common
{
    [System.Serializable]
    public struct FloatRange 
    {
        [SerializeField] private float _min, _max;

        public float Min => _min;
        public float Max => _max;

        public float RandomValueInRange 
        {
            get => Random.Range(_min, _max);
        }

        public FloatRange(float value)
        {
            _min = _max = value;
        }

        public FloatRange(float min, float max)
        {
            _min = min;
            _max = max;
        }
    }
    public class FloatRangeSliderAttribute : PropertyAttribute
    {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public FloatRangeSliderAttribute(float min, float max)
        {
            if (max < min)
            {
                max = min;
            }

            Min = min;
            Max = max;
        }
    }
}