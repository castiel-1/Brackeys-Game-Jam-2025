using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private GameObject _interactionPrompt;

    private IInteractable _interactableInRange = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // debugging
            Debug.Log("E was pressed");

            _interactableInRange?.Intearct();

            // take icon away if interaction is over
            if (_interactableInRange.CanInteract())
            {
                _interactionPrompt.SetActive(false);
            }
        }
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
