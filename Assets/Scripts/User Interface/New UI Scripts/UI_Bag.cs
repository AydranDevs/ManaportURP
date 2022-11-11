using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.UI;
using Manapotion.PartySystem;
using Manapotion.Items;

namespace Manapotion.UI
{
    public class UI_Bag : UI_InventoryBase
    {
        [SerializeField]
        private BagScriptableObject _bagScriptableObject;

        [SerializeField]
        private EquipmentManagerScriptableObject _laurieEquipmentScriptableObject;
        [SerializeField]
        private EquipmentManagerScriptableObject _mirabelleEquipmentScriptableObject;
        [SerializeField]
        private EquipmentManagerScriptableObject _winsleyEquipmentScriptableObject;

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

        private void Awake()
        {
            _bagScriptableObject.bagItemListChangedEvent.AddListener(RefreshItems);
        }

        protected override void Abstract_Show()
        {
            main.dimmer.FadeIn();

            LTDescr tweenObject;
            tweenObject = LeanTween.move(transforms[0], new Vector3(-22, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
            if (tweenObject != null)
            {
                tweenObject.setOnComplete(() => { uiState = UIState.Shown; });
            }
        }

        protected override void Abstract_Hide()
        {
            main.dimmer.FadeOut();
            
            LTDescr tweenObject;
            tweenObject = LeanTween.move(transforms[0], new Vector3(-172, 0, 0), 0.3f);
            tweenObject.setEase(LeanTweenType.easeOutQuad);
            if (tweenObject != null)
            {
                tweenObject.setOnComplete(() => { uiState = UIState.Hidden; });
            }
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
                if (item.itemScriptableObject.itemCategory == Items.ItemCategory.Consumable)
                {
                    go = Instantiate(consumableItemSlotPrefab, bagItemSlotContainer);
                }
                else if (item.itemScriptableObject.itemCategory == Items.ItemCategory.Ingredient)
                {
                    go = Instantiate(ingredientItemSlotPrefab, bagItemSlotContainer);
                }
                else if (item.itemScriptableObject.itemCategory == Items.ItemCategory.Material)
                {
                    go = Instantiate(materialItemSlotPrefab, bagItemSlotContainer);
                }
                else if (item.itemScriptableObject.itemCategory == Items.ItemCategory.Armour)
                {
                    go = Instantiate(armourItemSlotPrefab, equipableItemSlotContainer);
                }
                else if (item.itemScriptableObject.itemCategory == Items.ItemCategory.Weapon)
                {
                    go = Instantiate(weaponItemSlotPrefab, equipableItemSlotContainer);
                }
                else if (item.itemScriptableObject.itemCategory == Items.ItemCategory.Vanity)
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

                    ContextMenuHandler.SetTitle(item.ToString());
                    ContextMenuHandler.SetSubtitle(item.itemScriptableObject.itemCategory.ToString());
                    ContextMenuHandler.SetBody(item.itemScriptableObject.itemDescription);

                    if (item.itemScriptableObject.equipable)
                    {
                        // check if item has equipment restrictions
                        if (item.itemScriptableObject.charIDsThatCanEquip.Length > 0)
                        {
                            
                            // check how many characters can equip this item
                            if (item.itemScriptableObject.charIDsThatCanEquip.Length > 1)
                            {
                                // check if current party leader can equip this item
                                var index = Array.FindIndex(item.itemScriptableObject.charIDsThatCanEquip, x => x == Party.GetPartyMemberIndex(Party.GetCurrentLeader()));
                                if (index >= 0)
                                {
                                    ContextMenuHandler.AddOption(
                                        string.Format(
                                            "Equip <size=75%><alpha=#44>(on {0}?)",
                                            Party.GetCurrentLeader().gameObject.name),
                                        () => {
                                            Party.GetCurrentLeader().equipmentManagerScriptableObject.EquipItem(
                                                new Item
                                                { 
                                                    itemScriptableObject = item.itemScriptableObject,
                                                    amount = 1
                                                }
                                            );
                                        }
                                    );
                                }
                                else // if not, add option that displays which characters can equip this item.
                                {
                                    int num = item.itemScriptableObject.charIDsThatCanEquip.Length;
                                    StringBuilder stringBuilder = new StringBuilder("<alpha=#44>Only ");
                                    for (int i = 0; i < num; i++)
                                    {
                                        stringBuilder.Append(Party.GetMember(i).gameObject.name);
                                        if (i == num - 1)
                                        {
                                            stringBuilder.Append(" can equip this.");
                                            break;
                                        }
                                        else
                                        {
                                            stringBuilder.Append(" and ");
                                        }
                                    }
                                    ContextMenuHandler.AddOption(stringBuilder.ToString(), () => { });
                                    ContextMenuHandler.GetOption(stringBuilder.ToString()).SetClickable(false);
                                }
                            }
                            else
                            {
                                // check if current party leader can equip this item
                                // index >= 0 if true
                                var index = Array.FindIndex(item.itemScriptableObject.charIDsThatCanEquip, x => x == Party.GetPartyMemberIndex(Party.GetCurrentLeader()));
                                if (index >= 0)
                                {
                                    ContextMenuHandler.AddOption(
                                        string.Format(
                                            "Equip <size=75%><alpha=#44>(on {0}?)",
                                            Party.GetCurrentLeader().gameObject.name),
                                        () => {
                                            Party.GetCurrentLeader().equipmentManagerScriptableObject.EquipItem(
                                                new Item
                                                { 
                                                    itemScriptableObject = item.itemScriptableObject,
                                                    amount = 1
                                                }
                                            );
                                        }
                                    );
                                }
                                else // if false, show greyed out option
                                {
                                    ContextMenuHandler.AddOption(
                                        string.Format(
                                            "<alpha=#44>Only {0} can equip this.",
                                            Party.GetMember(item.itemScriptableObject.charIDsThatCanEquip[0]).gameObject.name
                                        ),
                                        () => { }
                                    );
                                    
                                    // make the option unclickable
                                    ContextMenuHandler.GetOption(
                                        string.Format(
                                            "<alpha=#44>Only {0} can equip this.",
                                            Party.GetMember(item.itemScriptableObject.charIDsThatCanEquip[0]).gameObject.name
                                        )
                                    ).SetClickable(false);
                                }
                            }
                        }
                        else
                        {
                            ContextMenuHandler.AddOption(
                                string.Format(
                                    "Equip <size=75%><alpha=#44>(on {0}?)",
                                    Party.GetCurrentLeader().gameObject.name),
                                () => {
                                    Party.GetCurrentLeader().equipmentManagerScriptableObject.EquipItem(
                                        new Item
                                        { 
                                            itemScriptableObject = item.itemScriptableObject,
                                            amount = 1
                                        }
                                    );
                            });
                        }
                    }
                    else
                    {
                        ContextMenuHandler.AddOption(string.Format("Use <size=75%><alpha=#44>(on {0}?)", Party.GetCurrentLeader().gameObject.name), () => {
                            _bagScriptableObject.UseItem(item, Party.GetPartyMemberIndex(Party.GetCurrentLeader()));
                            ContextMenuHandler.Hide();
                        });
                    }
                });

                handle.item.sprite = item.itemScriptableObject.itemSprite;
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
}
