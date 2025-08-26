using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private List<GameObject> _itemsInInventory;

    private void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddItemToInventory(GameObject item)
    {
        // debugging
        Debug.Log("item " + item.name + " added to inventory");

        _itemsInInventory.Add(item);
    }

    public void RemoveItemFromInventory(GameObject item)
    {
        // debugging
        Debug.Log("item " + item.name + " removed from inventory");
        _itemsInInventory.Remove(item);
    }

}
