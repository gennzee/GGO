using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private InventoryObject inventory;

    private Dictionary<InventoryObject.InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventoryObject.InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < inventory.listItemObjects.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.listItemObjects[i]))
            {
                itemsDisplayed[inventory.listItemObjects[i]].GetComponentInChildren<Text>().text = inventory.listItemObjects[i].amount.ToString();
            }
            else
            {
                CreateItemOnInventory(i);
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.listItemObjects.Count; i++)
        {
            CreateItemOnInventory(i);            
        }
    }

    private void CreateItemOnInventory(int i)
    {
        GameObject obj = Instantiate(inventory.listItemObjects[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<Image>().sprite = inventory.listItemObjects[i].item.icon;
        obj.GetComponentInChildren<Text>().text = inventory.listItemObjects[i].amount.ToString();
        itemsDisplayed.Add(inventory.listItemObjects[i], obj);
    }
}
