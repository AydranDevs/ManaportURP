using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {
    public Slider slider;

    bool tooltipShown = false; 

    public void SetMaxHealth(float maxHealth) {
        slider.maxValue = maxHealth;
    }

    public void SetHealth(float health) {
        slider.value = health;
    }

    // update tooltip
    private void Update() {
        if (!tooltipShown) return;

        TooltipHandler.UpdateTooltip_Static("Health: " + slider.value.ToString() + "/" + slider.maxValue.ToString());
    }

    public void ShowTooltip() {
        TooltipHandler.ShowTooltip_Static("Health: " + slider.value.ToString() + "/" + slider.maxValue.ToString());
        tooltipShown = true;
    }

    public void HideTooltip() {
        TooltipHandler.HideTooltip_Static();
        tooltipShown = false;
    }
}
