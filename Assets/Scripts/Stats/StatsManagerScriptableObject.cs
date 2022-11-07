using UnityEngine;
namespace Manapotion.Stats
{
    [CreateAssetMenu(menuName = "Manapotion/ScriptableObjects/Stats/New StatsManagerScriptableObject")]
    public class StatsManagerScriptableObject : ScriptableObject
    {
        public Stat[] statArray;

        public void OnEnable()
        {
            for (int i = 0; i < statArray.Length; i++)
            {
                statArray[i].value.Init();
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
            for (int i = 0; i < statArray.Length; i++)
            {
                if (statArray[i].statID == statID)
                {
                    return statArray[i];
                }
            }
            
            return null;
        }
    }
}