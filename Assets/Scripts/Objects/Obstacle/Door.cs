using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip _doorLocked;
    [SerializeField] private AudioClip _doorUnlock;

    private InventoryController _inventoryController;

    private bool _isLocked = true;
    private Animator _animator;

    private void Awake()
    {
        _inventoryController = FindFirstObjectByType<InventoryController>();
        _animator = GetComponentInChildren<Animator>();
    }

    public string Interact()
    {
        if (_inventoryController.HasItem("key"))
        {
            SoundFXManager.Instance.PlaySoundFXClip(_doorUnlock, transform, 0.07f);

            Unlock();
            _inventoryController.RemoveItemFromInventory("key");

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
        _animator.SetBool("isOpen", true);

        //disable the door collider
        GetComponent<BoxCollider2D>().enabled = false;

        // TODO add animator bool for displaying open sprite
    }
}
