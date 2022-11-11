using UnityEngine;
using Manapotion.Utilities;

namespace Manapotion.Stats
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Stats/Points/New PointsManagerScriptableObject")]
    public class PointsManagerScriptableObject : ScriptableObject
    {
        public PointScriptableObject[] pointsArray;
    }
}