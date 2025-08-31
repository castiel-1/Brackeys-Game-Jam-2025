using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{

    [SerializeField] private Sprite _nudelholzSprite;
    [SerializeField] private bool _hasNudelholz;
    [SerializeField] private bool _startDialog;

    private DialogManager _dialogManager;
    private InventoryController _inventoryController;

    private void OnEnable()
    {
        DialogManager.OnDialogFinished += AfterDialog;
    }

    private void OnDisable()
    {
        DialogManager.OnDialogFinished -= AfterDialog;
    }

    private void Start()
    {
        if (_startDialog)
        {
            // start dialog
            _dialogManager = FindFirstObjectByType<DialogManager>();
            _dialogManager.StartDialog();
        }
       
        // set up inventory with Nudelholz
        _inventoryController = FindFirstObjectByType<InventoryController>();

        if(_hasNudelholz &&_inventoryController)
        {
            _inventoryController.AddItemToInventory("nudelholz", _nudelholzSprite);
        }
    }

    private void AfterDialog()
    {
        GameManager.Instance.LoadNextLevel();
    }
    
}
