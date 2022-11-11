using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manapotion.PartySystem;
using Manapotion.Items;
using CodeMonkey.Utils;

namespace Manapotion.UI
{
    public class UI_Equipment : UI_InventoryBase
    {
        public enum EquipmentMenuState { Equip, Upgrade }
        public EquipmentMenuState equipmentMenuState { get; private set; } = EquipmentMenuState.Equip;

        [field: SerializeField]
        private BagScriptableObject _bagScriptableObject;
        
        [field: SerializeField]
        private EquipmentManagerScriptableObject _laurieEquipmentScriptableObject;
        [field: SerializeField]
        private EquipmentManagerScriptableObject _mirabelleEquipmentScriptableObject;
        [field: SerializeField]
        private EquipmentManagerScriptableObject _winsleyEquipmentScriptableObject;

        [System.Serializable]
        private struct UISpriteSet
        {
            public Sprite normal;
            public Sprite light;
            public Sprite dark;
            public Sprite box;
        }

        [SerializeField]
        private UISpriteSet _metalSpriteSet;
        [SerializeField]
        private UISpriteSet _goldSpriteSet;

        // All Image components in the equipment menu.
        [SerializeField]
        private Image[] normals;
        [SerializeField]
        private Image[] lights;
        [SerializeField]
        private Image[] darks;
        [SerializeField]
        private Image[] boxes;

        [SerializeField]
        private GameObject _spellstonesSRGameObject;
        [SerializeField]
        private GameObject _equipmentSRGameObject;

        [SerializeField]
        private Transform _weaponSlot;
        [SerializeField]
        private Sprite _weaponSlotSprite;
        [SerializeField]
        private Transform _armourSlot;
        [SerializeField]
        private Sprite _armourSlotSprite;
        [SerializeField]
        private Transform _vanitySlot;
        [SerializeField]
        private Sprite _vanitySlotSprite;

        Action VanitySlotRightClick;
        Action ArmourSlotRightClick;
        Action WeaponSlotRightClick;

        Action VanitySlotHover;
        Action VanitySlotHoverOut;
        Action ArmourSlotHover;
        Action ArmourSlotHoverOut;
        Action WeaponSlotHover;
        Action WeaponSlotHoverOut;

        [SerializeField]
        private AudioClip equipSoundClip;
        [SerializeField]
        private AudioClip unequipSoundClip;
        
        [SerializeField]
        private AudioSource audioSource;

        private void Awake() {
            _laurieEquipmentScriptableObject.BagItemEquippedEvent += BagItemEquippedEvent_RefreshUI;
            _mirabelleEquipmentScriptableObject.BagItemEquippedEvent += BagItemEquippedEvent_RefreshUI;
            _winsleyEquipmentScriptableObject.BagItemEquippedEvent += BagItemEquippedEvent_RefreshUI;
            
            Party.OnPartyLeaderChangedEvent.AddListener((PartyMember pm) => {
                SetEquipmentSlotData(pm.equipmentManagerScriptableObject);
            });
        }

        private void Start() {
            SetEquipmentSlotData(Party.GetCurrentLeader().equipmentManagerScriptableObject);

            foreach (var img in normals)
            {
                img.sprite = _metalSpriteSet.normal;
            }
            foreach (var img in lights)
            {
                img.sprite = _metalSpriteSet.light;
            }
            foreach (var img in darks)
            {
                img.sprite = _metalSpriteSet.dark;
            }
            foreach (var img in boxes)
            {
                img.sprite = _metalSpriteSet.box;
            }
        }

        // subscribes to BagItemEquippedEvent
        private void BagItemEquippedEvent_RefreshUI(object sender, EquipmentManagerScriptableObject.BagItemEquippedEventArgs e)
        {
            SetEquipmentSlotData(e.equipmentManagerScriptableObject);
            PlayEquipSound();
        }
        
