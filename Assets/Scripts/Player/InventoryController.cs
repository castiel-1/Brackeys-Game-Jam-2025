using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static event Action<GameObject> OnItemAddedToInventory;
    public static event Action<GameObject> OnItemRemovedFromInventory;

    private List<GameObject> _itemsInInventory = new();

    public void AddItemToInventory(GameObject item)
    {
        // debugging
        Debug.Log("item " + item.name + " added to inventory");

        _itemsInInventory.Add(item);

        OnItemAddedToInventory?.Invoke(item);
    }

    public void RemoveItemFromInventory(GameObject item)
    {
        // debugging
        Debug.Log("item " + item.name + " removed from inventory");

        _itemsInInventory.Remove(item);

        OnItemRemovedFromInventory?.Invoke(item);
    }

}
