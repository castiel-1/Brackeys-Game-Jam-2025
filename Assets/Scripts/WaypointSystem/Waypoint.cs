using NUnit.Framework;
using UnityEngine;

public class Waypoint : MonoBehaviour 
{
    [SerializeField, Min(0f)] private float _waitTime = 0;
    public float WaitTime => _waitTime;
}
