using System.Collections;
using TMPro;
using UnityEngine;

public class SetMessageManually : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _messageParent;
    [SerializeField] private TextMeshProUGUI _messageField;
    [SerializeField] private string _message;
    [SerializeField] private float _seconds;

    public bool CanInteract()
    {
        return true;
    }

    public string Interact()
    {
        DisplayMessage(_seconds, _message);
        return "";
    }

    IEnumerator DisplayMessage(float seconds, string message)
    {
        _messageParent.SetActive(true);
        _messageField.SetText(message);

        yield return new WaitForSeconds(seconds);

        _messageParent.SetActive(false);
    }
    
}
