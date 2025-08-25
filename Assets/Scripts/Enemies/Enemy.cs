using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private WaypointMover _waypointMover;

    // path they follow
    // vision cone 

    private bool _isKnockedOut = false;

    private void Start()
    {
        _waypointMover.moveSpeed = _enemySO.moveSpeed;
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