        protected override void Abstract_Show()
        {
            main.dimmer.FadeOut();
            Camera.main.GetComponent<PartyCam>().camZoomState = CamZoomState.ZoomingIn;

            LTDescr leftPanel;
            leftPanel = LeanTween.move(transforms[0], new Vector3(-30, 0, 0), 0.3f);
            leftPanel.setEase(LeanTweenType.easeOutQuad);

            LTDescr rightPanel;
            rightPanel = LeanTween.move(transforms[1], new Vector3(-142, 0, 0), 0.3f);
            rightPanel.setEase(LeanTweenType.easeOutQuad);
            rightPanel.setOnComplete(() => { uiState = UIState.Shown; });

            Camera.main.GetComponent<PartyCam>().camZoomState = CamZoomState.ZoomingIn;
            Party.SetLeaderChangeOverride(true);
        }

        protected override void Abstract_Hide()
        {
            LTDescr leftPanel;
            leftPanel = LeanTween.move(transforms[0], new Vector3(-172, 0, 0), 0.3f);
            leftPanel.setEase(LeanTweenType.easeOutQuad);

            LTDescr rightPanel;
            rightPanel = LeanTween.move(transforms[1], new Vector3(0, 0, 0), 0.3f);
            rightPanel.setEase(LeanTweenType.easeOutQuad);
            rightPanel.setOnComplete(() => { uiState = UIState.Hidden; });

            Camera.main.GetComponent<PartyCam>().camZoomState = CamZoomState.ZoomingOut;     
            Party.SetLeaderChangeOverride(false);
        }

        private void PlayEquipSound()
        {
            if (equipSoundClip != null || audioSource != null)
            {
                audioSource.clip = equipSoundClip;
                audioSource.Play();
            }
        }

