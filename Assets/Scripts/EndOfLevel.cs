using UnityEngine;

public class EndOfLevel : MonoBehaviour, IInteractable
{
    public bool CanInteract()
    {
        return true;
    }

    public string Interact()
    {
        GameManager.Instance.LoadNextLevel();

        return "";
    }
}
