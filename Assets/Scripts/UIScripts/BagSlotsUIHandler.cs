using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manapotion.PartySystem.Inventory;
using Manapotion.PartySystem;
using Manapotion.UI;

public class BagSlotsUIHandler : MonoBehaviour
{
    public static BagSlotsUIHandler Instance;

    public Image emptyImage;

    public GameObject consumableSlot;
    public GameObject ingredientSlot;
    public GameObject materialSlot;

    public bool[] slotsFilled;
    public GameObject[] slots;
    public ItemIcon[] itemIcons;

    private void Awake() {
        slotsFilled = new bool[(int)ItemID.MAXCOUNT];
        slots = new GameObject[(int)ItemID.MAXCOUNT];
        itemIcons = new ItemIcon[(int)ItemID.MAXCOUNT];
    }

    private void Start() {
        Instance = this;
        
        // initialize reference array
        for (int i = 0; i < slotsFilled.Length; i++) {
            slotsFilled[i] = false; 
        }
        emptyImage.gameObject.SetActive(true);
    }

    public void Refresh() {
        for (int i = 0; i < (int)ItemID.MAXCOUNT; i++) {
            string itemId = Party.Instance.partyInventory.bag.slots[i].itemId.ToString();
            string[] properties = itemId.Split('_');
            ItemCategory category = (ItemCategory)Enum.Parse(typeof(ItemCategory), properties[0]);
            
            if (Party.Instance.partyInventory.bag.slots[i].quantity > 0 && slotsFilled[i] == false) {
                AddSlot(category, itemId, i);
                slotsFilled[i] = true;
            }

            if (slots[i].GetComponent<BagSlotUIHandler>().number.text != null) {
                slots[i].GetComponent<BagSlotUIHandler>().number.text = Party.Instance.partyInventory.bag.slots[i].quantity.ToString();
            }
        }

        // check if bag is empty and if so activate the empty image
        int num = 0;
        foreach (bool slotState in slotsFilled) {
            if (slotState == true) num++;
        }
        if (num == 0) { emptyImage.gameObject.SetActive(true); } else { emptyImage.gameObject.SetActive(false); }
    }

    public void AddSlot(ItemCategory category, string name, int i) {
        if (category == ItemCategory.CONSUMABLE) {
            GameObject slot = Instantiate(consumableSlot, this.transform);
            slots[i] = slot;
            slots[i].name = name;
            RectTransform slotTransform = slot.GetComponent<RectTransform>();
        }else if (category == ItemCategory.INGREDIENT) {
            GameObject slot = Instantiate(ingredientSlot, this.transform);
            slots[i] = slot;
            slots[i].name = name;
            RectTransform slotTransform = slot.GetComponent<RectTransform>();
        }else if (category == ItemCategory.MATERIAL) {
            GameObject slot = Instantiate(materialSlot, this.transform);
            slots[i] = slot;
            slots[i].name = name;
            RectTransform slotTransform = slot.GetComponent<RectTransform>();
        }

        slots[i].GetComponent<BagSlotUIHandler>().item.sprite = itemIcons[i].Icon;
    }
}
