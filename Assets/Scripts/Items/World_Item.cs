using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Item : MonoBehaviour
{
    public static World_Item SpawnItemWorld(Vector3 pos, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfWorldItem, pos, Quaternion.identity);
    
        World_Item world_Item = transform.GetComponent<World_Item>();
        world_Item.SetItem(item);

        return world_Item;
    }

    private Item _item;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private BagScriptableObject _bagScriptableObject;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(Item item)
    {
        _item = item;
        spriteRenderer.sprite = _item.GetSprite();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerPartyLeader")
        {
            _bagScriptableObject.AddItem(_item);
            Destroy(this.gameObject);
        }
    }
}
