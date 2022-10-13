using UnityEngine;

namespace Manapotion.Equipables
{   
    [System.Serializable]
    public class EquipableData
    {
        public ItemID equipableID;
        
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

        public virtual void OnEquip()
        {
            equipped = true;
            Debug.Log("equipped " + equipableID);
        }
        public virtual void WhileEquipped() { }
        public virtual void OnUnequip() { equipped = false; }

        public virtual void OnAttack() { }
        public virtual void Attacking() { }
        public virtual void OnAttackEnd() { }
    }
}