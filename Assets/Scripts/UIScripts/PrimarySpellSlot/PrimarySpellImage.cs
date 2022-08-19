using UnityEngine;
using UnityEngine.UI;
using Manapotion.PartySystem.LaurieCharacter;

public class PrimarySpellImage : MonoBehaviour
{
    public Image image;
    public Image cooldown;
    public Image spellLock;

    bool tooltipShown = false; 

    public void SetIcon(Sprite image)
    {
        this.image.sprite = image;
    }

    public void UpdateCooldown(float time, float startTime)
    {
        float normalizedValue = Mathf.Clamp(time / startTime, 0.0f, 1.0f);
        cooldown.fillAmount = normalizedValue;

        if (normalizedValue == 1f)
        {
            cooldown.enabled = false;
        }
        else
        {
            cooldown.enabled = true;
        }
    }

    public void UpdateSpellLock(bool hasMana)
    {
        if (!hasMana)
        {
            spellLock.enabled = true;
        }
        else
        {
            spellLock.enabled = false;
        }
    }

    // update tooltip
    private void Update()
    {
        if (!tooltipShown) return;

        if (!spellLock.enabled)
        {
            TooltipHandler.UpdateTooltip_Static(Laurie.GetPrimarySpellInfo());
        }
        else
        {
            TooltipHandler.UpdateTooltip_Static(Laurie.GetPrimarySpellInfo() + " (Not enough mana!)");
        }

        if (!cooldown.enabled)
        {
            TooltipHandler.UpdateTooltip_Static(Laurie.GetPrimarySpellInfo());
        }
        else
        {
            TooltipHandler.UpdateTooltip_Static(Laurie.GetPrimarySpellInfo() + " (cooling down...)");
        }
    }

    public void ShowTooltip()
    {
        if (!spellLock.enabled)
        {
            TooltipHandler.ShowTooltip_Static(Laurie.GetPrimarySpellInfo());
        }
        else
        {
            TooltipHandler.ShowTooltip_Static(Laurie.GetPrimarySpellInfo() + " (Not enough mana!)");
        }

        if (!cooldown.enabled)
        {
            TooltipHandler.ShowTooltip_Static(Laurie.GetPrimarySpellInfo());
        }
        else
        {
            TooltipHandler.ShowTooltip_Static(Laurie.GetPrimarySpellInfo() + " (cooling down...)");
        }

        tooltipShown = true;
    }

    public void HideTooltip()
    {
        TooltipHandler.HideTooltip_Static();
        tooltipShown = false;
    }
}
