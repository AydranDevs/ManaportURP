namespace Manapotion.Stats
{
    public class Buff : IModifier
    {
        public Stat stat;
        public int value;

        public void AddValue(ref int v)
        {
            v += value;
        }
    }
}