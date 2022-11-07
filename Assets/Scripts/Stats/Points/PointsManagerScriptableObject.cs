using UnityEngine;
using Manapotion.Utilities;

namespace Manapotion.Stats
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Stats/Points/New PointsManagerScriptableObject")]
    public class PointsManagerScriptableObject : ScriptableObject
    {
        public PointScriptableObject[] pointsArray;

        public PointScriptableObject InitializePoint(PointScriptableObject point)
        {
            point.Initialize();
            return point;
        }

        public PointScriptableObject InitializePoint(PointScriptableObject point, StatsManagerScriptableObject statsManager)
        {
            point.Initialize(statsManager);
            return point;
        }

        public PointScriptableObject SetPoint(PointScriptableObject point, int currentValue, int maxValue)
        {
            point.value.maxValue = maxValue;
            point.value.currentValue = currentValue;
            return point;
        }
        public PointScriptableObject SetPoint(PointScriptableObject point, int maxValue)
        {
            point.value.maxValue = maxValue;
            return point;
        }

        public PointScriptableObject GetPoint(PointID pointID)
        {
            for (int i = 0; i < pointsArray.Length; i++)
            {
                if (pointsArray[i].pointID == pointID)
                {
                    return pointsArray[i];
                }
            }
            
            return null;
        }
    }
}