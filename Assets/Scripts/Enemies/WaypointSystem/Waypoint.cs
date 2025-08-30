using NUnit.Framework;
using UnityEngine;

public class Waypoint : MonoBehaviour 
{
    [SerializeField, Min(0f)] private float _waitTime = 0;
    [SerializeField] private Vector2 _viewDirectionWhileWait;
    public float WaitTime => _waitTime;
    public Vector2 ViewDirectionWhileWait => _viewDirectionWhileWait;
}
