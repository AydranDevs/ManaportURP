using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manapotion.UI;

public class ManaBarScript : MonoBehaviour
{
    public Slider slider;
    
    bool tooltipShown = false; 

    public void SetMaxMana(float maxMana)
    {
        slider.maxValue = maxMana;
    }

    public void SetMana(float mana)
    {
        slider.value = mana;
    }

    // update tooltip
    private void Update()
    {
        if (!tooltipShown)
        {
            return;
        }

        ContextMenuHandler.SetTitle(string.Format("Mana: {0}/{1}", slider.value, slider.maxValue));
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
