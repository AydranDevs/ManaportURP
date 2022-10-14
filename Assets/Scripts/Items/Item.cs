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
        manaport_vanity_scarlet_rings
    }

[Serializable]
public class Item
{
    public ItemID itemID;
    public int amount;

    #region Get
    // /// <summary>
    // /// Returns the Sprite for the item ID.
    // /// </summary>
    // /// <returns></returns>
    // public Sprite GetSprite()
    // {
    //     switch (itemID)
    //     {
    //         default:
    //         case ItemID.manaport_consumable_restore_mana_potion_I:      return ItemAssets.Instance.manaport_consumable_restore_mana_potion_I;
    //         case ItemID.manaport_consumable_restore_mana_potion_II:     return ItemAssets.Instance.manaport_consumable_restore_mana_potion_II;
    //         case ItemID.manaport_consumable_restore_mana_potion_III:    return ItemAssets.Instance.manaport_consumable_restore_mana_potion_III;
    //         case ItemID.manaport_consumable_restore_health_potion_I:    return ItemAssets.Instance.manaport_consumable_restore_health_potion_I;
    //         case ItemID.manaport_consumable_restore_health_potion_II:   return ItemAssets.Instance.manaport_consumable_restore_health_potion_II;
    //         case ItemID.manaport_consumable_restore_health_potion_III:  return ItemAssets.Instance.manaport_consumable_restore_health_potion_III;
    //         case ItemID.manaport_consumable_regen_mana_potion_I:        return ItemAssets.Instance.manaport_consumable_regen_mana_potion_I;
    //         case ItemID.manaport_consumable_regen_mana_potion_II:       return ItemAssets.Instance.manaport_consumable_regen_mana_potion_II;
    //         case ItemID.manaport_consumable_regen_mana_potion_III:      return ItemAssets.Instance.manaport_consumable_regen_mana_potion_III;
    //         case ItemID.manaport_consumable_regen_health_potion_I:      return ItemAssets.Instance.manaport_consumable_regen_health_potion_I;
    //         case ItemID.manaport_consumable_regen_health_potion_II:     return ItemAssets.Instance.manaport_consumable_regen_health_potion_II;
    //         case ItemID.manaport_consumable_regen_health_potion_III:    return ItemAssets.Instance.manaport_consumable_regen_health_potion_III;
    //         case ItemID.manaport_consumable_boost_pyro_potion_I:        return ItemAssets.Instance.manaport_consumable_boost_pyro_potion_I;
    //         case ItemID.manaport_consumable_boost_pyro_potion_II:       return ItemAssets.Instance.manaport_consumable_boost_pyro_potion_II;
    //         case ItemID.manaport_consumable_boost_pyro_potion_III:      return ItemAssets.Instance.manaport_consumable_boost_pyro_potion_III;
    //         case ItemID.manaport_consumable_boost_cryo_potion_I:        return ItemAssets.Instance.manaport_consumable_boost_cryo_potion_I;
    //         case ItemID.manaport_consumable_boost_cryo_potion_II:       return ItemAssets.Instance.manaport_consumable_boost_cryo_potion_II;
    //         case ItemID.manaport_consumable_boost_cryo_potion_III:      return ItemAssets.Instance.manaport_consumable_boost_cryo_potion_III;
    //         case ItemID.manaport_consumable_boost_toxi_potion_I:        return ItemAssets.Instance.manaport_consumable_boost_toxi_potion_I;
    //         case ItemID.manaport_consumable_boost_toxi_potion_II:       return ItemAssets.Instance.manaport_consumable_boost_toxi_potion_II;
    //         case ItemID.manaport_consumable_boost_toxi_potion_III:      return ItemAssets.Instance.manaport_consumable_boost_toxi_potion_III;
    //         case ItemID.manaport_consumable_boost_volt_potion_I:        return ItemAssets.Instance.manaport_consumable_boost_volt_potion_I;
    //         case ItemID.manaport_consumable_boost_volt_potion_II:       return ItemAssets.Instance.manaport_consumable_boost_volt_potion_II;
    //         case ItemID.manaport_consumable_boost_volt_potion_III:      return ItemAssets.Instance.manaport_consumable_boost_volt_potion_III;
    //         case ItemID.manaport_consumable_invincibility_potion_I:     return ItemAssets.Instance.manaport_consumable_invincibility_potion_I;
    //         case ItemID.manaport_consumable_invincibility_potion_II:    return ItemAssets.Instance.manaport_consumable_invincibility_potion_II;
    //         case ItemID.manaport_consumable_invincibility_potion_III:   return ItemAssets.Instance.manaport_consumable_invincibility_potion_III;
    //         case ItemID.manaport_consumable_absorption_potion_I:        return ItemAssets.Instance.manaport_consumable_absorption_potion_I;
    //         case ItemID.manaport_consumable_absorption_potion_II:       return ItemAssets.Instance.manaport_consumable_absorption_potion_II;
    //         case ItemID.manaport_consumable_absorption_potion_III:      return ItemAssets.Instance.manaport_consumable_absorption_potion_III;
    //         case ItemID.manaport_consumable_rage_potion_I:              return ItemAssets.Instance.manaport_consumable_rage_potion_I;
    //         case ItemID.manaport_consumable_rage_potion_II:             return ItemAssets.Instance.manaport_consumable_rage_potion_II;
    //         case ItemID.manaport_consumable_rage_potion_III:            return ItemAssets.Instance.manaport_consumable_rage_potion_III;
    //         case ItemID.manaport_weapon_training_tome:                  return ItemAssets.Instance.manaport_weapon_training_tome;
    //         case ItemID.manaport_weapon_azure_parasol:                  return ItemAssets.Instance.manaport_weapon_azure_parasol;
    //         case ItemID.manaport_weapon_garden_shear:                   return ItemAssets.Instance.manaport_weapon_garden_shear;
    //         case ItemID.manaport_armour_violet_robes:                   return ItemAssets.Instance.manaport_armour_violet_robes;
    //         case ItemID.manaport_armour_timid_raincoat:                 return ItemAssets.Instance.manaport_armour_timid_raincoat;
    //         case ItemID.manaport_armour_sunbeaten_overalls:             return ItemAssets.Instance.manaport_armour_sunbeaten_overalls;
    //         case ItemID.manaport_vanity_gold_ring_hat:                  return ItemAssets.Instance.manaport_vanity_gold_ring_hat;
    //         case ItemID.manaport_vanity_cutesy_blue_rainboots:          return ItemAssets.Instance.manaport_vanity_cutesy_blue_rainboots;
    //         case ItemID.manaport_vanity_scarlet_rings:                  return ItemAssets.Instance.manaport_vanity_scarlet_rings;
    //     }
    // }

