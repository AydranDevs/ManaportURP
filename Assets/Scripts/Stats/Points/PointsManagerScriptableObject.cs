using UnityEngine;
using Manapotion.Utilities;

namespace Manapotion.Stats
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Stats/Points/New PointsManagerScriptableObject")]
    public class PointsManagerScriptableObject : ScriptableObject
    {
        public StatsManagerScriptableObject statsManagerScriptableObject;
        public PointScriptableObject[] pointsArray;

        private void OnEnable() {

            // Initialize every point scriptable obj
            if (statsManagerScriptableObject != null)
            {
                for (int i = 0; i < pointsArray.Length; i++)
                {
                    pointsArray[i].Initialize(statsManagerScriptableObject);
                }
            }
        }
        
        /// <summary>
        /// Refresh each point scriptable obj (used when stats are updated)
        /// </summary>
        public void RefreshPoints()
        {
            for (int i = 0; i < pointsArray.Length; i++)
            {
                var modStat = statsManagerScriptableObject.GetStat(pointsArray[i].value_maxValueModulator);
                pointsArray[i].SetMaxValue(modStat.value.modifiedValue);
            }
        }

        public PointScriptableObject GetPointScriptableObject(PointID id)
        {
            for (int i = 0; i < pointsArray.Length; i++)
            {
                if (pointsArray[i].pointID == id)
                {
                    return pointsArray[i];
                }
            }

            return null;
        }
    }
}