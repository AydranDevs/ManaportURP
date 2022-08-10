using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.ManaBehaviour;
using Manapotion.StatusEffects;
using Manapotion.Status;

namespace PartyNamespace {
    public enum PartyStats {
        Health,
        Mana,
        XP,
        AttackDamage,
        AttackSpeed
    }
    
    public enum PartyBuffs {
        Rejuvenated, // stat boosts
        Comfortable,
        Cared_For,
        Loved,
        
        Empowered, // increases max values of related stats
        Sociable,
        Nimble,
        Toughened,
        
        Pyro_UP, // boosts elemental damage and resistance
        Cryo_UP,
        Toxi_UP,
        Volt_UP
    }
    
    [System.Serializable]
    public struct StatusEffectParticles {
        public GameObject rejuvenatedBuffParticles;
    }

    public class PartyMember : MonoBehaviour {
        public PartyMemberState partyMemberState;
        public StatusEffectParticles statusEffectParticles;
        [SerializeField] private List<GameObject> _statusEffectParticles;

        public List<Buff> statusEffects;

        // experience point values
        public int xpLevel;
        public float xpMax;
        public float xp;

        // health point values
        public float hitPointsMax = 20f;
        public Stat hitPoints;

        public Stat attackDamage;
        public Stat attackSpeed;

        public void Start() {
            ManaBehaviour.OnUpdate += Update;
            statusEffects = new List<Buff>();
            _statusEffectParticles = new List<GameObject>();

            InitStats();
            MaxHP();
        }

        private void InitStats() {
            hitPoints = new Stat(hitPointsMax, hitPointsMax, PartyStats.Health);

            attackDamage = new Stat(PartyStats.AttackDamage);
            attackSpeed = new Stat(PartyStats.AttackSpeed);
        }

        public void MaxHP() {
            hitPoints.Max();
        }

        public void AddStatusEffect(StatusEffect effect, int power, float duration) {
            var stEf = new Buff(effect, power, duration);

            if (StatusEffectsContains(stEf)) {
                for (int i = 0; i < statusEffects.Count; i++) {
                    if (statusEffects[i].effect.buffType == stEf.effect.buffType) {
                        statusEffects[i].ResetTime();
                        stEf = null;
                        return;
                    }
                }
            }else {
                stEf.active = true;
                stEf.Init(this);
                statusEffects.Add(stEf);
            }
        }

        void Update() {
            // remove all statuses that arent active
            if (statusEffects.Count >= 0 && statusEffects != null) {
                statusEffects.RemoveAll(status => !status.active);
            }
        }

        private bool StatusEffectsContains(Buff effect) {
            bool b = false;

            for (int i = 0; i < statusEffects.Count; i++) {
                if (statusEffects[i].effect.buffType == effect.effect.buffType) {
                    b = true;
                    return b;
                }
            }

            return b;
        }

        public void SummonParticles(GameObject g) {
            var go = GameObject.Instantiate(g, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity, transform);
            _statusEffectParticles.Add(go);
        }

        public void StopParticles(GameObject g) {
            if (_statusEffectParticles.Contains(g)) {
                int i = _statusEffectParticles.IndexOf(g);
                Destroy(_statusEffectParticles[i]);
            }
        }
    }
}
