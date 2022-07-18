using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarScript : MonoBehaviour {
    public Slider slider;
    
    bool tooltipShown = false; 

    public void SetMaxMana(float maxMana) {
        slider.maxValue = maxMana;
    }

    public void SetMana(float mana) {
        slider.value = mana;
    }

    // update tooltip
    private void Update() {
        if (!tooltipShown) return;

        TooltipHandler.UpdateTooltip_Static("Mana: " + slider.value.ToString() + "/" + slider.maxValue.ToString());
    }

    public void ShowTooltip() {
        TooltipHandler.ShowTooltip_Static("Mana: " + slider.value.ToString() + "/" + slider.maxValue.ToString());
        tooltipShown = true;
    }

    public void HideTooltip() {
        TooltipHandler.HideTooltip_Static();
        tooltipShown = false;
    }
}
