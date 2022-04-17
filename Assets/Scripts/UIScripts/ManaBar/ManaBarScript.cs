using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarScript : MonoBehaviour {
    public Slider slider;

    public void SetMaxMana(float maxMana) {
        slider.maxValue = maxMana;
    }

    public void SetMana(float mana) {
        slider.value = mana;
    }
}
