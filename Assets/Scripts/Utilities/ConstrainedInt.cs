using UnityEngine.Events;

namespace Manapotion.Utilities
{
    [System.Serializable]
    public class ConstrainedInt
    {
        /// <summary>
        /// called when the currentValue is changed
        /// </summary>
        public UnityEvent<int, int> ValueChangedEvent;
        /// <summary>
        /// called when the currentValue == zero
        /// </summary>
        public UnityEvent ValueIsZeroEvent;
        /// <summary>
        /// called when the currentValue == maxValue
        /// </summary>
        public UnityEvent ValueIsMaxEvent;

        [UnityEngine.SerializeField]
        private int _currentValue;
        public int currentValue
        {
            get
            {
                return _currentValue;
            }
            set
            {
                this._currentValue = value;
                ValueChanged();
            }
        }

        [UnityEngine.SerializeField]
        private int _maxValue;
        public int maxValue
        {
            get 
            {
                return _maxValue;
            } 
            set
            {
                this._maxValue = value;
                MaxValueChanged();
            }
        }

        private void ValueChanged()
        {
            if (_currentValue > _maxValue)
            {
                _currentValue = _maxValue;
                ValueIsMaxEvent.Invoke();
            }
            if (_currentValue <= 0)
            {
                _currentValue = 0;
                ValueIsZeroEvent.Invoke();
            }

            ValueChangedEvent.Invoke(_currentValue, _maxValue);
        }

        public void MaxValueChanged()
        {
            if (_currentValue > _maxValue)
            {
                _currentValue = _maxValue;
            }
        }

        public bool CanSubtract(int subtrahend)
        {
            var cv = _currentValue;
            return cv - subtrahend >= 0;
        }
    }
}
