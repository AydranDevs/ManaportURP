using System;
using UnityEngine;
using UnityEngine.UI;
using Manapotion.PartySystem;

namespace Manapotion.UI
{
    public struct AbilityIcon
    {
        public GameObject abilityGameObject;

        public Image image;
        public Image cooldown;
        public Image abilityLock;
    }

    public struct StatusBar
    {
        public GameObject statusBarGameObject;

        public Slider fill;
    }

    public class StatusUIManager
    {
        private MainUIManager _main;

        public AbilityIcon[] abilityIcons { get; private set; }
        public StatusBar[] statusBars { get; private set; }

        public bool isHidden { get; private set; }
        
        public StatusUIManager(MainUIManager main)
        {
            _main = main;

            // NOTE: maybe there can be more than 2 abilities per char?
            abilityIcons = new AbilityIcon[2];
            InitAbilityIcons();

            statusBars = new StatusBar[2];
            InitStatusBars();

            PartyMember.OnAbilityChanged += OnAbilityChanged_SetIconImage;
            PartyMember.OnCoolingDown += OnCoolingDown_SetIconCooldown;
            PartyMember.OnAbilityLockChanged += OnAbilityLockChanged_SetLockState;

            PartyMember.OnUpdateHealthBar += OnUpdateHealthBar_UpdateHealthBar;
            PartyMember.OnUpdateManaBar += OnUpdateManaBar_UpdateManaBar;
        }

        #region Init
        private void InitAbilityIcons()
        {
            for (int i = 0; i < abilityIcons.Length; i++)
            {
                abilityIcons[i].abilityGameObject = GameObject.Instantiate(_main.abilityIconPrefab, _main.abilityIconParent.transform);
                
                abilityIcons[i].image = abilityIcons[i].abilityGameObject.transform.GetChild(1).GetComponent<Image>();
                abilityIcons[i].cooldown = abilityIcons[i].abilityGameObject.transform.GetChild(2).GetComponent<Image>();
                abilityIcons[i].abilityLock = abilityIcons[i].abilityGameObject.transform.GetChild(3).GetComponent<Image>();
            }
        }
        
        private void InitStatusBars()
        {
            statusBars[0].statusBarGameObject = GameObject.Instantiate(_main.healthBarPrefab, _main.statusBarParent.transform);
            statusBars[1].statusBarGameObject = GameObject.Instantiate(_main.manaBarPrefab, _main.statusBarParent.transform);

            for (int i = 0; i < statusBars.Length; i++)
            {
                statusBars[i].fill = statusBars[i].statusBarGameObject.transform.GetComponent<Slider>();
            }
        }
        #endregion

        #region Set Ability Icon Values
        public void OnAbilityChanged_SetIconImage(object sender, PartyMember.OnAbilityChangedEventArgs e)
        {
            SetIconImage(e.index, e.sprite);
        }

        private void SetIconImage(int index, Sprite img)
        {
            abilityIcons[index].image.sprite = img;
        }

        public void OnCoolingDown_SetIconCooldown(object sender, PartyMember.OnCoolingDownEventArgs e)
        {
            SetIconCooldown(e.index, e.cooldownTime, e.cooldown);
        }

        private void SetIconCooldown(int index, float time, float startTime)
        {
            float normalizedValue = Mathf.Clamp(time / startTime, 0.0f, 1.0f);
            abilityIcons[index].cooldown.fillAmount = normalizedValue;

            if (normalizedValue == 1f)
            {
                abilityIcons[index].cooldown.enabled = false;
            }
            else
            {
                abilityIcons[index].cooldown.enabled = true;
            }
        }

        public void OnAbilityLockChanged_SetLockState(object sender, PartyMember.OnAbilityLockChangedEventArgs e)
        {
            SetLockState(e.index, e.isLocked);
        }

        private void SetLockState(int index, bool locked)
        {
            if (!locked)
            {
                abilityIcons[index].abilityLock.enabled = true;
            }
            else
            {
                abilityIcons[index].abilityLock.enabled = false;
            }
        }
        #endregion
    
        #region Set Status Bar Values
        public void OnUpdateHealthBar_UpdateHealthBar(object sender, PartyMember.OnUpdateHealthBarEventArgs e)
        {
            UpdateHealthBar(e.health, e.maxHealth);
        }
        private void UpdateHealthBar(float health, float maxHealth)
        {
            statusBars[0].fill.maxValue = maxHealth;
            statusBars[0].fill.value = health;
        }

        public void OnUpdateManaBar_UpdateManaBar(object sender, PartyMember.OnUpdateManaBarEventArgs e)
        {
            UpdateManaBar(e.mana, e.maxMana);
        }
        private void UpdateManaBar(float mana, float maxMana)
        {
            statusBars[1].fill.maxValue = maxMana;
            statusBars[1].fill.value = mana;
        }
        #endregion
    
        #region Show/Hide Status
        public void Hide()
        {
            isHidden = true;
            LTDescr tweenObject;
            tweenObject = LeanTween.move(_main.statusParent.GetComponent<RectTransform>(), new Vector3(-70, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
        }

        public void Show()
        {
            isHidden = false;
            LTDescr tweenObject;
            tweenObject = LeanTween.move(_main.statusParent.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
        }
        #endregion
    }
}
