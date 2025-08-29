using System.Collections;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private VisionCone _visionCone;
    [SerializeField] private Vector2 _viewDirection = Vector2.up;
    [SerializeField, Range(0, 360)] private float _viewAngle = 90;
    [SerializeField] private float _viewDistance = 5;
    [SerializeField] private float _alertValue = 30;

    private bool _isOn;
    private bool _hasPower = true;
    private Animator _animator;
    public bool IsOn
    {
        set
        {
            _isOn = value;
            ApplyState();
        }
    }

    private void OnEnable()
    {
        _visionCone.OnPlayerInVisionCone += RaiseAlertMeter;
    }

    private void OnDisable()
    {
        _visionCone.OnPlayerInVisionCone -= RaiseAlertMeter;
    }

    private void Awake()
    {
        // set viewAngle and viewRadius for vision cone
        _visionCone.ViewAngle = _viewAngle;
        _visionCone.ViewDistance = _viewDistance;
        _visionCone.ViewDirection = _viewDirection.normalized;

        _animator = GetComponent<Animator>();
    }

    public void RaiseAlertMeter(float distanceToPlayer)
    {
        AlertMeterEvent.OnAlertMeterRaised?.Invoke(_alertValue * Time.deltaTime); // because detection runs in update we need to scale the raise here
    }

    public void Toggle()
    {
        if (!_hasPower)
        {
            return;
        }

        // debugging
        Debug.Log("camera toggled.");

        _isOn = !_isOn;
        ApplyState();
    }

    public void SetPower(bool hasPower)
    {
        _hasPower = hasPower;
        ApplyState();
    }

    private void ApplyState()
    {
        // debugging
        Debug.Log("camera " + this.name + " isOn: " + _isOn);

        bool active = _isOn && _hasPower;

        _visionCone.gameObject.SetActive(active);
        _animator.SetBool("cameraOn", active);
    }

}
