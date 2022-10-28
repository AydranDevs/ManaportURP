using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.Equipables;
using Manapotion.PartySystem;

namespace Manapotion.Attacking
{
    public class Attack 
    {
        private PartyMember _member;
        public EquipableData weapon; // weapon to attack with

        public float attackCost; // stamina or mana
        public float attackRate; // num of attacks/sec
        public float attackRange; 

        public enum AttackType
        {
            magic,
            physical
        }

        public Attack(PartyMember partyMember)
        {
            _member = partyMember;
            weapon = _member.equipmentScriptableObject.weapon;
            Debug.Log(weapon.equipableID);
        }

        public void DoAttack()
        {

        }
    }
}
