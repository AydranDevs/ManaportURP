using UnityEngine;

public struct ItemMetadata
{
    public Sprite sprite;
    public string name;
    public string category;
    public string lore;

    public bool stackable;
}

public class ItemMetadataManager
{
    public ItemMetadata GetMetaData(ItemID id)
    {
        switch (id)
        {
            default:
            case ItemID.manaport_consumable_restore_mana_potion_I:      return manaport_consumable_restore_mana_potion_I;
            case ItemID.manaport_consumable_restore_mana_potion_II:     return manaport_consumable_restore_mana_potion_II;
            case ItemID.manaport_consumable_restore_mana_potion_III:    return manaport_consumable_restore_mana_potion_III;
            case ItemID.manaport_consumable_restore_health_potion_I:    return manaport_consumable_restore_health_potion_I;
            case ItemID.manaport_consumable_restore_health_potion_II:   return manaport_consumable_restore_health_potion_II;
            case ItemID.manaport_consumable_restore_health_potion_III:  return manaport_consumable_restore_health_potion_III;
            case ItemID.manaport_consumable_regen_mana_potion_I:        return manaport_consumable_regen_mana_potion_I;
            case ItemID.manaport_consumable_regen_mana_potion_II:       return manaport_consumable_regen_mana_potion_II;
            case ItemID.manaport_consumable_regen_mana_potion_III:      return manaport_consumable_regen_mana_potion_III;
            case ItemID.manaport_consumable_regen_health_potion_I:      return manaport_consumable_regen_health_potion_I;
            case ItemID.manaport_consumable_regen_health_potion_II:     return manaport_consumable_regen_health_potion_II;
            case ItemID.manaport_consumable_regen_health_potion_III:    return manaport_consumable_regen_health_potion_III;
            case ItemID.manaport_consumable_boost_pyro_potion_I:        return manaport_consumable_boost_pyro_potion_I;
            case ItemID.manaport_consumable_boost_pyro_potion_II:       return manaport_consumable_boost_pyro_potion_II;
            case ItemID.manaport_consumable_boost_pyro_potion_III:      return manaport_consumable_boost_pyro_potion_III;
            case ItemID.manaport_consumable_boost_cryo_potion_I:        return manaport_consumable_boost_cryo_potion_I;
            case ItemID.manaport_consumable_boost_cryo_potion_II:       return manaport_consumable_boost_cryo_potion_II;
            case ItemID.manaport_consumable_boost_cryo_potion_III:      return manaport_consumable_boost_cryo_potion_III;
            case ItemID.manaport_consumable_boost_toxi_potion_I:        return manaport_consumable_boost_toxi_potion_I;
            case ItemID.manaport_consumable_boost_toxi_potion_II:       return manaport_consumable_boost_toxi_potion_II;
            case ItemID.manaport_consumable_boost_toxi_potion_III:      return manaport_consumable_boost_toxi_potion_III;
            case ItemID.manaport_consumable_boost_volt_potion_I:        return manaport_consumable_boost_volt_potion_I;
            case ItemID.manaport_consumable_boost_volt_potion_II:       return manaport_consumable_boost_volt_potion_II;
            case ItemID.manaport_consumable_boost_volt_potion_III:      return manaport_consumable_boost_volt_potion_III;
            case ItemID.manaport_consumable_invincibility_potion_I:     return manaport_consumable_invincibility_potion_I;
            case ItemID.manaport_consumable_invincibility_potion_II:    return manaport_consumable_invincibility_potion_II;
            case ItemID.manaport_consumable_invincibility_potion_III:   return manaport_consumable_invincibility_potion_III;
            case ItemID.manaport_consumable_absorption_potion_I:        return manaport_consumable_absorption_potion_I;
            case ItemID.manaport_consumable_absorption_potion_II:       return manaport_consumable_absorption_potion_II;
            case ItemID.manaport_consumable_absorption_potion_III:      return manaport_consumable_absorption_potion_III;
            case ItemID.manaport_consumable_rage_potion_I:              return manaport_consumable_rage_potion_I;
            case ItemID.manaport_consumable_rage_potion_II:             return manaport_consumable_rage_potion_II;
            case ItemID.manaport_consumable_rage_potion_III:            return manaport_consumable_rage_potion_III;
            case ItemID.manaport_weapon_training_tome:                  return manaport_weapon_training_tome;
            case ItemID.manaport_weapon_azure_parasol:                  return manaport_weapon_azure_parasol;
            case ItemID.manaport_weapon_garden_shear:                   return manaport_weapon_garden_shear;
            case ItemID.manaport_armour_violet_robes:                   return manaport_armour_violet_robes;
            case ItemID.manaport_armour_timid_raincoat:                 return manaport_armour_timid_raincoat;
            case ItemID.manaport_armour_sunbeaten_overalls:             return manaport_armour_sunbeaten_overalls;
            case ItemID.manaport_vanity_gold_ring_hat:                  return manaport_vanity_gold_ring_hat;
            case ItemID.manaport_vanity_cutesy_blue_rainboots:          return manaport_vanity_cutesy_blue_rainboots;
            case ItemID.manaport_vanity_scarlet_rings:                  return manaport_vanity_scarlet_rings;

        }
    }

