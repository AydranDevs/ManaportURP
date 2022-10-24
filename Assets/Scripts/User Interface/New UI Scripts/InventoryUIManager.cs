using UnityEngine;
using UnityEngine.InputSystem;

namespace Manapotion.UI
{
    public enum InventoryState { None, Bag, Equip, Beastiary }

    public class InventoryUIManager
    {
        private MainUIManager _main;

        public InventoryState inventoryState = InventoryState.None;

        public UI_Bag _uI_Bag;
        private UI_Equipment _uI_Equipment;
        public UI_Beastiary _uI_Beastiary;

        public InventoryUIManager(MainUIManager main)
        {
            _main = main;

            _uI_Bag = _main.uI_Bag;
            _uI_Equipment = _main.uI_Equipment;
            _uI_Beastiary = _main.uI_Beastiary;
        }

        public void OnToggleBag(InputAction.CallbackContext context)
        {
            if (_uI_Equipment.uiState == UIState.Hiding || 
                _uI_Equipment.uiState == UIState.Showing || 
                _uI_Bag.uiState == UIState.Hiding || 
                _uI_Bag.uiState == UIState.Showing || 
                _uI_Beastiary.uiState == UIState.Hiding || 
                _uI_Beastiary.uiState == UIState.Showing)
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
                _uI_Bag.Show();
                _uI_Equipment.Hide();
                _uI_Beastiary.Hide();
            }
            else
            {
                inventoryState = InventoryState.None;
                _uI_Bag.Hide();
            }
            
            if (AllUIsAreClosed())
            {
                _main.invOpen = false;
                GameStateManager.Instance.ChangeGameState(GameState.Main);
            }
        }

        public void OnToggleEquip(InputAction.CallbackContext context) {
            if (_uI_Equipment.uiState == UIState.Hiding || 
                _uI_Equipment.uiState == UIState.Showing || 
                _uI_Bag.uiState == UIState.Hiding || 
                _uI_Bag.uiState == UIState.Showing || 
                _uI_Beastiary.uiState == UIState.Hiding || 
                _uI_Beastiary.uiState == UIState.Showing)
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
                _uI_Equipment.Show();
                _uI_Bag.Hide();
                _uI_Beastiary.Hide();
            }
            else
            {
                inventoryState = InventoryState.None;
                _uI_Equipment.Hide();
            }
            
            if (AllUIsAreClosed())
            {
                _main.invOpen = false;
                GameStateManager.Instance.ChangeGameState(GameState.Main);
            }
        }

        public void OnToggleBeastiary(InputAction.CallbackContext context) {
            if (_uI_Equipment.uiState == UIState.Hiding || 
                _uI_Equipment.uiState == UIState.Showing || 
                _uI_Bag.uiState == UIState.Hiding || 
                _uI_Bag.uiState == UIState.Showing || 
                _uI_Beastiary.uiState == UIState.Hiding || 
                _uI_Beastiary.uiState == UIState.Showing)
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
                _uI_Beastiary.Show();
                _uI_Bag.Hide();
                _uI_Equipment.Hide();
            }
            else
            {
                inventoryState = InventoryState.None;
                _uI_Beastiary.Hide();
            }

            if (AllUIsAreClosed())
            {
                _main.invOpen = false;
                GameStateManager.Instance.ChangeGameState(GameState.Main);
            }
        }

        private bool AllUIsAreClosed()
        {
            if ((_uI_Bag.uiState == UIState.Hidden || _uI_Bag.uiState == UIState.Hiding)  &&
                (_uI_Equipment.uiState == UIState.Hidden || _uI_Equipment.uiState == UIState.Hiding) &&
                (_uI_Beastiary.uiState == UIState.Hidden || _uI_Beastiary.uiState == UIState.Hiding))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
