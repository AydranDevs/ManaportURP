using UnityEngine;
using Manapotion.Utilities;

namespace Manapotion.Stats
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Stats/Points/New PointScriptableObject")]
    public class PointScriptableObject : ScriptableObject
    {
        public PointID pointID;
        public PointsManagerScriptableObject parent;

        public enum ModulatorType { Addend, Subtrahend, Factor, Divisor }
        
        [Header("Value Manipulators")]
        [Tooltip("Modifier to the value's maxValue field")]
        public StatID value_maxValueModulator;

        public bool startAtMaxValue;

        public void Initialize()
        {
            // Debug.Log("Initialize");
        }

        public void Initialize(StatsManagerScriptableObject statsManager)
        {
            var modStat = statsManager.GetStat(value_maxValueModulator);
            value.maxValue = modStat.value.modifiedValue;

            if (startAtMaxValue)
            {
                value.currentValue = value.maxValue;
            }
        }

        /// <summary>
        /// Set the current value of this point object.
        /// </summary>
        /// <param name="currentValue"></param>
        public void SetValue(int n)
        {
            value.currentValue = n;
        }

        /// <summary>
        /// Set the max value of this point object
        /// </summary>
        /// <param name="maxValue"></param>
        public void SetMaxValue(int n)
        {
            value.maxValue = n;
        }
        /// <summary>
        /// Set the max value of this point object
        /// </summary>
        /// <param name="maxValue"></param>
        /// <param name="maxCurrent">should the current value be set to max value as well?</param>
        public void SetMaxValue(int n, bool maxCurrent)
        {
            value.maxValue = n;
            if (maxCurrent)
            {
                value.currentValue = n;
            }
        }

        /// <summary>
        /// /// Set both the current and max value of this point object
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="maxValue"></param>
        public void SetValues(int currentValue, int maxValue)
        {
            value.maxValue = maxValue;
            value.currentValue = currentValue;
        }

        public ConstrainedInt value;   
    }
}