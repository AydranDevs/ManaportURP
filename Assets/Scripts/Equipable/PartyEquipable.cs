namespace Manapotion.Equipables
{
    public enum EquipableType { Weapon, Armour, Vanity }

    public abstract class PartyEquipable
    {
        public string ID;

        public bool[] charactersThatCanEquip = new bool[3]
        {
            false,  // Laurie
            false,  // Mirabelle
            false   // Winsley
        };

        public EquipableType equipableType = EquipableType.Weapon;

        public EquipableStat[] stats = new EquipableStat[]
        {
            new EquipableStat(EquipableStats.AttackDamage),
            new EquipableStat(EquipableStats.AttackSpeed),
            new EquipableStat(EquipableStats.CriticalStrike),
            new EquipableStat(EquipableStats.CriticalChance),

            new EquipableStat(EquipableStats.Defense),
            new EquipableStat(EquipableStats.KnockbackResistance)
        };

        #region Equip State changed
        // runs when the equipable is equipped
        public abstract void OnEquip();

        // runs while the equipable is equipped
        public abstract void WhileEquipped();

        // runs when the equipable is unequipped
        public abstract void OnUnequip();
        #endregion
    }
}