        private void SetEquipmentSlotData(EquipmentManagerScriptableObject equipmentManagerScriptableObject)
        {
            var vimg = _vanitySlot.Find("VanityImage").GetComponent<Image>();
            var aimg = _armourSlot.Find("ArmourImage").GetComponent<Image>();
            var wimg = _weaponSlot.Find("WeaponImage").GetComponent<Image>();

            vimg.sprite = _vanitySlotSprite; 
            aimg.sprite = _armourSlotSprite;
            wimg.sprite = _weaponSlotSprite;
            
            if (equipmentManagerScriptableObject?.vanity?.itemScriptableObject != null)
                vimg.sprite = equipmentManagerScriptableObject.vanity.itemScriptableObject.itemSprite;
            if (equipmentManagerScriptableObject?.armour?.itemScriptableObject != null)
                aimg.sprite = equipmentManagerScriptableObject.armour.itemScriptableObject.itemSprite;
            if (equipmentManagerScriptableObject?.weapon?.itemScriptableObject != null)
                wimg.sprite = equipmentManagerScriptableObject.weapon.itemScriptableObject.itemSprite;

            VanitySlotRightClick = delegate()
            {
                int charIDOrigin = Party.GetPartyMemberIndex(Party.GetCurrentLeader());
                ContextMenuHandler.Show(ContextMenuType.ContextMenu);
                
                ContextMenuHandler.SetTitle(equipmentManagerScriptableObject.vanity.ToString());
                ContextMenuHandler.SetSubtitle(equipmentManagerScriptableObject.vanity.itemScriptableObject.itemCategory.ToString());
                ContextMenuHandler.SetBody(equipmentManagerScriptableObject.vanity.itemScriptableObject.itemDescription);

                // Add the option to unequip the equipable
                ContextMenuHandler.AddOption("Un-Equip", () => {
                    equipmentManagerScriptableObject.UnequipItem(equipmentManagerScriptableObject.vanity);
                    SetEquipmentSlotData(equipmentManagerScriptableObject);
                    ContextMenuHandler.Hide();

                    if (unequipSoundClip != null || audioSource != null)
                    {
                        audioSource.clip = unequipSoundClip;
                        audioSource.Play();
                    }
                });

                ContextMenuHandler.AddOption("Upgrade", () => {
                    Debug.Log("Upgrading " + equipmentManagerScriptableObject.vanity.ToString() + " !!!");
                    UpgradeState();
                    ContextMenuHandler.Hide();
                });
            };
            ArmourSlotRightClick = delegate()
            {
                int charIDOrigin = Party.GetPartyMemberIndex(Party.GetCurrentLeader());
                ContextMenuHandler.Show(ContextMenuType.ContextMenu);

                ContextMenuHandler.SetTitle(equipmentManagerScriptableObject.armour.ToString());
                ContextMenuHandler.SetSubtitle(equipmentManagerScriptableObject.armour.itemScriptableObject.itemCategory.ToString());
                ContextMenuHandler.SetBody(equipmentManagerScriptableObject.armour.itemScriptableObject.itemDescription);

                // Add the option to unequip the equipable
                ContextMenuHandler.AddOption("Un-Equip", () => {
                    equipmentManagerScriptableObject.UnequipItem(equipmentManagerScriptableObject.armour);
                    SetEquipmentSlotData(equipmentManagerScriptableObject);
                    ContextMenuHandler.Hide();

                    if (unequipSoundClip != null || audioSource != null)
                    {
                        audioSource.clip = unequipSoundClip;
                        audioSource.Play();
                    }
                });

                ContextMenuHandler.AddOption("Upgrade", () => {
                    Debug.Log("Upgrading " + equipmentManagerScriptableObject.armour.ToString() + " !!!");
                    UpgradeState();
                    ContextMenuHandler.Hide();
                });
            };
            WeaponSlotRightClick = delegate()
            {
                int charIDOrigin = Party.GetPartyMemberIndex(Party.GetCurrentLeader());
                ContextMenuHandler.Show(ContextMenuType.ContextMenu);

                ContextMenuHandler.SetTitle(equipmentManagerScriptableObject.weapon.ToString());
                ContextMenuHandler.SetSubtitle(equipmentManagerScriptableObject.weapon.itemScriptableObject.itemCategory.ToString());
                ContextMenuHandler.SetBody(equipmentManagerScriptableObject.weapon.itemScriptableObject.itemDescription);

                // Add the option to unequip the equipable
                ContextMenuHandler.AddOption("Un-Equip", () => {
                    equipmentManagerScriptableObject.UnequipItem(equipmentManagerScriptableObject.weapon);
                    SetEquipmentSlotData(equipmentManagerScriptableObject);
                    ContextMenuHandler.Hide();

                    if (unequipSoundClip != null || audioSource != null)
                    {
                        audioSource.clip = unequipSoundClip;
                        audioSource.Play();
                    }
                });

                ContextMenuHandler.AddOption("Upgrade", () => {
                    Debug.Log("Upgrading " + equipmentManagerScriptableObject.weapon.ToString() + " !!!");
                    UpgradeState();
                    ContextMenuHandler.Hide();
                });
            };

            VanitySlotHover = delegate()
            {
                if (!ContextMenuHandler.Instance.contextMenuOpen)
                {
                    ContextMenuHandler.Show(ContextMenuType.Tooltip);
                }
                
                ContextMenuHandler.SetTitle(equipmentManagerScriptableObject.vanity.ToString());
                ContextMenuHandler.SetSubtitle(equipmentManagerScriptableObject.vanity.itemScriptableObject.itemCategory.ToString());
            };
            ArmourSlotHover = delegate()
            {
                if (!ContextMenuHandler.Instance.contextMenuOpen)
                {
                    ContextMenuHandler.Show(ContextMenuType.Tooltip);
                }
                
                ContextMenuHandler.SetTitle(equipmentManagerScriptableObject.armour.ToString());
                ContextMenuHandler.SetSubtitle(equipmentManagerScriptableObject.armour.itemScriptableObject.itemCategory.ToString());
            };
            WeaponSlotHover = delegate()
            {
                if (!ContextMenuHandler.Instance.contextMenuOpen)
                {
                    ContextMenuHandler.Show(ContextMenuType.Tooltip);
                }
                
                ContextMenuHandler.SetTitle(equipmentManagerScriptableObject.weapon.ToString());
                ContextMenuHandler.SetSubtitle(equipmentManagerScriptableObject.weapon.itemScriptableObject.itemCategory.ToString());
            };

            if (equipmentManagerScriptableObject?.vanity?.itemScriptableObject != null)
            {
                _vanitySlot.GetComponent<Button_UI>().MouseRightClickFunc += VanitySlotRightClick;
                _vanitySlot.GetComponent<Button_UI>().MouseOverFunc += VanitySlotHover;
                _vanitySlot.GetComponent<Button_UI>().MouseOutOnceFunc += () => {
                    if (!ContextMenuHandler.Instance.contextMenuOpen)
                    {
                        ContextMenuHandler.Hide();
                    }
                };
            }
            else
            {
                _vanitySlot.GetComponent<Button_UI>().MouseRightClickFunc = null;
                _vanitySlot.GetComponent<Button_UI>().MouseOverFunc = null;
                _vanitySlot.GetComponent<Button_UI>().MouseOutOnceFunc = null;
            }
            if (equipmentManagerScriptableObject?.armour?.itemScriptableObject != null)
            {
                _armourSlot.GetComponent<Button_UI>().MouseRightClickFunc += ArmourSlotRightClick;
                _armourSlot.GetComponent<Button_UI>().MouseOverFunc += ArmourSlotHover;
                _armourSlot.GetComponent<Button_UI>().MouseOutOnceFunc += () => {
                    if (!ContextMenuHandler.Instance.contextMenuOpen)
                    {
                        ContextMenuHandler.Hide();
                    }
                };
            }
            else 
            {
                _armourSlot.GetComponent<Button_UI>().MouseRightClickFunc = null;
                _armourSlot.GetComponent<Button_UI>().MouseOverFunc = null;
                _armourSlot.GetComponent<Button_UI>().MouseOutOnceFunc = null;
            }
            if (equipmentManagerScriptableObject?.weapon?.itemScriptableObject != null)
            {
                _weaponSlot.GetComponent<Button_UI>().MouseRightClickFunc += WeaponSlotRightClick;
                _weaponSlot.GetComponent<Button_UI>().MouseOverFunc += WeaponSlotHover;
                _weaponSlot.GetComponent<Button_UI>().MouseOutOnceFunc += () => {
                    if (!ContextMenuHandler.Instance.contextMenuOpen)
                    {
                        ContextMenuHandler.Hide();
                    }
                };
            }
            else
            {
                _weaponSlot.GetComponent<Button_UI>().MouseRightClickFunc = null;
                _weaponSlot.GetComponent<Button_UI>().MouseOverFunc = null;
                _weaponSlot.GetComponent<Button_UI>().MouseOutOnceFunc = null;
            }
        }

        private void UpgradeState()
        {
            LTDescr upgradeTwOb;
            upgradeTwOb = LeanTween.moveLocal(_spellstonesSRGameObject, new Vector3(0f, 0f, 0f), 0.3f);
            upgradeTwOb.setEase(LeanTweenType.easeOutQuad);

            LTDescr equipmentTwOb;
            equipmentTwOb = LeanTween.moveLocal(_equipmentSRGameObject, new Vector3(150f, 0f, 0f), 0.3f);
            equipmentTwOb.setEase(LeanTweenType.easeOutQuad);

            foreach (var img in normals)
            {
                img.sprite = _goldSpriteSet.normal;
            }
            foreach (var img in lights)
            {
                img.sprite = _goldSpriteSet.light;
            }
            foreach (var img in darks)
            {
                img.sprite = _goldSpriteSet.dark;
            }
            foreach (var img in boxes)
            {
                img.sprite = _goldSpriteSet.box;
            }
        }
    }
}
