using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{

    [SerializeField] private Sprite _nudelholzSprite;
    [SerializeField] private Sprite _doughSprite;
    [SerializeField] private bool _hasNudelholz;
    [SerializeField] private bool _hasDough;
    [SerializeField] private bool _startDialog;
    [SerializeField] private bool _loadNextSceneAfterDialog = true;

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

        if(_hasDough &&_inventoryController)
        {
            _inventoryController.AddItemToInventory("dough", _doughSprite);
        }
    }

    private void AfterDialog()
    {
        if (_loadNextSceneAfterDialog)
        {
            GameManager.Instance.LoadNextLevel();
        }
    }
    
}
