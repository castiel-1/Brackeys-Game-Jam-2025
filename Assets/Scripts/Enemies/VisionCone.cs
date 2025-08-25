using UnityEngine;

public class VisionCone : MonoBehaviour
{
    [SerializeField] private int _meshResolution = 10; // how many rays we draw all together
    [SerializeField] private float viewRadius = 40;

    [HideInInspector] public float ViewAngle; // gets set by enemy in start

    private void OnEnable()
    {
        Enemy.OnViewDirectionUpdated += DrawVisionCone;
    }
    private void OnDisable()
    {
        Enemy.OnViewDirectionUpdated -= DrawVisionCone;   
    }

    public void DrawVisionCone(Vector2 viewDirection)
    {
        float stepSize = ViewAngle / _meshResolution;

        for(int i = 0; i < _meshResolution; i++)
        {

        }
    }

}
