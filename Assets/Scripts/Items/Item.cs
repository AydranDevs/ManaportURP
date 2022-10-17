using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemCategories
{
    public const string Consumable = "Consumable";
    public const string Ingredient = "Ingredient";
    public const string Material = "Material";
    public const string Armour = "Armour";
    public const string Weapon = "Weapon";
    public const string Vanity = "Vanity";
    public const string Spellstone = "Spellstone";
}

public enum ItemID
    {
        manaport_nothing,
        manaport_consumable_restore_mana_potion_I,
        manaport_consumable_restore_mana_potion_II,
        manaport_consumable_restore_mana_potion_III,
        manaport_consumable_restore_health_potion_I,
        manaport_consumable_restore_health_potion_II,
        manaport_consumable_restore_health_potion_III,
        manaport_consumable_regen_mana_potion_I,
        manaport_consumable_regen_mana_potion_II,
        manaport_consumable_regen_mana_potion_III,
        manaport_consumable_regen_health_potion_I,
        manaport_consumable_regen_health_potion_II,
        manaport_consumable_regen_health_potion_III,
        manaport_consumable_boost_pyro_potion_I,
        manaport_consumable_boost_pyro_potion_II,
        manaport_consumable_boost_pyro_potion_III,
        manaport_consumable_boost_cryo_potion_I,
        manaport_consumable_boost_cryo_potion_II,
        manaport_consumable_boost_cryo_potion_III,
        manaport_consumable_boost_toxi_potion_I,
        manaport_consumable_boost_toxi_potion_II,
        manaport_consumable_boost_toxi_potion_III,
        manaport_consumable_boost_volt_potion_I,
        manaport_consumable_boost_volt_potion_II,
        manaport_consumable_boost_volt_potion_III,
        manaport_consumable_invincibility_potion_I,
        manaport_consumable_invincibility_potion_II,
        manaport_consumable_invincibility_potion_III,
        manaport_consumable_absorption_potion_I,
        manaport_consumable_absorption_potion_II,
        manaport_consumable_absorption_potion_III,
        manaport_consumable_rage_potion_I,
        manaport_consumable_rage_potion_II,
        manaport_consumable_rage_potion_III,
        manaport_weapon_training_tome,
        manaport_weapon_azure_parasol,
        manaport_weapon_garden_shear,
        manaport_armour_violet_robes,
        manaport_armour_timid_raincoat,
        manaport_armour_sunbeaten_overalls,
        manaport_vanity_gold_ring_hat,
        manaport_vanity_cutesy_blue_rainboots,
        manaport_vanity_scarlet_rings,
        manaport_spellstone_pyromancing_stone,
        manaport_spellstone_cryomancing_stone,
        manaport_spellstone_toximancing_stone,
        manaport_spellstone_voltmancing_stone
    }

[Serializable]
public class Item
{
    public ItemID itemID;
    public int amount;

    public ItemMetadata GetMetadata()
    {
        return ItemAssets.itemMetadataManager.GetMetaData(this.itemID);
    }
}
