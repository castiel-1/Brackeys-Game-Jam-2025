using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private WaypointMover _waypointMover;

    // vision cone 

    private Vector2 _viewDirection;
    private bool _isKnockedOut = false;
    private Vector2 _lastPosition;

    private void Start()
    {
        _waypointMover.moveSpeed = _enemySO.moveSpeed;
        _lastPosition = transform.position;

        _viewDirection = (_waypointMover.Waypoints[0].transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if(_isKnockedOut)
        {
            return;
        }

        if(((Vector2)transform.position - _lastPosition).sqrMagnitude > 0.0001f)
        {
            _viewDirection = ((Vector2)transform.position - _lastPosition).normalized;
            _lastPosition = transform.position;
        }

        Debug.DrawRay(transform.position, _viewDirection, Color.red);
    }

    public void KnockOutGuard()
    {
        if (!_isKnockedOut)
        {
            _isKnockedOut = true;


            StartCoroutine(KnockOutRoutine());
        }
    }

    IEnumerator KnockOutRoutine()
    {
        float timer = _enemySO.knockoutDuration;

        yield return new WaitForSeconds(timer);

        _isKnockedOut = false;
    }
}
