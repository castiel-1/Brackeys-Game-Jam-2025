using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private string _itemName;

    private bool _isPickedUp = false;
    private InventoryController _inventoryController;
    private Sprite _sprite;

    private void Awake()
    {
        _inventoryController = FindFirstObjectByType<InventoryController>();
        _sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public bool CanInteract()
    {
        return !_isPickedUp;
    }

    public string Interact()
    {
        // debugging
        Debug.Log("sprite picked up: " + _sprite.name);

        _inventoryController.AddItemToInventory(_itemName, _sprite);
        gameObject.SetActive(false);

        return "Picked up " + _itemName + "!";
    }
}
