using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private string _itemID;

    private bool _isPickedUp = false;
    private InventoryController _inventoryController;

    private void Awake()
    {
        _inventoryController = FindFirstObjectByType<InventoryController>();
    }

    public bool CanInteract()
    {
        return !_isPickedUp;
    }

    public string Interact()
    {
        _inventoryController.AddItemToInventory(_itemID);
        gameObject.SetActive(false);

        return "Picked up " + _itemID + "!";
    }
}
