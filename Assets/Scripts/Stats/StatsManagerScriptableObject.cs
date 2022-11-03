using UnityEngine;
namespace Manapotion.Stats
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Stats/New StatsManagerScriptableObject")]
    public class StatsManagerScriptableObject : ScriptableObject
    {
        public Stat[] stats;

        public void StatModified(Stat stat)
        {
            Debug.Log(
                string.Format(
                    "{0} was modified. Value is now {1}", stat.statID, stat.value.modifiedValue
                )
            );
        }
    }
}