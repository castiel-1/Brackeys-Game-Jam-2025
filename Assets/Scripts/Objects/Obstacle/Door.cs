using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool _isLocked = true;

    public void Intearct()
    {
        if (CanInteract())
        {
            Unlock();
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

        //disable the door collider
        GetComponent<BoxCollider2D>().enabled = false;

        // TODO add animator bool for displaying open sprite
    }
}
