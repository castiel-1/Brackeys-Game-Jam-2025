using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private GameObject _itemUIPrefab;

    private Dictionary<string, GameObject> _itemUIDict = new();
    private Image _itemSprite;

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

    private void Awake()
    {
        _itemSprite = _itemUIPrefab.transform.GetChild(1).GetComponent<Image>();
    }

    public void SpawnItemInUI(string item, Sprite sprite)
    {
        GameObject itemUI = Instantiate(_itemUIPrefab, _itemContainer.transform);
        _itemSprite.sprite = sprite;

        _itemUIDict.Add(item, itemUI);
    }

    public void DespawnItemInUI(string item)
    {
        GameObject itemUI = _itemUIDict[item];

        Destroy(itemUI);

        _itemUIDict.Remove(item);   
    }

}
