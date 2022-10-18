using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Manapotion.UI
{
    public enum InventoryState { None, Bag, Equip, Beastiary }

    public class InventoryUIManager
    {
        private MainUIManager _main;

        public InventoryState inventoryState = InventoryState.None;

        public BagUIManager bagUI;
        public EquipUIManager equipUI;
        public InventoryUIBase beastiaryUI;

        public static UnityEvent bagOpenedEvent;
        public static UnityEvent bagClosedEvent;
        public static UnityEvent equipmentOpenedEvent;
        public static UnityEvent equipmentClosedEvent;
        public static UnityEvent beastiaryOpenedEvent;
        public static UnityEvent beastiaryClosedEvent;

        public InventoryUIManager(MainUIManager main)
        {
            _main = main;

            bagUI = new BagUIManager(); 
            equipUI = new EquipUIManager(_main); 
            beastiaryUI = new InventoryUIBase();

            bagUI.attachedObjects = new GameObject[] { _main.bagUIObject };
            equipUI.attachedObjects = new GameObject[] { _main.leftEquipUIObject, _main.rightEquipUIObject };
            beastiaryUI.attachedObjects = new GameObject[] { _main.beastiaryUIObject }; 

            if (bagOpenedEvent == null) bagOpenedEvent = new UnityEvent();
            if (bagClosedEvent == null) bagClosedEvent = new UnityEvent();
            if (equipmentOpenedEvent == null) equipmentOpenedEvent = new UnityEvent();
            if (equipmentClosedEvent == null) equipmentClosedEvent = new UnityEvent();
            if (beastiaryOpenedEvent == null) beastiaryOpenedEvent = new UnityEvent();
            if (beastiaryClosedEvent == null) beastiaryClosedEvent = new UnityEvent(); 
         }

        public void OnToggleBag(InputAction.CallbackContext context)
        {
            if (equipUI.hiding || equipUI.showing || beastiaryUI.hiding || beastiaryUI.showing)
            {
                return;
            }
            if (!context.started)
            {
                return;
            }

            if (inventoryState != InventoryState.Bag)
            {
                inventoryState = InventoryState.Bag;
                bagUI.Show();
                bagOpenedEvent.Invoke();
                equipUI.Hide();
                beastiaryUI.Hide();
            }
            else
            {
                inventoryState = InventoryState.None;
                bagUI.Hide();
                bagClosedEvent.Invoke();
            }
            
            if (AllUIsAreClosed())
            {
                _main.invOpen = false;
                GameStateManager.Instance.ChangeGameState(GameState.Main);
            }
        }

        public void OnToggleEquip(InputAction.CallbackContext context) {
            if (bagUI.hiding || bagUI.showing || beastiaryUI.hiding || beastiaryUI.showing)
            {
                return;
            }
            if (!context.started)
            {
                return;
            }

            if (inventoryState != InventoryState.Equip)
            {
                inventoryState = InventoryState.Equip;
                equipUI.Show();
                equipmentOpenedEvent.Invoke();
                bagUI.Hide();
                beastiaryUI.Hide();
            }
            else
            {
                inventoryState = InventoryState.None;
                equipUI.Hide();
                equipmentClosedEvent.Invoke();
            }
            
            if (AllUIsAreClosed())
            {
                _main.invOpen = false;
                GameStateManager.Instance.ChangeGameState(GameState.Main);
            }
        }

        public void OnToggleBeastiary(InputAction.CallbackContext context) {
            if (bagUI.hiding || bagUI.showing || equipUI.hiding || equipUI.showing)
            {
                return;
            }
            if (!context.started)
            {
                return;
            }

            if (inventoryState != InventoryState.Beastiary)
            {
                inventoryState = InventoryState.Beastiary;
                beastiaryUI.Show();
                beastiaryOpenedEvent.Invoke();
                bagUI.Hide();
                equipUI.Hide();
            }
            else
            {
                inventoryState = InventoryState.None;
                beastiaryUI.Hide();
                beastiaryClosedEvent.Invoke();
            }

            if (AllUIsAreClosed())
            {
                _main.invOpen = false;
                GameStateManager.Instance.ChangeGameState(GameState.Main);
            }
        }

        private bool AllUIsAreClosed()
        {
            return !bagUI.active && !equipUI.active && !beastiaryUI.active;
        }
    }
}
