using UnityEngine;
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

        public InventoryUIManager(MainUIManager main)
        {
            _main = main;

            bagUI = new BagUIManager(); 
            equipUI = new EquipUIManager(_main); 
            beastiaryUI = new InventoryUIBase();

            bagUI.attachedObjects = new GameObject[] { _main.bagUIObject };
            equipUI.attachedObjects = new GameObject[] { _main.leftEquipUIObject, _main.rightEquipUIObject };
            beastiaryUI.attachedObjects = new GameObject[] { _main.beastiaryUIObject }; 
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
                equipUI.Hide();
                beastiaryUI.Hide();
            }
            else
            {
                inventoryState = InventoryState.None;
                bagUI.Hide();
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
                bagUI.Hide();
                beastiaryUI.Hide();
            }
            else
            {
                inventoryState = InventoryState.None;
                equipUI.Hide();
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
                bagUI.Hide();
                equipUI.Hide();
            }
            else
            {
                inventoryState = InventoryState.None;
                beastiaryUI.Hide();
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
