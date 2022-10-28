using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manapotion.UI
{
    public class StatusBar : MonoBehaviour
    {
        public Slider slider;

        bool tooltipShown = false; 

        public void SetMaxValue(float value)
        {
            slider.maxValue = value;
        }

        public void SetValue(float value)
        {
            slider.value = value;
        }

        // update tooltip
        private void Update()
        {
            if (!tooltipShown)
            {
                return;
            }

            ContextMenuHandler.SetTitle(string.Format("{0}/{1}", slider.value, slider.maxValue));
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
}
