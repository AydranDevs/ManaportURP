using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.UI;

public class UI_Bag : MonoBehaviour
{
    [SerializeField]
    private BagScriptableObject _bagScriptableObject;

    [SerializeField]
    private Transform bagItemSlotContainer;
    [SerializeField]
    private Transform equipableItemSlotContainer;
    
    [SerializeField]
    private GameObject consumableItemSlotPrefab;
    [SerializeField]
    private GameObject ingredientItemSlotPrefab;
    [SerializeField]
    private GameObject materialItemSlotPrefab;
    [SerializeField]
    private GameObject armourItemSlotPrefab;
    [SerializeField]
    private GameObject weaponItemSlotPrefab;
    [SerializeField]
    private GameObject vanityItemSlotPrefab;
    

    [field: SerializeField]
    public List<Item> itemList { get; private set; }

    private bool _contextMenuOpen = false;

    private void Awake()
    {
        _bagScriptableObject.bagItemListChangedEvent.AddListener(RefreshItems);
    }

    private void RefreshItems()
    {
        foreach (Transform child in bagItemSlotContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in equipableItemSlotContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in _bagScriptableObject.GetItemList())
        {
            GameObject go;
            if (item.GetMetadata().category == ItemCategories.Consumable)
            {
                go = Instantiate(consumableItemSlotPrefab, bagItemSlotContainer);
            }
            else if (item.GetMetadata().category == ItemCategories.Ingredient)
            {
                go = Instantiate(ingredientItemSlotPrefab, bagItemSlotContainer);
            }
            else if (item.GetMetadata().category == ItemCategories.Material)
            {
                go = Instantiate(materialItemSlotPrefab, bagItemSlotContainer);
            }
            else if (item.GetMetadata().category == ItemCategories.Armour)
            {
                go = Instantiate(armourItemSlotPrefab, equipableItemSlotContainer);
            }
            else if (item.GetMetadata().category == ItemCategories.Weapon)
            {
                go = Instantiate(weaponItemSlotPrefab, equipableItemSlotContainer);
            }
            else
            {
                go = Instantiate(vanityItemSlotPrefab, equipableItemSlotContainer);
            }

            var handle = go.GetComponent<BagSlotUIHandler>();
            handle.clickCtrl.onRight.AddListener(() =>
            {
                ContextMenuHandler.Show(ContextMenuType.ContextMenu);

                ContextMenuHandler.SetTitle(item.GetMetadata().name);
                ContextMenuHandler.SetSubtitle(item.GetMetadata().lore);
                ContextMenuHandler.SetBody(item.GetMetadata().category);

                ContextMenuHandler.AddOption("Equip", () => {
                    Debug.Log("Equip");
                });
                ContextMenuHandler.AddOption("Drop", () => {
                    Debug.Log("Drop");
                });
            });

            handle.item.sprite = item.GetMetadata().sprite;
            if (item.amount > 1)
            {
                handle.number.SetText(item.amount.ToString());
            }
            else
            {
                handle.number.SetText("");
            }
        }
    }
}
