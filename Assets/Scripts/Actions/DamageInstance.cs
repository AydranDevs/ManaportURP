namespace Manapotion.Actions
{
    public class DamageInstance
    {
        public enum DamageInstanceType { Physical, Magical }
        public DamageInstanceType damageInstanceType = DamageInstanceType.Physical;
        public enum DamageInstanceElement { Arcane, Pyro, Cryo, Toxi, Volt }
        public DamageInstanceElement damageInstanceElement = DamageInstanceElement.Arcane;
        public int damageInstanceAmount;   
    }
}