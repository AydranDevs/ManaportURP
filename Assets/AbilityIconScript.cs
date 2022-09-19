using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.UI;

public class AbilityIconScript : MonoBehaviour
{
    private bool tooltipShown;
    // update tooltip
    private void Update()
    {
        if (!tooltipShown)
        {
            return;
        }

        ContextMenuHandler.SetTitle(string.Format("primary spell"));
        ContextMenuHandler.SetSubtitle("ye");
    }

    public void ShowTooltip()
    {
        ContextMenuHandler.Show(ContextMenuType.Tooltip);
        tooltipShown = true;
    }

    public void HideTooltip()
    {
        ContextMenuHandler.Hide();
        tooltipShown = false;
    }
}
