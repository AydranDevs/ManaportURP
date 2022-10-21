using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manapotion.PartySystem;
using Manapotion.UI;
using CodeMonkey.Utils;

public class UI_Equipment : MonoBehaviour
{
    public enum EquipmentMenuState { Equip, Upgrade }
    public EquipmentMenuState equipmentMenuState { get; private set; } = EquipmentMenuState.Equip;

    [field: SerializeField]
    private BagScriptableObject _bagScriptableObject;
    
    [field: SerializeField]
    private EquipmentScriptableObject _laurieEquipmentScriptableObject;
    [field: SerializeField]
    private EquipmentScriptableObject _mirabelleEquipmentScriptableObject;
    [field: SerializeField]
    private EquipmentScriptableObject _winsleyEquipmentScriptableObject;

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
        _laurieEquipmentScriptableObject.bagItemEquippedEvent.AddListener(SetEquipmentSlotData);
        _mirabelleEquipmentScriptableObject.bagItemEquippedEvent.AddListener(SetEquipmentSlotData);
        _winsleyEquipmentScriptableObject.bagItemEquippedEvent.AddListener(SetEquipmentSlotData);
        
        _laurieEquipmentScriptableObject.bagItemEquippedEvent.AddListener(PlayEquipSound);
        _mirabelleEquipmentScriptableObject.bagItemEquippedEvent.AddListener(PlayEquipSound);
        _winsleyEquipmentScriptableObject.bagItemEquippedEvent.AddListener(PlayEquipSound);
        
