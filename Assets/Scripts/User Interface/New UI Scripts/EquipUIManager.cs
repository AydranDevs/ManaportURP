using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem.Inventory;
using Manapotion.PartySystem;
using Manapotion.Utilities;

namespace Manapotion.UI
{
    public class EquipUIManager : InventoryUIBase
    {
        private Party _party;
        private PartyInventory _partyInventory;

        private PartyMember _chosenMember;

        public EquipUIManager(MainUIManager main)
        {
            this.main = main;

            _party = Party.Instance;
            _partyInventory = _party.partyInventory;

            Party.OnPartyLeaderChanged += OnPartyLeaderChanged_GetChosenMember;
            OnPartyLeaderChanged_GetChosenMember();
        }

        private void OnPartyLeaderChanged_GetChosenMember()
        {
            _chosenMember = Party.GetCurrentLeader();
        }

        public override void Refresh()
        {
            // List<BagSlot> tempEquip = new List<BagSlot>();
            
            // for (int i = 0; i < (int)ItemID.MAXCOUNT; i++)
            // {
            //     string itemId = Party.Instance.partyInventory.bag.slots[i].itemId.ToString();
            //     string[] properties = itemId.Split('_');

            //     if (properties[0] != "WEAPON" && properties[0] != "ARMOUR" && properties[0] != "VANITY")
            //     {
            //         continue;
            //     }

            //     if (Party.Instance.partyInventory.bag.slots[i].quantity < 1)
            //     {
            //         continue;
            //     }

            //     tempEquip.Add(Party.Instance.partyInventory.bag.slots[i]);
            //     ItemCategory category = (ItemCategory)Enum.Parse(typeof(ItemCategory), properties[0]);

            //     AddSlot(category, itemId, i);
            //     if (slots[i] != null)
            //     {
            //         if (slots[i].GetComponent<BagSlotUIHandler>().number.text != null)
            //         {
            //             slots[i].GetComponent<BagSlotUIHandler>().number.text = Party.Instance.partyInventory.bag.slots[i].quantity.ToString();
            //         }
            //     }
            // }

            // for (int i = 0; i < tempEquip.Count; i++)
            // {
            //     equipment[i] = tempEquip[i];
            // }

            // Debug.Log("refreshed");
        }

        private void AddSlot(ItemCategory cat, string id, int i)
        {
            // if (slots[i] != null)
            // {
            //     return;
            // }

            // if (cat == ItemCategory.WEAPON)
            // {
            //     GameObject slot = MainUIManager.Instantiate(this.main.weaponSlot, this.main.equipmentSlotParent.transform);
            //     slots[i] = slot;
            //     slots[i].name = id;
            // }
            // else if (cat == ItemCategory.ARMOUR)
            // {
            //     GameObject slot = MainUIManager.Instantiate(this.main.armourSlot, this.main.equipmentSlotParent.transform);
            //     slots[i] = slot;
            //     slots[i].name = id;
            // }
            // else if (cat == ItemCategory.VANITY)
            // {
            //     GameObject slot = MainUIManager.Instantiate(this.main.vanitySlot, this.main.equipmentSlotParent.transform);
            //     slots[i] = slot;
            //     slots[i].name = id;
            // }

            // slots[i].GetComponent<BagSlotUIHandler>().item.sprite = this.main.itemIcons[i].Icon;
        }

        private void Equip(int i)
        {
            // _chosenMember.Equip(i, );
        }

        public override void Show()
        {
            if (active)
            {
                return;
            }

            LTDescr tweenObject1;
            tweenObject1 = LeanTween.move(attachedObjects[0].GetComponent<RectTransform>(), new Vector3(-30, 0, 0), 0.3f);
            tweenObject1.setEase(LeanTweenType.easeOutQuad);

            LTDescr tweenObject2;
            tweenObject2 = LeanTween.move(attachedObjects[1].GetComponent<RectTransform>(), new Vector3(-142, 0, 0), 0.3f);
            tweenObject2.setEase(LeanTweenType.easeOutQuad);
            active = true;

            Camera.main.GetComponent<PartyCam>().camZoomState = CamZoomState.ZoomingIn;
            main.dimmer.FadeOut();
            Party.SetLeaderChangeOverride(true);
        }

        public override void Hide()
        {
            if (!active)
            {
                return;
            }

            LTDescr tweenObject1;
            tweenObject1 = LeanTween.move(attachedObjects[0].GetComponent<RectTransform>(), new Vector3(-172, 0, 0), 0.3f);
            tweenObject1.setEase(LeanTweenType.easeOutQuad);

            LTDescr tweenObject2;
            tweenObject2 = LeanTween.move(attachedObjects[1].GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.3f);
            tweenObject2.setEase(LeanTweenType.easeOutQuad);
            active = false;

            Camera.main.GetComponent<PartyCam>().camZoomState = CamZoomState.ZoomingOut;
            main.dimmer.FadeIn();
            Party.SetLeaderChangeOverride(false);
        }
    }
}