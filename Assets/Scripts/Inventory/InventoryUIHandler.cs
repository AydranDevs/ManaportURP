using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory 
{
    public class InventoryUIHandler : MonoBehaviour
    {
        public GameObject bag;
        public GameObject equipment;
        public GameObject beastiary;

        public bool invBagOpen = false;
        public bool invEquipOpen = false;
        public bool invBeastiaryOpen = false;

        public int filledBagSlots = 0;

        public GameStateManager gameManager;
        public static InventoryUIHandler Instance;

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            gameManager = GameStateManager.Instance;
        }

        // public void Hide() {
        //     LTDescr tweenObject;
        //     tweenObject = LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.3f);
        //     tweenObject.setEase(LeanTweenType.easeOutQuad);
        // }

        // public void Show() {
        //     LTDescr tweenObject;
        //     tweenObject = LeanTween.move(gameObject.GetComponent<RectTransform>(), new Vector3(150, 0, 0), 0.3f);
        //     tweenObject.setEase(LeanTweenType.easeOutQuad);
        // }
        

        public void OnToggleBag(InputAction.CallbackContext context) {
            if (!context.started) return;

            ToggleInv("bag");
        }

        public void OnToggleEquip(InputAction.CallbackContext context) {
            if (!context.started) return;

            ToggleInv("equip");
        }

        public void OnToggleBeastiary(InputAction.CallbackContext context) {
            if (!context.started) return;

            ToggleInv("beastiary");
        }

        private void ToggleInv(string menu) {
            if (menu == "bag") {
                invBagOpen = !invBagOpen;
                invEquipOpen = false;
                invBeastiaryOpen = false;
            }else if (menu == "equip") {
                invBagOpen = false;
                invEquipOpen = !invEquipOpen;
                invBeastiaryOpen = false;
            }else if (menu == "beastiary") {
                invBagOpen = false;
                invEquipOpen = false;
                invBeastiaryOpen = !invBeastiaryOpen;
            }

            if (invBagOpen) {
                BagUIHandler.ShowBag();
                EquipmentUIHandler.HideEquipment();
                BeastiaryUIHandler.HideBeastiary();
            }else if (invEquipOpen) {
                BagUIHandler.HideBag();
                EquipmentUIHandler.ShowEquipment();
                BeastiaryUIHandler.HideBeastiary();
            }else if (invBeastiaryOpen) {
                BagUIHandler.HideBag();
                EquipmentUIHandler.HideEquipment();
                BeastiaryUIHandler.ShowBeastiary();
            }

            // set Game State to inventory
            if (invBagOpen || invEquipOpen || invBeastiaryOpen) {
                gameManager.ChangeGameState(GameState.Inv);
                InvSlotsUIHandler.Instance.Refresh();
            }else {
                gameManager.ChangeGameState(GameState.Main);
                
                BagUIHandler.HideBag();
                EquipmentUIHandler.HideEquipment();
                BeastiaryUIHandler.HideBeastiary();
            }
        }
    }
}
