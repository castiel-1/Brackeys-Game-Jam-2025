using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private WaypointMover _waypointMover;
    [SerializeField] private VisionCone _visionCone;
    [SerializeField] private Vector2 _viewDirection;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private float _volume = 0.2f;

    private bool _isKnockedOut = false;
    private Animator _animator;
    private InventoryController _inventoryController;

    private void OnEnable()
    {
        _visionCone.OnPlayerInVisionCone += RaiseAlertMeter;
        _waypointMover.OnMovingToWaypoint += UpdateViewDirection;
    }

    private void OnDisable()
    {
        _visionCone.OnPlayerInVisionCone -= RaiseAlertMeter;
        _waypointMover.OnMovingToWaypoint -= UpdateViewDirection;
    }

    private void Awake()
    {
        // set move speed in mover
        _waypointMover.moveSpeed = _enemySO.moveSpeed;
       
        // set viewAngle and viewRadius for vision cone
        _visionCone.ViewAngle = _enemySO.viewAngle;
        _visionCone.ViewDistance = _enemySO.viewDistance;

        // if the initial viewDirection is left we flip sprite
        if(_viewDirection.x < 0)
        {
            _sr.flipX = true;
        }

        _animator = GetComponent<Animator>();
        _inventoryController = GameObject.FindFirstObjectByType<InventoryController>();
    }

    private void Update()
    {
        if(_isKnockedOut)
        {
            return;
        }

        // debugging
        Debug.DrawRay(transform.position, _viewDirection, Color.red);
    }

    public bool CanInteract()
    {
        return !_isKnockedOut && _inventoryController.HasItem("nudelholz");
    }

    public string Interact()
    {
        // debugging
        Debug.Log("enemy knocked out");

        SoundFXManager.Instance.PlaySoundFXClip(_hitSound, transform, _volume);

        if (!_isKnockedOut)
        {
            StartCoroutine(KnockOutRoutine());
            return "Guard knocked out!";
        }

        // debugging
        Debug.Log("enemy up again");

        return "";
    }

    IEnumerator KnockOutRoutine()
    {
        _isKnockedOut = true;
        if(_waypointMover) _waypointMover.enabled = false;
        _visionCone.gameObject.SetActive(false);
        _animator.SetBool("isKnockedOut", _isKnockedOut);

        float timer = _enemySO.knockoutDuration;

        yield return new WaitForSeconds(timer + 0.5f);


        _isKnockedOut = false;
        _animator.SetBool("isKnockedOut", _isKnockedOut);

        yield return new WaitForSeconds(0.5f);

        _visionCone.gameObject.SetActive(true);
        if (_waypointMover) _waypointMover.enabled = true;

    }

    public void RaiseAlertMeter(float distanceToPlayer)
    {
        // debugging
        Debug.Log("raising alert meter");

        float clampedDistance = Mathf.Clamp(distanceToPlayer, 0, _enemySO.viewDistance);

        // if we are closer to player the value is closer to 0
        float t = Mathf.InverseLerp(0, _enemySO.viewDistance, clampedDistance);

        // if we are closer to 0 the raise is greater
        float raise = 1f - (t * t);

        float alertValue = Mathf.Lerp(_enemySO.minimumAlertRaiseSpeed, _enemySO.maximumAlertRaiseSpeed, raise);

        AlertMeterEvent.OnAlertMeterRaised?.Invoke(alertValue * Time.deltaTime); // because detection runs in update we need to scale the raise here
    }

    public void UpdateViewDirection(Vector2 direction)
    {
        _viewDirection = direction;

        if(_viewDirection.x > 0)
        {
            _sr.flipX = false;
        }
        else
        {
            _sr.flipX = true;
        }

        // set view direciton in vision cone
        _visionCone.ViewDirection = _viewDirection;
    }

    public void DisableVisionCone(float seconds)
    {
        // debugging
        Debug.Log("disabled enemy vision cone for: " + seconds + " seconds");

        StartCoroutine(DisableVisionConeRoutine(seconds));
    }

    IEnumerator DisableVisionConeRoutine(float seconds)
    {
        _visionCone.enabled = false;
        yield return new WaitForSeconds(seconds);
        _visionCone.enabled = true;
    }

}
