using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manapotion.UI
{

    public enum UIState { Hidden, Hiding, Shown, Showing }

    public abstract class UI_InventoryBase : MonoBehaviour
    {
        public UIState uiState = UIState.Hidden;

        public List<RectTransform> transforms;

        [SerializeField]
        protected MainUIManager main;

        /// <summary>
        /// Shows the UI element.
        /// </summary>
        public void Show()
        {
            if (uiState == UIState.Shown)
            {
                return;
            }

            uiState = UIState.Showing;
            Abstract_Show();
        }
        protected abstract void Abstract_Show();

        /// <summary>
        /// Hides the UI element.
        /// </summary>
        public void Hide()
        {
            if (uiState == UIState.Hidden)
            {
                return;
            }

            uiState = UIState.Hiding;
            Abstract_Hide();
        }
        protected abstract void Abstract_Hide();

        /// <summary>
        /// Refreshes the contents of the UI element.
        /// </summary>
        public void Refresh()
        {
            Virtual_Refresh();
        }
        protected virtual void Virtual_Refresh() { }

        public UIState GetUIState()
        {
            return uiState;
        }
    }
}

