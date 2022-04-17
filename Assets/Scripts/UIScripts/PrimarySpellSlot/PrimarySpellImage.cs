using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrimarySpellImage : MonoBehaviour {
    public Image image;
    public Image cooldown;
    public Image spellLock;

    public void SetIcon(Sprite image) {
        this.image.sprite = image;
    }

    public void UpdateCooldown(float time, float startTime) {
        float normalizedValue = Mathf.Clamp(time / startTime, 0.0f, 1.0f);
        cooldown.fillAmount = normalizedValue;

        if (normalizedValue == 1f) {
            cooldown.enabled = false;
        }else {
            cooldown.enabled = true;
        }
    }

    public void UpdateSpellLock(bool hasMana) {
        if (!hasMana) {
            spellLock.enabled = true;
        }else {
            spellLock.enabled = false;
        }
    }
}
