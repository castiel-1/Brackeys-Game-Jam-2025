using System.Collections;
using TMPro;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private GameObject _interactionPrompt;
    [SerializeField] private GameObject _interactMessage;
    [SerializeField] private float _displayTime = 2f;

    private IInteractable _interactableInRange = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // debugging
            Debug.Log("E was pressed");

            string displayMessage = _interactableInRange?.Interact();

            if(displayMessage != "")
            {
                StartCoroutine(DisplayInteractMessageRoutine(displayMessage, _displayTime));
                _interactionPrompt.SetActive(false);
            }
        }
    }

    public void DisplayMessage(string message, float displayTime)
    {
        StartCoroutine(DisplayInteractMessageRoutine(message, displayTime));
    }

    IEnumerator DisplayInteractMessageRoutine(string message, float displayTime)
    {
        _interactMessage.SetActive(true);
        _interactMessage.GetComponentInChildren<TextMeshPro>().text = message;

        yield return new WaitForSeconds(displayTime);

        _interactMessage.GetComponentInChildren<TextMeshPro>().text = "";
        _interactMessage.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // debugging
        Debug.Log("entered 2d collider");

        // if the component is interactable and in a state to do so...
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            // debugging
            Debug.Log("collider is interactable");

            _interactableInRange = interactable;
            _interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if the component is interactable and is the one that made our interaction pop up...
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == _interactableInRange)
        {
            _interactableInRange = null;
            _interactionPrompt.SetActive(false);
        }
    }

    
}
