using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    public static ItemMetadataManager itemMetadataManager { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        itemMetadataManager = new ItemMetadataManager();
    }

    public Transform pfWorldItem;

    public Sprite manaport_consumable_restore_mana_potion_I,
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
        manaport_vanity_scarlet_rings;
}
