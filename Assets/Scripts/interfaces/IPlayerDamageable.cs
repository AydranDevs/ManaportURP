using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerDamageable {
    void Damage(float damage, float critDamage, bool crit, bool status, string statusType, float dps);
    void Die();
    void DamageOverTime();
}
