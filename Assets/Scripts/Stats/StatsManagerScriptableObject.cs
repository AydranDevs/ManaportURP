using UnityEngine;
namespace Manapotion.Stats
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Stats/New StatsManagerScriptableObject")]
    public class StatsManagerScriptableObject : ScriptableObject
    {
        public Stat[] stats;

        public void OnEnable()
        {
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i].value.Init();
            }
        }

        public void StatModified(Stat stat)
        {
            Debug.Log(
                string.Format(
                    "{0} was modified. Value is now {1}", stat.statID, stat.value.modifiedValue
                )
            );
        }

        public Stat GetStat(StatID statID)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i].statID == statID)
                {
                    return stats[i];
                }
            }
            
            return null;
        }
    }
}