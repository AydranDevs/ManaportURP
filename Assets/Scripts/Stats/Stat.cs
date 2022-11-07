namespace Manapotion.Stats
{
    [System.Serializable]
    public class Stat
    {
        public StatsManagerScriptableObject parent;
        public StatID statID;
        public ModifiableInt value;

        public void SetParent(StatsManagerScriptableObject p)
        {
            parent = p;
            value = new ModifiableInt();
        }
        public void StatModified()
        {
            parent.StatModified(this);
        }
    }
}