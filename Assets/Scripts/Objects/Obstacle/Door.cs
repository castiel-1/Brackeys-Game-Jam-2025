using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _consumedKeyName;
    [SerializeField] private AudioClip _doorLocked;
    [SerializeField] private AudioClip _doorUnlock;
    [SerializeField] private bool horizontal;

    [SerializeField] private bool facingRight;
    [SerializeField] private Sprite _doorOpenLeft;
    [SerializeField] private Sprite _doorOpenRight;

    private InventoryController _inventoryController;

    private bool _isLocked = true;

    private void Awake()
    {
        _inventoryController = FindFirstObjectByType<InventoryController>();
    }

    public string Interact()
    {
        if (_inventoryController.HasItem(_consumedKeyName) || _inventoryController.HasItem("dough"))
        {
            SoundFXManager.Instance.PlaySoundFXClip(_doorUnlock, transform, 0.07f);

            Unlock();

            if (_consumedKeyName == "dough")
            {
                // only remove if key is dough
                _inventoryController.RemoveItemFromInventory("dough");
            }
            else if (_inventoryController.HasItem(_consumedKeyName))
            {
                // remove the specific key
                _inventoryController.RemoveItemFromInventory(_consumedKeyName);
            }

            return "Door unlocked!";
        }
        else
        {
            SoundFXManager.Instance.PlaySoundFXClip(_doorLocked, transform, 0.07f);

            return "It's locked...";
        }
        
    }

    public bool CanInteract()
    {
        return _isLocked;
    }

    // unlock AND open door 
    private void Unlock()
    {
        // debugging
        Debug.Log("opened door");

        _isLocked = false;

        if (horizontal)
        {
            //disable GO to reveal open frame from tilemap
            gameObject.SetActive(false);
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;

            if (facingRight)
            {
                GetComponent<SpriteRenderer>().sprite = _doorOpenRight;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = _doorOpenLeft;
            }
        }
     
    }
}
