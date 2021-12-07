using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> listItemObjects = new List<InventorySlot>();

    public void AddItem(ItemObject item, int amount)
    {
        bool isItemExisted = false;
        for (int i = 0; i < listItemObjects.Count; i++)
        {
            if (listItemObjects[i].item == item)
            {
                listItemObjects[i].AddAmount(amount);
                isItemExisted = true;
                break;
            }
        }
        if (!isItemExisted)
        {
            listItemObjects.Add(new InventorySlot(item, amount));
        }        
    }

    [Serializable]
    public class InventorySlot
    {
        public ItemObject item;
        public int amount;
        public InventorySlot(ItemObject item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public void AddAmount(int value)
        {
            amount += value;
        }
    }
}
