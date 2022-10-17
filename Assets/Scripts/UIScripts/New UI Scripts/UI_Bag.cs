using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.UI;
using Manapotion.PartySystem;

public class UI_Bag : MonoBehaviour
{
    [SerializeField]
    private BagScriptableObject _bagScriptableObject;

    [SerializeField]
    private EquipmentScriptableObject _laurieEquipmentScriptableObject;
    [SerializeField]
    private EquipmentScriptableObject _mirabelleEquipmentScriptableObject;
    [SerializeField]
    private EquipmentScriptableObject _winsleyEquipmentScriptableObject;

    [SerializeField]
    private Transform bagItemSlotContainer;
    [SerializeField]
    private Transform equipableItemSlotContainer;
    [SerializeField]
    private Transform spellStoneItemSlotContainer;
    
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
    [SerializeField]
    private GameObject spellStoneItemSlotPrefab;
    

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
        foreach (Transform child in spellStoneItemSlotContainer)
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
            else if (item.GetMetadata().category == ItemCategories.Vanity)
            {
                go = Instantiate(vanityItemSlotPrefab, equipableItemSlotContainer);
            }
            else
            {
                go = Instantiate(spellStoneItemSlotPrefab, spellStoneItemSlotContainer);
            }

            // On right click of an item, open the context menu.
            var handle = go.GetComponent<BagSlotUIHandler>();
            handle.clickCtrl.onRight.AddListener(() =>
            {
                ContextMenuHandler.Show(ContextMenuType.ContextMenu);

                ContextMenuHandler.SetTitle(item.GetMetadata().name);
                ContextMenuHandler.SetSubtitle(item.GetMetadata().category);
                ContextMenuHandler.SetBody(item.GetMetadata().lore);

                if (item.GetMetadata().equipable)
                {
                    #region Check if item has equipment restrictions
                    bool foundCharIDThatCanEquip = false;
                    foreach (var i in item.GetMetadata().equipableData.charIDsThatCanEquip)
                    {
                        if (i == 0)
                        {
                            foundCharIDThatCanEquip = true;
                            ContextMenuHandler.AddOption(string.Format("Equip <size=75%><alpha=#44>(on {0}?)", Party.GetMember(0).gameObject.name), () => {
                                _laurieEquipmentScriptableObject.EquipItem(item);
                                ContextMenuHandler.Hide();
                            });
                            break;
                        }
                        else if (i == 1)
                        {
                            foundCharIDThatCanEquip = true;
                            ContextMenuHandler.AddOption(string.Format("Equip <size=75%><alpha=#44>(on {0}?)", Party.GetMember(1).gameObject.name), () => {
                                _mirabelleEquipmentScriptableObject.EquipItem(item);
                                ContextMenuHandler.Hide();
                            });
                            break;
                        }
                        else if (i == 2)
                        {
                            foundCharIDThatCanEquip = true;
                            ContextMenuHandler.AddOption(string.Format("Equip <size=75%><alpha=#44>(on {0}?)", Party.GetMember(2).gameObject.name), () => {
                                _winsleyEquipmentScriptableObject.EquipItem(item);
                                ContextMenuHandler.Hide();
                            });
                            break;
                        }
                    }
                    
                    if (!foundCharIDThatCanEquip)
                    {
                        ContextMenuHandler.AddOption(string.Format("Equip <size=75%><alpha=#44>(on {0}?)", Party.GetCurrentLeader().gameObject.name), () => {
                            if (Party.GetPartyMemberIndex(Party.GetCurrentLeader()) == 2)
                            {
                                _winsleyEquipmentScriptableObject.EquipItem(item);
                            }
                            else if (Party.GetPartyMemberIndex(Party.GetCurrentLeader()) == 1)
                            {
                                _mirabelleEquipmentScriptableObject.EquipItem(item);
                            }
                            else
                            {
                                _laurieEquipmentScriptableObject.EquipItem(item);
                            }
                            ContextMenuHandler.Hide();
                        });
                    }
                    #endregion
                }
                else
                {
                    ContextMenuHandler.AddOption(string.Format("Use <size=75%><alpha=#44>(on {0}?)", Party.GetCurrentLeader().gameObject.name), () => {
                        _bagScriptableObject.UseItem(item, Party.GetPartyMemberIndex(Party.GetCurrentLeader()));
                        ContextMenuHandler.Hide();
                    });
                }
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
