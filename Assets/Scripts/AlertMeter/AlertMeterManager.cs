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
        if(_slider.value + amount >= _alertMeterMax)
        {
            // debugging
            Debug.Log("alert meter full");

            GameManager.Instance.GameOverScreen();
        }

        _slider.value += amount;

        // debugging
        Debug.Log("slider amount: " +  _slider.value);
    }
}
