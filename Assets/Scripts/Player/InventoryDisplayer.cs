using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _itemHolder;
    [SerializeField] private GameObject _itemUIPrefab;

    private Dictionary<string, GameObject> _itemUIDict = new();

    private void OnEnable()
    {
        InventoryController.OnItemAddedToInventory += SpawnItemInUI;
        InventoryController.OnItemRemovedFromInventory += DespawnItemInUI;
    }

    private void OnDisable()
    {
        InventoryController.OnItemAddedToInventory -= SpawnItemInUI;
        InventoryController.OnItemRemovedFromInventory -= DespawnItemInUI;
    }

    public void SpawnItemInUI(string item)
    {
        GameObject itemUI = Instantiate(_itemUIPrefab, _itemHolder.transform);

        _itemUIDict.Add(item, itemUI);
    }

    public void DespawnItemInUI(string item)
    {
        GameObject itemUI = _itemUIDict[item];

        Destroy(itemUI);

        _itemUIDict.Remove(item);   
    }

}