    public ItemMetadata manaport_consumable_restore_mana_potion_I = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_restore_mana_potion_I,
        name = "Potion of Mana Restoration I",
        category = ItemCategories.Consumable,
        lore = "Restores mana.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_restore_mana_potion_II = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_restore_mana_potion_II,
        name = "Potion of Mana Restoration II",
        category = ItemCategories.Consumable,
        lore = "Restores mana.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_restore_mana_potion_III = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_restore_mana_potion_III,
        name = "Potion of Mana Restoration III",
        category = ItemCategories.Consumable,
        lore = "Restores mana.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_restore_health_potion_I = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_restore_health_potion_I,
        name = "Potion of Health Restoration I",
        category = ItemCategories.Consumable,
        lore = "Restores health.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_restore_health_potion_II = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_restore_health_potion_II,
        name = "Potion of Health Restoration II",
        category = ItemCategories.Consumable,
        lore = "Restores health.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_restore_health_potion_III = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_restore_health_potion_III,
        name = "Potion of Health Restoration III",
        category = ItemCategories.Consumable,
        lore = "Restores health.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_regen_mana_potion_I = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_regen_mana_potion_I,
        name = "Potion of Health Regeneration I",
        category = ItemCategories.Consumable,
        lore = "Increases the mana regeneration rate.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_regen_mana_potion_II = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_regen_mana_potion_II,
        name = "Potion of Health Regeneration II",
        category = ItemCategories.Consumable,
        lore = "Increases the mana regeneration rate.",
        
