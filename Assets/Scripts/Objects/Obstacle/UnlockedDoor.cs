using UnityEngine;

public class UnlockedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip _doorOpenAudio;
    [SerializeField] private AudioClip _doorCloseAudio;
    [SerializeField] private float _volume = 0.2f;

    [SerializeField] private bool _horizontal;

    [SerializeField] private Sprite _doorClosed;
    [SerializeField] private Sprite _doorOpenRight;
    [SerializeField] private Sprite _doorOpenLeft;
    [SerializeField] private bool _facingRight;

    private bool _open = false;

    public string Interact()
    {
        bool opened = ToggleDoor();

        if (opened)
        {
            SoundFXManager.Instance.PlaySoundFXClip(_doorOpenAudio, transform, _volume);
            return "Door opened!";
        }
        else
        {
            SoundFXManager.Instance.PlaySoundFXClip(_doorCloseAudio, transform, _volume);
            return "Door closed!";
        }

    }

    public bool CanInteract()
    {
        return true;
    }

    private bool ToggleDoor() // if true -> opened, else -> closed
    {
        _open = !_open;

        if(_open)
        {
            if (_horizontal)
            {
                GetComponent<SpriteRenderer>().sprite = null;
                GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = _facingRight ? _doorOpenRight : _doorOpenLeft;
                GetComponent<BoxCollider2D>().isTrigger = true;
            }

            return true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = _doorClosed;
            GetComponent<BoxCollider2D>().isTrigger = false;

            return false;
        }
    }
}
