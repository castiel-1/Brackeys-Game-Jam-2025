using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    public Sprite sprite;
    public float moveSpeed;
    [Range(0, 360)] public int viewAngle = 90;
    public float viewDistance = 10f;
    public float knockoutDuration;
    public float minimumAlertRaiseSpeed = 1;
    public float maximumAlertRaiseSpeed = 5;
}
