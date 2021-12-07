using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { EquipmentItem, UseItem, QuestItem }
public abstract class ItemObject : ScriptableObject
{
    public ItemType itemType;
    public GameObject prefab;
    public Sprite icon;
    [TextArea(15,20)]
    public string description;
}
