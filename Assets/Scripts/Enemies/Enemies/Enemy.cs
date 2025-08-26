using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private WaypointMover _waypointMover;
    [SerializeField] private VisionCone _visionCone;
    [SerializeField] private Vector2 _viewDirection;

    private bool _isKnockedOut = false;

    private void OnEnable()
    {
        VisionCone.OnPlayerInVisionCone += RaiseAlertMeter;
        WaypointMover.OnMovingToWaypoint += UpdateViewDirection;
    }

    private void OnDisable()
    {
        VisionCone.OnPlayerInVisionCone -= RaiseAlertMeter;
        WaypointMover.OnMovingToWaypoint -= UpdateViewDirection;
    }

    private void Start()
    {
        if(_waypointMover  != null)
        {
            // set move speed in mover
            _waypointMover.moveSpeed = _enemySO.moveSpeed;
        }
       
        // set viewAngle for vision cone
        _visionCone.ViewAngle = _enemySO.viewAngle;
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

    public void KnockOutGuard()
    {
        // debugging
        Debug.Log("enemy knocked out");

        if (!_isKnockedOut)
        {
            StartCoroutine(KnockOutRoutine());
        }

        // debugging
        Debug.Log("enemy up again");
    }

    IEnumerator KnockOutRoutine()
    {
        _isKnockedOut = true;
        if(_waypointMover) _waypointMover.enabled = false;
        _visionCone.gameObject.SetActive(false);

        float timer = _enemySO.knockoutDuration;

        yield return new WaitForSeconds(timer);

        _visionCone.gameObject.SetActive(true);
        if(_waypointMover) _waypointMover.enabled = true;
        _isKnockedOut = false;
    }

    public void RaiseAlertMeter(float distanceToPlayer)
    {
        // debugging
        Debug.Log("alert meter in enemy calculated");
    }

    public void UpdateViewDirection(Vector2 direction)
    {
        // debugging
        Debug.Log("view direction in enemy updated");
        _viewDirection = direction;

        // set view direciton in vision cone
        _visionCone.ViewDirection = _viewDirection;
    }
}
