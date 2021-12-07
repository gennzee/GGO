using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Use Item", menuName = "Inventory System/Items/UseItem")]
public class UseObjectItem : ItemObject
{
    public int restoreHealthValue;

    private void Awake()
    {
        itemType = ItemType.UseItem;
    }
}
