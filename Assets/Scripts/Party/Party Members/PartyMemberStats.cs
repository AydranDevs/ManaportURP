using Manapotion.Status;

namespace Manapotion.PartySystem
{
    [System.Serializable]
    public class PartyMemberStats
    {
        #region HP
        public Stat manaport_stat_hitpoints;
        public Stat manaport_stat_max_hitpoints;
        public Stat manaport_stat_hitpoints_regen_rate;
        public Stat manaport_stat_hitpoints_regen_amount;
        #endregion
        #region MP
        public Stat manaport_stat_manapoints;
        public Stat manaport_stat_max_manapoints;
        public Stat manaport_stat_manapoints_regen_rate;
        public Stat manaport_stat_manapoints_regen_amount;
        #endregion
        #region SP
        public Stat manaport_stat_staminapoints;
        public Stat manaport_stat_max_staminapoints;
        public Stat manaport_stat_staminapoints_regen_rate;
        public Stat manaport_stat_staminapoints_regen_amount;
        #endregion
        #region RP
        public Stat manaport_stat_remedypoints;
        public Stat manaport_stat_max_remedypoints;
        #endregion
        public Stat manaport_stat_experience_points;
        public Stat manaport_stat_max_experience_points;
        public Stat manaport_stat_experience_level;
        public Stat manaport_stat_base_magical_damage;
        public Stat manaport_stat_base_magical_speed;
        public Stat manaport_stat_base_healing_rate;
        public Stat manaport_stat_base_healing_amount;
        public Stat manaport_stat_base_physical_damage;
        public Stat manaport_stat_base_physical_speed;
        public Stat manaport_stat_base_defence;
        public Stat manaport_stat_base_pyro_resistance;
        public Stat manaport_stat_base_cryo_resistance;
        public Stat manaport_stat_base_toxi_resistance;
        public Stat manaport_stat_base_volt_resistance;
        public Stat manaport_stat_base_arcane_resistance;
        public Stat manaport_stat_base_stress_resistance;
        public Stat manaport_stat_base_stability;
        public Stat manaport_stat_base_push_speed;
        public Stat manaport_stat_base_walk_speed;
        public Stat manaport_stat_base_sprint_modifier;
        public Stat manaport_stat_base_dash_modifier;
        public Stat manaport_stat_ability_distance;
        public Stat manaport_stat_ability_cooldown;
    }
}
