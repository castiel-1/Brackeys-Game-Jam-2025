using System.Collections;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private VisionCone _visionCone;
    [SerializeField] private Vector2 _viewDirection;
    [SerializeField, Range(0, 360)] private float _viewAngle = 90;
    [SerializeField] private float _viewDistance = 5;
    [SerializeField] private float _alertValue = 30;

    private bool _isDisabled = false;

    private void OnEnable()
    {
        _visionCone.OnPlayerInVisionCone += RaiseAlertMeter;
    }

    private void OnDisable()
    {
        _visionCone.OnPlayerInVisionCone -= RaiseAlertMeter;
    }

    private void Start()
    {
        // set viewAngle and viewRadius for vision cone
        _visionCone.ViewAngle = _viewAngle;
        _visionCone.ViewDistance = _viewDistance;
    }

    private void Update()
    {
        if (_isDisabled)
        {
            return;
        }
    }

    public void RaiseAlertMeter(float distanceToPlayer)
    {
        AlertMeterEvent.OnAlertMeterRaised?.Invoke(_alertValue * Time.deltaTime); // because detection runs in update we need to scale the raise here
    }

    public void DisableVisionCone(float seconds)
    {
        // debugging
        Debug.Log("disabled camera vision cone for: " + seconds + " seconds");

        StartCoroutine(DisableVisionConeRoutine(seconds));
    }

    IEnumerator DisableVisionConeRoutine(float seconds)
    {
        _visionCone.enabled = false;
        yield return new WaitForSeconds(seconds);
        _visionCone.enabled = true;
    }

}