        Party.OnPartyLeaderChangedEvent.AddListener((PartyMember pm) => {
            SetEquipmentSlotData(pm.equipmentScriptableObject);
        });
    }

    private void Start() {
        SetEquipmentSlotData(Party.GetCurrentLeader().equipmentScriptableObject);
    }

    private void PlayEquipSound(EquipmentScriptableObject equipmentScriptable)
    {
        if (equipSoundClip != null || audioSource != null)
        {
            audioSource.clip = equipSoundClip;
            audioSource.Play();
        }
    }

    private void SetEquipmentSlotData(EquipmentScriptableObject equipmentScriptable)
    {
        var vimg = _vanitySlot.Find("VanityImage").GetComponent<Image>();
        var aimg = _armourSlot.Find("ArmourImage").GetComponent<Image>();
        var wimg = _weaponSlot.Find("WeaponImage").GetComponent<Image>();

        vimg.sprite = _vanitySlotSprite; 
        aimg.sprite = _armourSlotSprite;
        wimg.sprite = _weaponSlotSprite;
        
        if (equipmentScriptable.vanity.GetItemIDMetaData().sprite != null)
            vimg.sprite = equipmentScriptable.vanity.GetItemIDMetaData().sprite;
        if (equipmentScriptable.armour.GetItemIDMetaData().sprite != null)
            aimg.sprite = equipmentScriptable.armour.GetItemIDMetaData().sprite;
        if (equipmentScriptable.weapon.GetItemIDMetaData().sprite != null)
            wimg.sprite = equipmentScriptable.weapon.GetItemIDMetaData().sprite;

        VanitySlotRightClick = delegate()
        {
            int charIDOrigin = Party.GetPartyMemberIndex(Party.GetCurrentLeader());
            ContextMenuHandler.Show(ContextMenuType.ContextMenu);
            
            ContextMenuHandler.SetTitle(equipmentScriptable.vanity.GetItemIDMetaData().name);
            ContextMenuHandler.SetSubtitle(equipmentScriptable.vanity.GetItemIDMetaData().category);
            ContextMenuHandler.SetBody(equipmentScriptable.vanity.GetItemIDMetaData().lore);

            // Add the option to unequip the equipable
            ContextMenuHandler.AddOption("Un-Equip", () => {
                equipmentScriptable.UnequipEquipable(equipmentScriptable.vanity);
                SetEquipmentSlotData(equipmentScriptable);
                ContextMenuHandler.Hide();

                if (unequipSoundClip != null || audioSource != null)
                {
                    audioSource.clip = unequipSoundClip;
                    audioSource.Play();
                }
            });

            ContextMenuHandler.AddOption("Upgrade", () => {
                Debug.Log("Upgrading " + equipmentScriptable.vanity.equipableID + " !!!");
                UpgradeState();
                ContextMenuHandler.Hide();
            });
        };
        ArmourSlotRightClick = delegate()
        {
            int charIDOrigin = Party.GetPartyMemberIndex(Party.GetCurrentLeader());
            ContextMenuHandler.Show(ContextMenuType.ContextMenu);

            ContextMenuHandler.SetTitle(equipmentScriptable.armour.GetItemIDMetaData().name);
            ContextMenuHandler.SetSubtitle(equipmentScriptable.armour.GetItemIDMetaData().category);
            ContextMenuHandler.SetBody(equipmentScriptable.armour.GetItemIDMetaData().lore);

            // Add the option to unequip the equipable
            ContextMenuHandler.AddOption("Un-Equip", () => {
                equipmentScriptable.UnequipEquipable(equipmentScriptable.armour);
                ContextMenuHandler.Hide();

                if (unequipSoundClip != null || audioSource != null)
                {
                    audioSource.clip = unequipSoundClip;
                    audioSource.Play();
                }
            });

            ContextMenuHandler.AddOption("Upgrade", () => {
                Debug.Log("Upgrading " + equipmentScriptable.armour.equipableID + " !!!");
                UpgradeState();
                ContextMenuHandler.Hide();
            });
        };
        WeaponSlotRightClick = delegate()
        {
            int charIDOrigin = Party.GetPartyMemberIndex(Party.GetCurrentLeader());
            ContextMenuHandler.Show(ContextMenuType.ContextMenu);

            ContextMenuHandler.SetTitle(equipmentScriptable.weapon.GetItemIDMetaData().name);
            ContextMenuHandler.SetSubtitle(equipmentScriptable.weapon.GetItemIDMetaData().category);
            ContextMenuHandler.SetBody(equipmentScriptable.weapon.GetItemIDMetaData().lore);

            // Add the option to unequip the equipable
            ContextMenuHandler.AddOption("Un-Equip", () => {
                equipmentScriptable.UnequipEquipable(equipmentScriptable.weapon);
                SetEquipmentSlotData(equipmentScriptable);
                ContextMenuHandler.Hide();

                if (unequipSoundClip != null || audioSource != null)
                {
                    audioSource.clip = unequipSoundClip;
                    audioSource.Play();
                }
            });

            ContextMenuHandler.AddOption("Upgrade", () => {
                Debug.Log("Upgrading " + equipmentScriptable.weapon.equipableID + " !!!");
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
            
            ContextMenuHandler.SetTitle(equipmentScriptable.vanity.GetItemIDMetaData().name);
            ContextMenuHandler.SetSubtitle(equipmentScriptable.vanity.GetItemIDMetaData().category);
        };
        ArmourSlotHover = delegate()
        {
            if (!ContextMenuHandler.Instance.contextMenuOpen)
            {
                ContextMenuHandler.Show(ContextMenuType.Tooltip);
            }
            
            ContextMenuHandler.SetTitle(equipmentScriptable.armour.GetItemIDMetaData().name);
            ContextMenuHandler.SetSubtitle(equipmentScriptable.armour.GetItemIDMetaData().category);
        };
        WeaponSlotHover = delegate()
        {
            if (!ContextMenuHandler.Instance.contextMenuOpen)
            {
                ContextMenuHandler.Show(ContextMenuType.Tooltip);
            }
            
            ContextMenuHandler.SetTitle(equipmentScriptable.weapon.GetItemIDMetaData().name);
            ContextMenuHandler.SetSubtitle(equipmentScriptable.weapon.GetItemIDMetaData().category);
        };

        if (equipmentScriptable.vanity.equipableID != ItemID.manaport_nothing)
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
        if (equipmentScriptable.armour.equipableID != ItemID.manaport_nothing)
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
        if (equipmentScriptable.weapon.equipableID != ItemID.manaport_nothing)
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
    }
}
