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
    public struct BuffParticles {
        public GameObject rejuvenatedBuffParticles;
    }

    public class PartyMember : MonoBehaviour {
        public PartyMemberState partyMemberState;
        public BuffParticles buffParticles;

        public List<Buff> buffs;

        // health point values
        public float hitPointsMax = 20f;
        public Stat hitPoints;

        // experience point values
        public float xpLevel;
        public float xpMax;
        public float xp;

        public Stat attackDamage;
        public Stat attackSpeed;


        public void Start() {
            ManaBehaviour.OnUpdate += Update;

            InitStats();
            MaxHP();

            buffs = new List<Buff>();
        }

        private void InitStats() {
            hitPoints = new Stat(hitPointsMax, hitPointsMax, PartyStats.Health);

            attackDamage = new Stat(PartyStats.AttackDamage);
            attackSpeed = new Stat(PartyStats.AttackSpeed);
        }

        public void MaxHP() {
            hitPoints.Max();
        }

        public void AddBuff(StatusEffect effect, int power, float duration) {
            var buffToAdd = new Buff(effect, power, duration);

            for (int i = 0; i < buffs.Count; i++) {
                if (buffs[i].effect == buffToAdd.effect) {
                    buffs[i].duration = duration;
                    return;
                }
            }

            buffs.Add(buffToAdd);
            buffToAdd.active = true;
            buffToAdd.Init(this);
            StatusEffectsUIHandler.Instance.AddStatus(this.gameObject, buffToAdd);
        }

        void Update() {
            foreach (var buff in buffs) {
                if (buff.active == false) {
                    StatusEffectsUIHandler.Instance.RemoveStatus(this.gameObject, buff);
                    buffs.Remove(buff);
                }
            }
        }

        public GameObject SummonParticles(GameObject go, Transform parent) {
            return Instantiate(go, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity, parent);
        }
    }
}
