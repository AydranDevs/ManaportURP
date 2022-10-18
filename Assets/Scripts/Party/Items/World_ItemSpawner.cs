using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_ItemSpawner : MonoBehaviour
{
    public Item item;

    void Start()
    {
        World_Item.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);
    }
}
