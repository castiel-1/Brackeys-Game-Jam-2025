using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static event Action<string> OnItemAddedToInventory;
    public static event Action<string> OnItemRemovedFromInventory;

    private List<string> _itemsInInventory = new();

    public void AddItemToInventory(string item)
    {
        // debugging
        Debug.Log("item " + item + " added to inventory");

        _itemsInInventory.Add(item);

        OnItemAddedToInventory?.Invoke(item);
    }

    public void RemoveItemFromInventory(string item)
    {
        // debugging
        Debug.Log("item " + item + " removed from inventory");

        _itemsInInventory.Remove(item);

        OnItemRemovedFromInventory?.Invoke(item);
    }

    public bool HasItem(string item)
    {
        return _itemsInInventory.Contains(item);
    }

    public void GetItem(GameObject item)
    {
        
    }

}
