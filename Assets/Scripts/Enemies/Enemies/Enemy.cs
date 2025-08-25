using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private WaypointMover _waypointMover;
    [SerializeField] private VisionCone _visionCone;

    // vision cone 

    private Vector2 _viewDirection;
    private bool _isKnockedOut = false;
    private Vector2 _lastPosition;

    private void Start()
    {
        if(_waypointMover  != null)
        {
            // set move speed in mover
            _waypointMover.moveSpeed = _enemySO.moveSpeed;

            // set initial enemy position
            _lastPosition = transform.position;

            // set initial view direction
            _viewDirection = (_waypointMover.Waypoints[0].transform.position - transform.position).normalized;
        }
       
        // set viewAngle for vision cone
        _visionCone.ViewAngle = _enemySO.viewAngle;

        _visionCone.DrawVisionCone(Vector2.right);
        
    }

    private void FixedUpdate()
    {
        if(_isKnockedOut)
        {
            return;
        }

        // if player moves...
        if(((Vector2)transform.position - _lastPosition).sqrMagnitude > 0.0001f)
        {
            // update view direction
            _viewDirection = ((Vector2)transform.position - _lastPosition).normalized;

            _lastPosition = transform.position;
        }

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
    }

    IEnumerator KnockOutRoutine()
    {
        _isKnockedOut = true;
        _waypointMover.enabled = false;

        float timer = _enemySO.knockoutDuration;

        yield return new WaitForSeconds(timer);

        _waypointMover.enabled = true;
        _isKnockedOut = false;
    }
}
