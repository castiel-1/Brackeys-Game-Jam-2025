using System;
using System.Collections;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public static event Action<Vector2> OnMovingToWaypoint;

    [SerializeField] private Transform _waypointParent;
    [SerializeField] private bool _loopPath = true;

    [HideInInspector] public float moveSpeed; // set by enemy in awake

    private Vector2 _viewDirection = Vector2.zero;
    private Waypoint[] _waypoints;
    private int _currentIndex;
    private bool _isWaiting;
    private bool _movingForward = true;
    private Animator _animator;

    public Waypoint[] Waypoints => _waypoints;

    private void Start()
    {
        _waypoints = new Waypoint[_waypointParent.childCount];
        _waypoints = _waypointParent.GetComponentsInChildren<Waypoint>();

        _animator = GetComponent<Animator>();
    }

    // TODO add if game paused: return
    void FixedUpdate()
    {
        if (_isWaiting)
        {
            return;
        }

        MoveToWaypoint();

        OnMovingToWaypoint?.Invoke(_viewDirection);
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
            // if the target waypoint has a wait time...
            if(currentWaypoint.WaitTime != 0)
            {
                StartCoroutine(WaitAtWaypointRoutine(currentWaypoint.WaitTime));
            }

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
        }
    }

    IEnumerator WaitAtWaypointRoutine(float duration)
    {
        _isWaiting = true;

        _animator.SetBool("isMoving", false);

        yield return new WaitForSeconds(duration);

        _isWaiting = false;

        _animator.SetBool("isMoving", true);
    }
}
