using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {
    public Slider slider;

    public void SetMaxHealth(float maxHealth) {
        slider.maxValue = maxHealth;
    }

    public void SetHealth(float health) {
        slider.value = health;
    }
}
