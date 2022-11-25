using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    // public static ItemMetadataManager itemMetadataManager { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        //  itemMetadataManager = new ItemMetadataManager();
    }

    public Transform pfWorldItem;
}
