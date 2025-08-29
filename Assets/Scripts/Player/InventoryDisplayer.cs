using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _itemUIPrefab;

    private GameObject _itemContainer;
    private Dictionary<string, List<GameObject>> _itemUIDict = new();

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

        if (_itemUIDict.ContainsKey(item))
        {
            _itemUIDict[item].Add(itemUI);
        }
        else
        {
            List<GameObject> itemsGO = new();
            itemsGO.Add(itemUI);
            _itemUIDict[item] = itemsGO;
        }

    }

    public void DespawnItemInUI(string item)
    {
        GameObject itemUI = _itemUIDict[item][0];

        Destroy(itemUI);

        if (_itemUIDict[item].Count > 0)
        {
            _itemUIDict[item].RemoveAt(0);
        }
        else
        {
            _itemUIDict.Remove(item);
        }

    }

}
