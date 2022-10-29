using System;
using UnityEngine;

namespace Manapotion.Equipables
{   
    [System.Serializable]
    public class EquipableData
    {
        // public ItemID equipableID;

        // // int array that holds the character ids for characters that can equip this item
        // public int[] charIDsThatCanEquip;

        // // holds the primary and secondary spell stones for the equipabable, if aplicable
        // public Item[] spellstones;
        
        // public EquipableStat[] stats = new EquipableStat[]
        // {
        //     new EquipableStat(EquipableStats.AttackDamage),
        //     new EquipableStat(EquipableStats.AttackSpeed),
        //     new EquipableStat(EquipableStats.CriticalChance),
        //     new EquipableStat(EquipableStats.CriticalStrike),
            
        //     new EquipableStat(EquipableStats.Defense),
        //     new EquipableStat(EquipableStats.KnockbackResistance)
        // };

        // public bool equipped { get; private set; } = false;

        // public void OnEquip()
        // {
        //     equipped = true;
        //     OnEquipEvent?.Invoke(this, new OnEquipEventArgs { equipableID = this.equipableID, stats = this.stats });
        // }
        // public event EventHandler<OnEquipEventArgs> OnEquipEvent;
        // public class OnEquipEventArgs : EventArgs
        // {
        //     public ItemID equipableID;
        //     public EquipableStat[] stats;
        // }
        // public void OnUnequip()
        // {
        //     equipped = false;
        //     OnUnequipEvent?.Invoke(this, new OnUnequipEventArgs { equipableID = this.equipableID });
        // }
        // public event EventHandler<OnUnequipEventArgs> OnUnequipEvent;
        // public class OnUnequipEventArgs : EventArgs
        // {
        //     public ItemID equipableID;
        // }

        // public void OnAttack() { OnAttackStartEvent?.Invoke(this, new OnAttackStartEventArgs { equipableID = this.equipableID, stats = this.stats }); }
        // public event EventHandler<OnAttackStartEventArgs> OnAttackStartEvent;
        // public class OnAttackStartEventArgs : EventArgs
        // {
        //     public ItemID equipableID;
        //     public EquipableStat[] stats;
        // }
        // public void Attacking() { OnAttackingEvent?.Invoke(this, new OnAttackingEventArgs { equipableID = this.equipableID, stats = this.stats }); }
        // public event EventHandler<OnAttackingEventArgs> OnAttackingEvent;
        // public class OnAttackingEventArgs : EventArgs
        // {
        //     public ItemID equipableID;
        //     public EquipableStat[] stats;
        // }
        // public void OnAttackEnd() { OnAttackEndEvent?.Invoke(this, new OnAttackEndEventArgs { equipableID = this.equipableID, stats = this.stats }); }
        // public event EventHandler<OnAttackEndEventArgs> OnAttackEndEvent;
        // public class OnAttackEndEventArgs : EventArgs
        // {
        //     public ItemID equipableID;
        //     public EquipableStat[] stats;
        // }
    
        // public ItemMetadata GetItemIDMetaData()
        // {
        //     return ItemAssets.itemMetadataManager.GetMetaData(equipableID);
        // }
    }
}