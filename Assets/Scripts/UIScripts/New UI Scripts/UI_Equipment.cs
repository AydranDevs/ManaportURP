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
    [field: SerializeField]
    private BagScriptableObject _bagScriptableObject;
    
    [field: SerializeField]
    private EquipmentScriptableObject _laurieEquipmentScriptableObject;
    [field: SerializeField]
    private EquipmentScriptableObject _mirabelleEquipmentScriptableObject;
    [field: SerializeField]
    private EquipmentScriptableObject _winsleyEquipmentScriptableObject;

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

    [System.Serializable]
    private struct CharEquipment
    {
        public Item weapon;
        public Item armour;
        public Item vanity;
    }

    Action VanitySlotRightClick;
    Action ArmourSlotRightClick;
    Action WeaponSlotRightClick;

    [SerializeField]
    private CharEquipment _laurieEquipment;
    [SerializeField]
    private CharEquipment _mirabelleEquipment;
    [SerializeField]
    private CharEquipment _winsleyEquipment;

    private void Awake() {
        _bagScriptableObject.bagItemEquippedEvent.AddListener(ItemEquipped);
        Party.OnPartyLeaderChanged = RefreshShownEquippedItems;
    }

    private void ItemEquipped(Item item, int charID)
    {
        var iC = item.GetMetadata().category;
        switch (charID)
        {
            case 0:
                if (iC == ItemCategories.Weapon)
                {
                    _laurieEquipment.weapon = new Item { itemID = item.itemID, amount = item.amount };
                }
                else if (iC == ItemCategories.Armour)
                {
                    _laurieEquipment.armour = new Item { itemID = item.itemID, amount = item.amount };
                }
                else
                {
                    _laurieEquipment.vanity = new Item { itemID = item.itemID, amount = item.amount };
                }
            break;
            case 1:
                if (iC == ItemCategories.Weapon)
                {
                    _mirabelleEquipment.weapon = new Item { itemID = item.itemID, amount = item.amount };
                }
                else if (iC == ItemCategories.Armour)
                {
                    _mirabelleEquipment.armour = new Item { itemID = item.itemID, amount = item.amount };
                }
                else
                {
                    _mirabelleEquipment.vanity = new Item { itemID = item.itemID, amount = item.amount };
                }
            break;
            case 2:
                if (iC == ItemCategories.Weapon)
                {
                    _winsleyEquipment.weapon = new Item { itemID = item.itemID, amount = item.amount };
                }
                else if (iC == ItemCategories.Armour)
                {
                    _winsleyEquipment.armour = new Item { itemID = item.itemID, amount = item.amount };
                }
                else
                {
                    _winsleyEquipment.vanity = new Item { itemID = item.itemID, amount = item.amount };
                }
            break;
        }
        RefreshShownEquippedItems();
    }
    
    private void ItemUnEquipped(Item item, int charID)
    {
        var iC = item.GetMetadata().category;
        switch (charID)
        {
            case 0:
                if (iC == ItemCategories.Weapon)
                {
                    _laurieEquipment.weapon = new Item { itemID = ItemID.manaport_nothing };
                }
                else if (iC == ItemCategories.Armour)
                {
                    _laurieEquipment.armour = new Item { itemID = ItemID.manaport_nothing };
                }
                else
                {
                    _laurieEquipment.vanity = new Item { itemID = ItemID.manaport_nothing };
                }
            break;
            case 1:
                if (iC == ItemCategories.Weapon)
                {
                    _mirabelleEquipment.weapon = new Item { itemID = ItemID.manaport_nothing };
                }
                else if (iC == ItemCategories.Armour)
                {
                    _mirabelleEquipment.armour = new Item { itemID = ItemID.manaport_nothing };
                }
                else
                {
                    _mirabelleEquipment.vanity = new Item { itemID = ItemID.manaport_nothing };
                }
            break;
            case 2:
                if (iC == ItemCategories.Weapon)
                {
                    _winsleyEquipment.weapon = new Item { itemID = ItemID.manaport_nothing };
                }
                else if (iC == ItemCategories.Armour)
                {
                    _winsleyEquipment.armour = new Item { itemID = ItemID.manaport_nothing };
                }
                else
                {
                    _winsleyEquipment.vanity = new Item { itemID = ItemID.manaport_nothing };
                }
            break;
        }
        RefreshShownEquippedItems();
    }

    private void RefreshShownEquippedItems()
    {
        int currentLeader = Party.GetPartyMemberIndex(Party.GetCurrentLeader());
        switch (currentLeader)
        {
            case 0:
                SetEquipmentSlotData(_laurieEquipment);
            break;
            case 1:
                SetEquipmentSlotData(_mirabelleEquipment);
            break;
            case 2:
                SetEquipmentSlotData(_winsleyEquipment);
            break;
        }
    }

    private void SetEquipmentSlotData(CharEquipment ce)
    {
        var vimg = _vanitySlot.Find("VanityImage").GetComponent<Image>();
        var aimg = _armourSlot.Find("ArmourImage").GetComponent<Image>();
        var wimg = _weaponSlot.Find("WeaponImage").GetComponent<Image>();

        vimg.sprite = _vanitySlotSprite; 
        aimg.sprite = _armourSlotSprite;
        wimg.sprite = _weaponSlotSprite;
        
        if (ce.vanity.GetMetadata().sprite != null)
            vimg.sprite = ce.vanity.GetMetadata().sprite;
        if (ce.armour.GetMetadata().sprite != null)
            aimg.sprite = ce.armour.GetMetadata().sprite;
        if (ce.weapon.GetMetadata().sprite != null)
            wimg.sprite = ce.weapon.GetMetadata().sprite;

        VanitySlotRightClick = delegate()
        {
            ContextMenuHandler.Show(ContextMenuType.ContextMenu);
            
            ContextMenuHandler.SetTitle(ce.vanity.GetMetadata().name);
            ContextMenuHandler.SetSubtitle(ce.vanity.GetMetadata().category);
            ContextMenuHandler.SetBody(ce.vanity.GetMetadata().lore);

            ContextMenuHandler.AddOption("Un-Equip", () => {
                _bagScriptableObject.AddItem(ce.vanity);
                ItemUnEquipped(ce.vanity, Party.GetPartyMemberIndex(Party.GetCurrentLeader()));
                ContextMenuHandler.Hide();
            });
        };
        ArmourSlotRightClick = delegate()
        {
            ContextMenuHandler.Show(ContextMenuType.ContextMenu);

            ContextMenuHandler.SetTitle(ce.armour.GetMetadata().name);
            ContextMenuHandler.SetSubtitle(ce.armour.GetMetadata().category);
            ContextMenuHandler.SetBody(ce.armour.GetMetadata().lore);

            ContextMenuHandler.AddOption("Un-Equip", () => {
                _bagScriptableObject.AddItem(ce.armour);
                ItemUnEquipped(ce.armour, Party.GetPartyMemberIndex(Party.GetCurrentLeader()));
                ContextMenuHandler.Hide();
            });
        };
        WeaponSlotRightClick = delegate()
        {
            ContextMenuHandler.Show(ContextMenuType.ContextMenu);

            ContextMenuHandler.SetTitle(ce.weapon.GetMetadata().name);
            ContextMenuHandler.SetSubtitle(ce.weapon.GetMetadata().category);
            ContextMenuHandler.SetBody(ce.weapon.GetMetadata().lore);

            ContextMenuHandler.AddOption("Un-Equip", () => {
                _bagScriptableObject.AddItem(ce.weapon);
                ItemUnEquipped(ce.weapon, Party.GetPartyMemberIndex(Party.GetCurrentLeader()));
                ContextMenuHandler.Hide();
            });
        };

        if (ce.vanity.itemID != ItemID.manaport_nothing)
        {
            _vanitySlot.GetComponent<Button_UI>().MouseRightClickFunc += VanitySlotRightClick;
        }
        else
        {
            _vanitySlot.GetComponent<Button_UI>().MouseRightClickFunc -= VanitySlotRightClick;
        }
        _armourSlot.GetComponent<Button_UI>().MouseRightClickFunc += ArmourSlotRightClick;
        _weaponSlot.GetComponent<Button_UI>().MouseRightClickFunc += WeaponSlotRightClick;
    }
}
