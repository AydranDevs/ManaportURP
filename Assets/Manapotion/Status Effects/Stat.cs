using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PartyNamespace;

namespace Manapotion.Status {
    /*
    class for managing a stat's info
    */
    public class Stat {
        public PartyStats id;
        public float maxValue;
        public float value;

        public Stat(PartyStats id) {
            this.id = id;
        }

        public Stat(float value, float maxValue, PartyStats id) {
            this.value = value;
            this.maxValue = maxValue;
            this.id = id;
        }

        public void Max() {
            value = maxValue;
        }

        public void Min() {
            value = 0f;
        }

        public float GetValue() {
            return value;
        }

        public float GetMaxValue() {
            return maxValue;
        }

        public void SetMaxValue(float num) {
            maxValue = num;
        }
    }
}

