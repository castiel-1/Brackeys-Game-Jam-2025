using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public event Action<Vector2> OnMovingToWaypoint;

    [SerializeField] private Transform _waypointParent;
    [SerializeField] private bool _loopPath = true;

    [HideInInspector] public float moveSpeed; // set by enemy in awake

    private Vector2 _viewDirection = Vector2.zero;
    private Waypoint[] _waypoints;
    private int _currentIndex = 0;
    private bool _isWaiting;
    private bool _movingForward = true;
    private Animator _animator;

    public Waypoint[] Waypoints => _waypoints;

    private void Start()
    {
        _waypoints = new Waypoint[_waypointParent.childCount];
        _waypoints = _waypointParent.GetComponentsInChildren<Waypoint>();

        _animator = GetComponent<Animator>();

        _viewDirection = (_waypoints[_currentIndex].transform.position - transform.position).normalized;
        OnMovingToWaypoint?.Invoke(_viewDirection);

        // debugging
        Debug.Log("view direction = " + _viewDirection);
    }

    // TODO add if game paused: return
    void FixedUpdate()
    {
        if (_isWaiting)
        {
            return;
        }

        MoveToWaypoint();

    }

    private void MoveToWaypoint()
    {
        Waypoint currentWaypoint = _waypoints[_currentIndex];

        Vector2 position = currentWaypoint.transform.position;

        transform.position = Vector2.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);

        _viewDirection = position - (Vector2)transform.position;

        // if we are close to the target waypoint...
        if(Vector2.Distance(transform.position, position) < 0.1f)
        {
            int prevIndex = _currentIndex;

            // set index to next correct waypoint
            // if loop...
            if (_loopPath)
            {
                _currentIndex = (_currentIndex + 1) % _waypoints.Length;
            }
            // if back and forth...
            else
            {
                if (_movingForward)
                {
                    if (_currentIndex == _waypoints.Length - 1)
                    {
                        _movingForward = false;
                    }
                    else
                    {
                        _currentIndex++;
                    }
                }
                else
                {
                    if (_currentIndex == 0)
                    {
                        _movingForward = true;
                    }
                    else
                    {
                        _currentIndex--;
                    }

                }
            }


            // if we need to wait at this waypoint...
            if (currentWaypoint.WaitTime > 0)
            {
                StartCoroutine(WaitAtWaypointRoutine(
                    currentWaypoint.WaitTime,
                    currentWaypoint.ViewDirectionWhileWait,
                    _waypoints[_currentIndex].transform.position
                ));
            }
            else
            {
                // no waiting — update look immediately
                Vector2 newTarget = _waypoints[_currentIndex].transform.position;
                _viewDirection = (newTarget - (Vector2)transform.position).normalized;
                OnMovingToWaypoint?.Invoke(_viewDirection);
            }
          
        }
    }

    IEnumerator WaitAtWaypointRoutine(float duration, Vector2 viewDirectionWhileWaiting, Vector2 nextTargetPos)
    {
        _isWaiting = true;
        _animator.SetBool("isMoving", false);

        // face the forced direction while waiting
        _viewDirection = viewDirectionWhileWaiting.normalized;
        OnMovingToWaypoint?.Invoke(_viewDirection);

        yield return new WaitForSeconds(duration);

        // after waiting, look toward next waypoint
        _viewDirection = (nextTargetPos - (Vector2)transform.position).normalized;
        OnMovingToWaypoint?.Invoke(_viewDirection);

        _isWaiting = false;
        _animator.SetBool("isMoving", true);
    }
}
