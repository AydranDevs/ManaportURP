using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manapotion.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;

    bool tooltipShown = false; 

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    // update tooltip
    private void Update()
    {
        if (!tooltipShown)
        {
            return;
        }

        ContextMenuHandler.SetTitle(string.Format("Health: {0}/{1}", slider.value, slider.maxValue));
    }

    public void ShowTooltip()
    {
        ContextMenuHandler.Clear();
        ContextMenuHandler.Show(ContextMenuType.Tooltip);
        tooltipShown = true;
    }

    public void HideTooltip()
    {
        ContextMenuHandler.Hide();
        ContextMenuHandler.Clear();
        tooltipShown = false;
    }
}
