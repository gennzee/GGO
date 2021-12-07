using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory System/Items/EquipmentItem")]
public class EquipmentObjectItem : ItemObject
{
    public int maxHpIncrease;
    public int maxMpIncrease;  
    
    private void Awake()
    {
        itemType = ItemType.EquipmentItem;
    }
}
