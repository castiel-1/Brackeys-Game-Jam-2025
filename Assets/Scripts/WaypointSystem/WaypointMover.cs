using System.Collections;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Transform _waypointParent;
    [SerializeField] private bool _loopPath = true;

    public float moveSpeed; // set by enemy in awake

    private Waypoint[] _waypoints;
    private int _currentIndex;
    private bool _isWaiting;


    private void Start()
    {
        _waypoints = new Waypoint[_waypointParent.childCount];

        for(int i = 0; i < _waypointParent.childCount; i++)
        {
            _waypoints[i] = _waypointParent.GetComponentInChildren<Waypoint>();
        }
    }

    // add if game paused: return
    void Update()
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

        // if we are close to the target waypoint...
        if(Vector2.Distance(transform.position, position) < 0.1f)
        {
            // if the target waypoint has a wait time...
            if(currentWaypoint.WaitTime != 0)
            {
                StartCoroutine(WaitAtWaypointRoutine(currentWaypoint.WaitTime));
            }
        }
    }

    IEnumerator WaitAtWaypointRoutine(float duration)
    {
        _isWaiting = true;

        yield return new WaitForSeconds(duration);

        _currentIndex = _loopPath ? (_currentIndex + 1) % _waypoints.Length : Mathf.Min(_currentIndex++, _waypoints.Length);

        _isWaiting = false;
    }
}
