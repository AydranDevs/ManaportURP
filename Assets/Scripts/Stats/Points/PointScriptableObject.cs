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
        [Tooltip("Type of modification to make to the value's maxValue field (Add, Subtract, Multiply, Divide)")]
        public ModulatorType modulatorType;

        public bool startAtMaxValue;

        public void Initialize()
        {
            // Debug.Log("Initialize");
        }

        public void Initialize(StatsManagerScriptableObject statsManager)
        {
            var modStat = statsManager.GetStat(value_maxValueModulator);
            switch (modulatorType)
            {
                case ModulatorType.Addend: value.maxValue += modStat.value.modifiedValue; break;
                case ModulatorType.Subtrahend: value.maxValue -= modStat.value.modifiedValue; break;
                case ModulatorType.Factor: value.maxValue *= modStat.value.modifiedValue; break;
                case ModulatorType.Divisor: value.maxValue /= modStat.value.modifiedValue; break;
            }

            if (startAtMaxValue)
            {
                value.currentValue = value.maxValue;
            }
        }

        public ConstrainedInt value;   
    }
}