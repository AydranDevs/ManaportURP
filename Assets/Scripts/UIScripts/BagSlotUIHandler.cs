using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Manapotion.UI;

public class BagSlotUIHandler : MonoBehaviour {
    public Image item;
    public TextMeshProUGUI number;
    public Button button;

    public GUIClickController clickCtrl;

    private bool _contextMenuOpen = false;

    private void Awake()
    {
        // clickCtrl.onRight.AddListener(OpenContextMenu);
    }

    private void Update()
    {
        if (!_contextMenuOpen)
        {
            return;
        }
    }

    private void OpenContextMenu()
    {
        Debug.Log("Open context menu now");
        ContextMenuHandler.Show(ContextMenuType.ContextMenu);

        ContextMenuHandler.SetTitle("this is a context menu woahhh");
        ContextMenuHandler.SetSubtitle("big dick energy");
        ContextMenuHandler.SetBody("i like balls in my mouth");

        _contextMenuOpen = true;
    }
}
