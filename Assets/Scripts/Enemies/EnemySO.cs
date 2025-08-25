using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    public Sprite sprite;
    public float moveSpeed;
    [Range(0, 260)] public int visionAngle;
    public float knockoutDuration;
}
