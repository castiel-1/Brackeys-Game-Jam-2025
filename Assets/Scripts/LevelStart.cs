using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{
    // things to do on level start...

    /* 1. dialog
     * 2. speech bubble with hints/that tells you what to do
     * 3. add nudelholz after level 1
     * 
     * 
     */

    [SerializeField] private Sprite _nudelholzSprite;

    private DialogManager _dialogManager;
    private InventoryController _inventoryController;
    

    private void Start()
    {
        // start dialog
        _dialogManager = FindFirstObjectByType<DialogManager>();
        _dialogManager.StartDialog();

        // set up inventory with Nudelholz
        _inventoryController = FindFirstObjectByType<InventoryController>();

        if(GameManager.Instance.GetCurrentLevel() != 0 && _inventoryController)
        {
            _inventoryController.AddItemToInventory("nudelholz", _nudelholzSprite);
        }


    }

    private void AddNudelholzToInventory()
    {

    }
}
