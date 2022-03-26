using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDamageable {
    void Damage(float damage, float critDamage, bool crit, bool status, string statusType, float dps, bool castByPlayer);
    void Die();
    void DamageOverTime();
}
