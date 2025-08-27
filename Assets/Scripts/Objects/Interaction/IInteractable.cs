using UnityEngine;

public interface IInteractable 
{
    public string Interact(); // string is message to be displayed after interaction
    public bool CanInteract();
}