    // /// <summary>
    // /// Returns a fixed name for the item ID.
    // /// </summary>
    // /// <returns></returns>
    // public string GetItemName()
    // {
    //     List<string> fixedStrings = new List<string>();

    //     string[] splitAtUnderScores = itemID.ToString().Split('_');
    //     for (int i = 2; i < splitAtUnderScores.Length; i++)
    //     {
    //         string unfixed = splitAtUnderScores[i];
    //         string str = char.ToUpper(unfixed[0]) + unfixed.Substring(1);
    //         fixedStrings.Add(str);
    //     }

    //     StringBuilder sb = new StringBuilder("", 100);
    //     for (int i = 0; i < fixedStrings.Count; i++)
    //     {
    //         sb.Append(fixedStrings[i] + " ");
    //     }

    //     string final = sb.ToString();

    //     return final;
    // }

    // public string GetItemCategory()
    // {
    //     string[] splitAtUnderScores = itemID.ToString().Split('_');
    //     string unfixedCat = splitAtUnderScores[1];
    //     string capitalized = char.ToUpper(unfixedCat[0]) + unfixedCat.Substring(1);

    //     return capitalized;
    // }
    #endregion

    #region Is
    // /// <summary>
    // /// Returns true if the item is stackable.
    // /// </summary>
    // /// <returns></returns>
    // public bool IsStackable()
    // {
    //     switch (itemID)
    //     {
    //         default:
    //         case ItemID.manaport_consumable_restore_mana_potion_I:
    //         case ItemID.manaport_consumable_restore_mana_potion_II:
    //         case ItemID.manaport_consumable_restore_mana_potion_III:
    //         case ItemID.manaport_consumable_restore_health_potion_I:
    //         case ItemID.manaport_consumable_restore_health_potion_II:
    //         case ItemID.manaport_consumable_restore_health_potion_III:
    //         case ItemID.manaport_consumable_regen_mana_potion_I:
    //         case ItemID.manaport_consumable_regen_mana_potion_II:
    //         case ItemID.manaport_consumable_regen_mana_potion_III:
    //         case ItemID.manaport_consumable_regen_health_potion_I:
    //         case ItemID.manaport_consumable_regen_health_potion_II:
    //         case ItemID.manaport_consumable_regen_health_potion_III:
    //         case ItemID.manaport_consumable_boost_pyro_potion_I:
    //         case ItemID.manaport_consumable_boost_pyro_potion_II:
    //         case ItemID.manaport_consumable_boost_pyro_potion_III:
    //         case ItemID.manaport_consumable_boost_cryo_potion_I:
    //         case ItemID.manaport_consumable_boost_cryo_potion_II:
    //         case ItemID.manaport_consumable_boost_cryo_potion_III:
    //         case ItemID.manaport_consumable_boost_toxi_potion_I:
    //         case ItemID.manaport_consumable_boost_toxi_potion_II:
    //         case ItemID.manaport_consumable_boost_toxi_potion_III:
    //         case ItemID.manaport_consumable_boost_volt_potion_I:
    //         case ItemID.manaport_consumable_boost_volt_potion_II:
    //         case ItemID.manaport_consumable_boost_volt_potion_III:
    //         case ItemID.manaport_consumable_invincibility_potion_I:
    //         case ItemID.manaport_consumable_invincibility_potion_II:
    //         case ItemID.manaport_consumable_invincibility_potion_III:
    //         case ItemID.manaport_consumable_absorption_potion_I:
    //         case ItemID.manaport_consumable_absorption_potion_II:
    //         case ItemID.manaport_consumable_absorption_potion_III:
    //         case ItemID.manaport_consumable_rage_potion_I:
    //         case ItemID.manaport_consumable_rage_potion_II:
    //         case ItemID.manaport_consumable_rage_potion_III:
    //             return true;
    //         case ItemID.manaport_weapon_training_tome:
    //         case ItemID.manaport_weapon_azure_parasol:
    //         case ItemID.manaport_weapon_garden_shear:
    //         case ItemID.manaport_armour_violet_robes:
    //         case ItemID.manaport_armour_timid_raincoat:
    //         case ItemID.manaport_armour_sunbeaten_overalls:
    //         case ItemID.manaport_vanity_gold_ring_hat:
    //         case ItemID.manaport_vanity_cutesy_blue_rainboots:
    //         case ItemID.manaport_vanity_scarlet_rings:
    //             return false;
    //     }
    // }

