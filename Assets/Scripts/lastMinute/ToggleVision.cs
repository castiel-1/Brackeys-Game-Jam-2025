using UnityEngine;

public class ToggleVision : MonoBehaviour
{
    [SerializeField] private GameObject _visionOveraly;

    private bool _overlayOn = false;

    public void ToggleLights()
    {
        _overlayOn = !_overlayOn;
        _visionOveraly.SetActive(_overlayOn);
    }
}
