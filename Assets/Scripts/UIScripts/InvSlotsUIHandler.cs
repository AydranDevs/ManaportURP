using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;

public enum ItemCategory 
{
    CONSUMABLE,
    INGREDIENT,
    MATERIAL
}

public class InvSlotsUIHandler : MonoBehaviour
{
    public static InvSlotsUIHandler Instance;

    public Image emptyImage;

    public GameObject consumableSlot;
    public GameObject ingredientSlot;
    public GameObject materialSlot;

    public bool[] slotsFilled = new bool[(int)ItemID.MAXCOUNT];
    public GameObject[] slots = new GameObject[(int)ItemID.MAXCOUNT];
    public ItemIcon[] itemIcons = new ItemIcon[(int)ItemID.MAXCOUNT];

    private void Start() {
        Instance = this;
        
        // initialize reference array
        for (int i = 0; i < (int)ItemID.MAXCOUNT; i++) {
            slotsFilled[i] = false; 
        }
        emptyImage.gameObject.SetActive(true);
    }

    public void Refresh() {
        for (int i = 0; i < (int)ItemID.MAXCOUNT; i++) {
            string itemId = PartyInventory.Instance.bag.slots[i].itemId.ToString();
            string[] properties = itemId.Split('_');
            ItemCategory category = (ItemCategory)Enum.Parse(typeof(ItemCategory), properties[0]);
            
            if (PartyInventory.Instance.bag.slots[i].quantity > 0 && slotsFilled[i] == false) {
                AddSlot(category, itemId, i);
                slotsFilled[i] = true;
            }

            if (slots[i].GetComponent<InvSlotUIHandler>().number.text != null) {
                slots[i].GetComponent<InvSlotUIHandler>().number.text = PartyInventory.Instance.bag.slots[i].quantity.ToString();
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

        slots[i].GetComponent<InvSlotUIHandler>().item.sprite = itemIcons[i].Icon;
    }
}
