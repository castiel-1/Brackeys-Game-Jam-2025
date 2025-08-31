using System.Collections;
using TMPro;
using UnityEngine;

public class SetMessageManually : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _messageParent;
    [SerializeField] private GameObject _messageField;
    [SerializeField] private string _message;
    [SerializeField] private float _seconds;
    [SerializeField] private AudioClip _audioClip;

    public bool CanInteract()
    {
        return true;
    }

    public string Interact()
    {
        StartCoroutine(DisplayMessage(_seconds, _message));
        SoundFXManager.Instance.PlaySoundFXClip(_audioClip, transform, 0.07f);
        return "";
    }

    IEnumerator DisplayMessage(float seconds, string message)
    {
        _messageParent.SetActive(true);
        _messageField.GetComponent<TextMeshPro>().SetText(message);

        yield return new WaitForSeconds(seconds);

        _messageParent.SetActive(false);

        GameManager.Instance.LoadNextLevel();
    }
    
}
