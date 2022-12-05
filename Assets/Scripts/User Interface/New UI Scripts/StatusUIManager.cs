using System;
using UnityEngine;
using UnityEngine.UI;
using Manapotion.PartySystem;

namespace Manapotion.UI
{
    public struct AbilityIconData
    {
        public GameObject abilityGameObject;

        public Image image;
        public Image cooldown;
        public Image abilityLock;
    }

    public struct StatusBarData
    {
        public GameObject statusBarGameObject;

        public StatusBar statusBar;
    }

    public class StatusUIManager
    {
        private MainUIManager _main;

        public AbilityIconData[] abilityIcons { get; private set; }
        public StatusBarData[] statusBars { get; private set; }

        public bool isHidden { get; private set; }
        
        public StatusUIManager(MainUIManager main)
        {
            _main = main;

            // NOTE: maybe there can be more than 2 abilities per char?
            abilityIcons = new AbilityIconData[2];
            InitAbilityIcons();

            statusBars = new StatusBarData[2];
            InitStatusBars();

            PartyMember.OnAbilityChangedEvent += OnAbilityChanged_SetIconImage;
            PartyMember.OnCoolingDownEvent += OnCoolingDown_SetIconCooldown;
            PartyMember.OnAbilityLockChangedEvent += OnAbilityLockChanged_SetLockState;

            PartyMember.OnUpdateHealthBarEvent += OnUpdateHealthBar_UpdateHealthBar;
            PartyMember.OnUpdateActionBarEvent += OnUpdateActionBar_SetActionBar;
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
                statusBars[i].statusBar = statusBars[i].statusBarGameObject.GetComponent<StatusBar>();
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
        public void OnUpdateActionBar_SetActionBar(object sender, PartyMember.OnUpdateActionBarEventArgs e)
        {
            // instantiate the correct action bar
            switch (e.actionPoints)
            {
                case Stats.PointID.Manapoints:
                    GameObject.Destroy(statusBars[1].statusBarGameObject);
                    statusBars[1].statusBarGameObject = GameObject.Instantiate(_main.manaBarPrefab, _main.statusBarParent.transform);
                    statusBars[1].statusBar = statusBars[1].statusBarGameObject.GetComponent<StatusBar>();
                    break;
                case Stats.PointID.Remedypoints:
                    GameObject.Destroy(statusBars[1].statusBarGameObject);
                    statusBars[1].statusBarGameObject = GameObject.Instantiate(_main.remedyBarPrefab, _main.statusBarParent.transform);
                    statusBars[1].statusBar = statusBars[1].statusBarGameObject.GetComponent<StatusBar>();
                    break;
                case Stats.PointID.Staminapoints:
                    GameObject.Destroy(statusBars[1].statusBarGameObject);
                    statusBars[1].statusBarGameObject = GameObject.Instantiate(_main.staminaBarPrefab, _main.statusBarParent.transform);
                    statusBars[1].statusBar = statusBars[1].statusBarGameObject.GetComponent<StatusBar>();
                    break;
            }

            SetBarData(1, e.currentValue, e.maxValue);
        }

        public void OnUpdateHealthBar_UpdateHealthBar(object sender, PartyMember.OnUpdateHealthBarEventArgs e)
        {
            SetBarData(0, e.currentValue, e.maxValue);
        }

        private void SetBarData(int i, float value, float maxValue)
        {
            statusBars[i].statusBar.SetMaxValue(maxValue);
            statusBars[i].statusBar.SetValue(value);
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
