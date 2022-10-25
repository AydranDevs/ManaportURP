using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;

namespace Manapotion.Status {
    
    // public enum StatID 
    // {
    //     manaport_stat_hitpoints,
    //     manaport_stat_max_hitpoints,
    //     manaport_stat_hitpoints_regen_rate,
    //     manaport_stat_hitpoints_regen_amount,
    //     manaport_stat_manapoints,
    //     manaport_stat_max_manapoints,
    //     manaport_stat_manapoints_regen_rate,
    //     manaport_stat_manapoints_regen_amount,
    //     manaport_stat_experience_points,
    //     manaport_stat_max_experience_points,
    //     manaport_stat_experience_level,
    //     manaport_stat_base_magical_damage,
    //     manaport_stat_base_magical_speed,
    //     manaport_stat_base_healing_rate,
    //     manaport_stat_base_healing_amount,
    //     manaport_stat_base_physical_damage,
    //     manaport_stat_base_physical_speed,
    //     manaport_stat_base_defence,
    //     manaport_stat_base_pyro_resistance,
    //     manaport_stat_base_cryo_resistance,
    //     manaport_stat_base_toxi_resistance,
    //     manaport_stat_base_volt_resistance,
    //     manaport_stat_base_arcane_resistance,
    //     manaport_stat_base_stress_resistance,
    //     manaport_stat_base_stability,
    //     manaport_stat_base_push_speed,
    //     manaport_stat_base_walk_speed,
    //     manaport_stat_base_sprint_modifier,
    //     manaport_stat_base_dash_modifier,
    //     manaport_stat_ability_distance,
    //     manaport_stat_ability_cooldown,
    // }

    /*
    class for managing a stat's info
    */
    [System.Serializable]
    public class Stat {
        // public StatID id;
        public float maxValue;
        public float value;

        // public Stat(StatID id) {
        //     this.id = id;
        // }

        public Stat(float value, float maxValue) // , StatID id)
        {
            this.value = value;
            this.maxValue = maxValue;
            // this.id = id;
        }

        public void Max()
        {
            value = maxValue;
        }

        public void Min()
        {
            value = 0f;
        }

        public float GetValue()
        {
            return value;
        }

        public float GetMaxValue()
        {
            return maxValue;
        }

        public void SetMaxValue(float num)
        {
            maxValue = num;
        }

        public bool Empty()
        {
            return value == 0f;
        }
    }
}