        stackable = true
    };
    public ItemMetadata manaport_consumable_regen_mana_potion_III = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_regen_mana_potion_III,
        name = "Potion of Health Regeneration III",
        category = ItemCategories.Consumable,
        lore = "Increases the mana regeneration rate.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_regen_health_potion_I = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_regen_health_potion_I,
        name = "Potion of Mana Regeneration I",
        category = "Consumbale",
        lore = "Increases the health regeneration rate.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_regen_health_potion_II = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_regen_health_potion_II,
        name = "Potion of Mana Regeneration II",
        category = ItemCategories.Consumable,
        lore = "Increases the health regeneration rate.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_regen_health_potion_III = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_regen_health_potion_III,
        name = "Potion of Mana Regeneration III",
        category = ItemCategories.Consumable,
        lore = "Increases the health regeneration rate.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_pyro_potion_I = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_pyro_potion_I,
        name = "Potion of Pyrodominance I",
        category = ItemCategories.Consumable,
        lore = "Increases Pyro damage. \n\n'MMMMPH! MMPH!'",

        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_pyro_potion_II = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_pyro_potion_II,
        name = "Potion of Pyrodominance II",
        category = ItemCategories.Consumable,
        lore = "Increases Pyro damage. \n\n'MMMMPH! MMPH!'",

        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_pyro_potion_III = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_pyro_potion_III,
        name = "Potion of Pyrodominance III",
        category = ItemCategories.Consumable,
        lore = "Increases Pyro damage. \n\n'MMMMPH! MMPH!'",

        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_cryo_potion_I = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_cryo_potion_I,
        name = "Potion of Cryodominance I",
        category = ItemCategories.Consumable,
        lore = "Increases Cryo damage.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_cryo_potion_II = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_cryo_potion_II,
        name = "Potion of Cryodominance II",
        category = ItemCategories.Consumable,
        lore = "Increases Cryo damage.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_cryo_potion_III = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_cryo_potion_III,
        name = "Potion of Cryodominance III",
        category = ItemCategories.Consumable,
        lore = "Increases Cryo damage.",
        
        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_toxi_potion_I = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_toxi_potion_I,
        name = "Potion of Toxidominance I",
        category = ItemCategories.Consumable,
        lore = "Increases Toxi damage.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_toxi_potion_II = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_toxi_potion_II,
        name = "Potion of Toxidominance II",
        category = ItemCategories.Consumable,
        lore = "Increases Toxi damage.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_toxi_potion_III = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_toxi_potion_III,
        name = "Potion of Toxidominance III",
        category = ItemCategories.Consumable,
        lore = "Increases Toxi damage.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_volt_potion_I = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_volt_potion_I,
        name = "Potion of Voltdominance I",
        category = ItemCategories.Consumable,
        lore = "Increases Volt damage.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_volt_potion_II = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_volt_potion_II,
        name = "Potion of Voltdominance II",
        category = ItemCategories.Consumable,
        lore = "Increases Volt damage.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_boost_volt_potion_III = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_boost_volt_potion_III,
        name = "Potion of Voltdominance III",
        category = ItemCategories.Consumable,
        lore = "Increases Volt damage.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_invincibility_potion_I = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_invincibility_potion_I,
        name = "Potion of Invincibility I",
        category = ItemCategories.Consumable,
        lore = "Grants invulnerability to all damage. \n\n'I AM BOOLETT-PROOOOOOOOOOOOOOOOOOOF!!!'",

        stackable = true
    };
    public ItemMetadata manaport_consumable_invincibility_potion_II = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_invincibility_potion_II,
        name = "Potion of Invincibility II",
        category = ItemCategories.Consumable,
        lore = "Grants invulnerability to all damage. \n\n'I AM BOOLETT-PROOOOOOOOOOOOOOOOOOOF!!!'",

        stackable = true
    };
    public ItemMetadata manaport_consumable_invincibility_potion_III = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_invincibility_potion_III,
        name = "Potion of Invincibility III",
        category = ItemCategories.Consumable,
        lore = "Grants invulnerability to all damage. \n\n'I AM BOOLETT-PROOOOOOOOOOOOOOOOOOOF!!!'",

        stackable = true
    };
    public ItemMetadata manaport_consumable_absorption_potion_I = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_absorption_potion_I,
        name = "Potion of Absorption I",
        category = ItemCategories.Consumable,
        lore = "Allows the absorption of all damage. \n\n'Raus, Raus! I am ze Ubermensch!'",

        stackable = true
    };
    public ItemMetadata manaport_consumable_absorption_potion_II = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_absorption_potion_II,
        name = "Potion of Absorption II",
        category = ItemCategories.Consumable,
        lore = "Allows the absorption of all damage. \n\n'Raus, Raus! I am ze Ubermensch!'",

        stackable = true
    };
    public ItemMetadata manaport_consumable_absorption_potion_III = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_absorption_potion_III,
        name = "Potion of Absorption III",
        category = ItemCategories.Consumable,
        lore = "Allows the absorption of all damage. \n\n'Raus, Raus! I am ze Ubermensch!'",

        stackable = true
    };
    public ItemMetadata manaport_consumable_rage_potion_I = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_rage_potion_I,
        name = "Potion of Pure Rage I",
        category = ItemCategories.Consumable,
        lore = "Rip and tear until it is done.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_rage_potion_II = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_rage_potion_II,
        name = "Potion of Pure Rage II",
        category = ItemCategories.Consumable,
        lore = "Rip and tear until it is done.",

        stackable = true
    };
    public ItemMetadata manaport_consumable_rage_potion_III = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_consumable_rage_potion_III,
        name = "Potion of Pure Rage III",
        category = ItemCategories.Consumable,
        lore = "Rip and tear until it is done.",

        stackable = true
    };
    public ItemMetadata manaport_weapon_training_tome = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_weapon_training_tome,
        name = "MPUAA Issued Trainee Spelltome",
        category = ItemCategories.Weapon,
        lore = "a Spelltome issued by Manaport Upper Arcane Academy students for use in training.",

        stackable = false
    };
    public ItemMetadata manaport_weapon_azure_parasol = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_weapon_azure_parasol,
        name = "Azure Parasol",
        category = ItemCategories.Weapon,
        lore = "A Parasol handmade by Mirabelle's father. The last gift she got from him.",

        stackable = false
    };
    public ItemMetadata manaport_weapon_garden_shear = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_weapon_garden_shear,
        name = "Garden Shear",
        category = ItemCategories.Weapon,
        lore = "What was once an old, rusty pair of garden shears is now an old, rusty blade.",

        stackable = false
    };
    public ItemMetadata manaport_armour_violet_robes = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_armour_violet_robes,
        name = "Violet Robes",
        category = ItemCategories.Armour,
        lore = "A modified academy uniform. Laurie's favourite shade of purple.",

        stackable = false
    };
    public ItemMetadata manaport_armour_timid_raincoat = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_armour_timid_raincoat,
        name = "Timid Raincoat",
        category = ItemCategories.Armour,
        lore = "It used to be yellow. It rains so much in the mountains that the rubber has lost its colour.",

        stackable = false
    };
    public ItemMetadata manaport_armour_sunbeaten_overalls = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_armour_sunbeaten_overalls,
        name = "Sunbeaten Overalls",
        category = ItemCategories.Armour,
        lore = "It's warm to the touch, like the sun's energy had been absorbed into the fabric itself.",

        stackable = false
    };
    public ItemMetadata manaport_vanity_gold_ring_hat = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_vanity_gold_ring_hat,
        name = "Gold Ring Hat",
        category = ItemCategories.Vanity,
        lore = "It's rare to see Laurie without this.",

        stackable = false
    };
    public ItemMetadata manaport_vanity_cutesy_blue_rainboots = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_vanity_cutesy_blue_rainboots,
        name = "Cutesy Blue Rainboots",
        category = ItemCategories.Vanity,
        lore = "Despite what they've been through, they're still as bright as ever.",

        stackable = false
    };
    public ItemMetadata manaport_vanity_scarlet_rings = new ItemMetadata
    {
        sprite = ItemAssets.Instance.manaport_vanity_scarlet_rings,
        name = "Scarlet Rings",
        category = ItemCategories.Vanity,
        lore = "Still holding strong after so many years.",

        stackable = false
    };
}
