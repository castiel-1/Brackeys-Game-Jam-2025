using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _itemUIPrefab;

    private GameObject _itemContainer;
    private Dictionary<string, GameObject> _itemUIDict = new();


    private void Awake()
    {
        _itemContainer = GameObject.FindGameObjectWithTag("Inventory");
    }

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

    public void SpawnItemInUI(string item, Sprite sprite)
    {

        GameObject itemUI = Instantiate(_itemUIPrefab, _itemContainer.transform);
        itemUI.transform.GetChild(1).GetComponent<Image>().sprite = sprite;

        _itemUIDict.Add(item, itemUI);
    }

    public void DespawnItemInUI(string item)
    {
        GameObject itemUI = _itemUIDict[item];

        Destroy(itemUI);

        _itemUIDict.Remove(item);   
    }

}
