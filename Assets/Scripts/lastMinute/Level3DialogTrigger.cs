using UnityEngine;

public class Level3DialogTrigger : MonoBehaviour
{
    private DialogManager _dialogManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _dialogManager = FindFirstObjectByType<DialogManager>();

        _dialogManager.StartDialog();

        Destroy(gameObject);
    }
}
