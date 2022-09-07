namespace Manapotion.Equipables
{
    public enum EquipableStats
    {
        // Used on Weapons
        AttackDamage,
        AttackSpeed,
        CriticalStrike,
        CriticalChance,

        // Used on Armour
        Defense,
        KnockbackResistance,

    }

    public class EquipableStat
    {
        public EquipableStats id;
        public float value;

        public EquipableStat(EquipableStats id) {
            this.id = id;
        }

        public EquipableStat(float value, EquipableStats id) {
            this.value = value;
            this.id = id;
        }
    }
}
