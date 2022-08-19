using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem;

namespace Manapotion.GameDataManagement {
    [System.Serializable]
    public struct CharData {
        // Transform
        public float[] position;
        public int facingState;
        
        // Ability
        public int primaryAttackType;
        public int primaryAttackEffect;
        public int secondaryAttackType;
        public int secondaryAttackEffect;
    }

    [System.Serializable]
    public class GameData {
        // public CharData laurie;
        // public CharData mirabelle;
        // public CharData winsley;

        // public GameData(PartyMember char) {

        // }
    }
}
