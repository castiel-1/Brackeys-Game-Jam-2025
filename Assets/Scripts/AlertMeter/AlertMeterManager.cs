using UnityEngine;
using UnityEngine.UI;

public class AlertMeterManager : MonoBehaviour
{
    [SerializeField] private float _alertMeterMax = 100;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        AlertMeterEvent.OnAlertMeterRaised += RaiseAlertMeter;
    }

    private void OnDisable()
    {
        AlertMeterEvent.OnAlertMeterRaised -= RaiseAlertMeter;
    }

    private void Start()
    {
        _slider.maxValue = _alertMeterMax;
    }

    private void RaiseAlertMeter(float amount)
    {
        // debugging
        Debug.Log("alert meter raised in alert meter");

        if(_slider.value + amount >= _alertMeterMax)
        {
            // restart level, fail screen
        }

        _slider.value += amount;
    }
}
