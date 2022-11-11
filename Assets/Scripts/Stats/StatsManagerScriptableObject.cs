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
                statArray[i].Init();
                statArray[i].OnStatModifiedEvent += StatModified;
            }
        }

        public void StatModified(object sender, Stat.OnStatModifiedEventArgs e)
        {
            Debug.Log($"{e.statID} stat modified! base: {e.baseValue}, mod: {e.modifiedValue}");
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