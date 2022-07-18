using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Inventory;

public class CharacterInteractionHandler : MonoBehaviour {
    public List<Transform> interactablesList;

    private void Start() {
        interactablesList = new List<Transform>();
    }
    
    public void AddToInventory(string itemId) {
        string[] properties = itemId.Split(' ');
        ItemID id = (ItemID)Enum.Parse(typeof(ItemID), properties[0]);
        PartyInventory.Instance.bag.AddItem(id, 1);
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (!context.started) return;

        interactablesList[0].gameObject.GetComponent<InteractableBase>().Interact();
        interactablesList.Remove(interactablesList[0]);
    }
}
