using UnityEngine;

public class EndOfLevel : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip _pickupClip;
    [SerializeField] private float _volume = 0.1f;

    public bool CanInteract()
    {
        return true;
    }

    public string Interact()
    {
        SoundFXManager.Instance.PlaySoundFXClip(_pickupClip, transform, _volume);

        GameManager.Instance.LoadNextLevel();

        return "";
    }
}
