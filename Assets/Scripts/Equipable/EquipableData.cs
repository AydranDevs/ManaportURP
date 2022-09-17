namespace Manapotion.Equipables
{   
    [System.Serializable]
    public class EquipableData
    {
        public string name;
        public string lore;
        
        public EquipableStat[] stats = new EquipableStat[]
        {
            new EquipableStat(EquipableStats.AttackDamage),
            new EquipableStat(EquipableStats.AttackSpeed),
            new EquipableStat(EquipableStats.CriticalChance),
            new EquipableStat(EquipableStats.CriticalStrike),
            
            new EquipableStat(EquipableStats.Defense),
            new EquipableStat(EquipableStats.KnockbackResistance)
        };

        public bool equipped { get; private set; } = false;

        public virtual void OnEquip() { equipped = true; }
        public virtual void WhileEquipped() { }
        public virtual void OnUnequip() { equipped = false; }

        public virtual void OnAttack() { }
        public virtual void Attacking() { }
        public virtual void OnAttackEnd() { }
    }
}