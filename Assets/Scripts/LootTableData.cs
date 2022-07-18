using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Inventory;

[CreateAssetMenu(fileName = "LootTableData", menuName = "Manaport/LootTableData", order = 1)]
public class LootTableData : ScriptableObject {
    [SerializeField] private List<RewardItem> _items;
    [System.NonSerialized] private bool isInitialized = false;

    private float _totalWeight;
    
    private void Initialize() {
        if (isInitialized) return;

        _totalWeight = _items.Sum(item => item.weight);
        isInitialized = true;
    }

    public RewardItem GetRandomItem() {
        Initialize();

        float roll = Random.Range(0f, _totalWeight);

        foreach (var item in _items) {
            if (item.weight >= roll) return item;
            roll -= item.weight;
        }

        throw new System.Exception("reward gen failed");
    }

}

[System.Serializable]
public class RewardItem {
    public GameObject item;
    public float weight;
}
