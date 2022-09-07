using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.PartySystem.Inventory;
using Manapotion.PartySystem;

namespace Manapotion.UI
{
    public enum ItemCategory 
    {
        CONSUMABLE,
        INGREDIENT,
        MATERIAL
    }

    public class BagUIManager : InventoryUIBase
    {
        private MainUIManager _main;

        public bool[] slotsFilled;
        public int filledBagSlots = 0;
        public GameObject[] slots;

        public BagUIManager(MainUIManager main)
        {
            _main = main;

            slotsFilled = new bool[(int)ItemID.MAXCOUNT];
            slots = new GameObject[(int)ItemID.MAXCOUNT];

            // initialize reference array
            for (int i = 0; i < slotsFilled.Length; i++) {
                slotsFilled[i] = false; 
            }
            _main.emptyImage.gameObject.SetActive(true);
        }

        public void Refresh()
        {
            for (int i = 0; i < (int)ItemID.MAXCOUNT; i++)
            {
                string itemId = Party.Instance.partyInventory.bag.slots[i].itemId.ToString();
                string[] properties = itemId.Split('_');
                
                ItemCategory category = (ItemCategory)Enum.Parse(typeof(ItemCategory), properties[0]);
                
                if (Party.Instance.partyInventory.bag.slots[i].quantity > 0 && slotsFilled[i] == false)
                {
                    AddSlot(category, itemId, i);
                    slotsFilled[i] = true;
                }

                if (slots[i] != null)
                {
                    if (slots[i].GetComponent<BagSlotUIHandler>().number.text != null)
                    {
                        slots[i].GetComponent<BagSlotUIHandler>().number.text = Party.Instance.partyInventory.bag.slots[i].quantity.ToString();
                    }
                }
            }

            // check if bag is empty and if so activate the empty image
            int num = 0;
            foreach (bool slotState in slotsFilled)
            {
                if (slotState == true) num++;
            }
            
            if (num == 0)
            {
                _main.emptyImage.gameObject.SetActive(true);
            }
            else
            {
                _main.emptyImage.gameObject.SetActive(false);
            }
        }

        public void AddSlot(ItemCategory category, string name, int i)
        {
            if (category == ItemCategory.CONSUMABLE)
            {
                GameObject slot = MainUIManager.Instantiate(_main.consumableSlot, _main.bagSlotParent.transform);
                slots[i] = slot;
                slots[i].name = name;
            }
            else if (category == ItemCategory.INGREDIENT)
            {
                GameObject slot = MainUIManager.Instantiate(_main.ingredientSlot, _main.bagSlotParent.transform);
                slots[i] = slot;
                slots[i].name = name;
            }
            else if (category == ItemCategory.MATERIAL)
            {
                GameObject slot = MainUIManager.Instantiate(_main.materialSlot, _main.bagSlotParent.transform);
                slots[i] = slot;
                slots[i].name = name;
            }

            slots[i].GetComponent<BagSlotUIHandler>().item.sprite = _main.itemIcons[i].Icon;
        }
    }
}