    // /// <summary>
    // /// Returns true if the item is equipable.
    // /// </summary>
    // /// <returns></returns>
    // public bool IsEquipable()
    // {
    //     switch (itemID)
    //     {
    //         default:
    //         case ItemID.manaport_consumable_restore_mana_potion_I:
    //         case ItemID.manaport_consumable_restore_mana_potion_II:
    //         case ItemID.manaport_consumable_restore_mana_potion_III:
    //         case ItemID.manaport_consumable_restore_health_potion_I:
    //         case ItemID.manaport_consumable_restore_health_potion_II:
    //         case ItemID.manaport_consumable_restore_health_potion_III:
    //         case ItemID.manaport_consumable_regen_mana_potion_I:
    //         case ItemID.manaport_consumable_regen_mana_potion_II:
    //         case ItemID.manaport_consumable_regen_mana_potion_III:
    //         case ItemID.manaport_consumable_regen_health_potion_I:
    //         case ItemID.manaport_consumable_regen_health_potion_II:
    //         case ItemID.manaport_consumable_regen_health_potion_III:
    //         case ItemID.manaport_consumable_boost_pyro_potion_I:
    //         case ItemID.manaport_consumable_boost_pyro_potion_II:
    //         case ItemID.manaport_consumable_boost_pyro_potion_III:
    //         case ItemID.manaport_consumable_boost_cryo_potion_I:
    //         case ItemID.manaport_consumable_boost_cryo_potion_II:
    //         case ItemID.manaport_consumable_boost_cryo_potion_III:
    //         case ItemID.manaport_consumable_boost_toxi_potion_I:
    //         case ItemID.manaport_consumable_boost_toxi_potion_II:
    //         case ItemID.manaport_consumable_boost_toxi_potion_III:
    //         case ItemID.manaport_consumable_boost_volt_potion_I:
    //         case ItemID.manaport_consumable_boost_volt_potion_II:
    //         case ItemID.manaport_consumable_boost_volt_potion_III:
    //         case ItemID.manaport_consumable_invincibility_potion_I:
    //         case ItemID.manaport_consumable_invincibility_potion_II:
    //         case ItemID.manaport_consumable_invincibility_potion_III:
    //         case ItemID.manaport_consumable_absorption_potion_I:
    //         case ItemID.manaport_consumable_absorption_potion_II:
    //         case ItemID.manaport_consumable_absorption_potion_III:
    //         case ItemID.manaport_consumable_rage_potion_I:
    //         case ItemID.manaport_consumable_rage_potion_II:
    //         case ItemID.manaport_consumable_rage_potion_III:
    //             return false;
    //         case ItemID.manaport_weapon_training_tome:
    //         case ItemID.manaport_weapon_azure_parasol:
    //         case ItemID.manaport_weapon_garden_shear:
    //         case ItemID.manaport_armour_violet_robes:
    //         case ItemID.manaport_armour_timid_raincoat:
    //         case ItemID.manaport_armour_sunbeaten_overalls:
    //         case ItemID.manaport_vanity_gold_ring_hat:
    //         case ItemID.manaport_vanity_cutesy_blue_rainboots:
    //         case ItemID.manaport_vanity_scarlet_rings:
    //             return true;
    //     }
    // }

    public ItemMetadata GetMetadata()
    {
        return ItemAssets.itemMetadataManager.GetMetaData(this.itemID);
    }
    
    #endregion

    #region Virtual Methods
    public virtual void UseItem()
    {

    }
    #endregion
}
