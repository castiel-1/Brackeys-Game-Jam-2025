using System;
using Unity.VisualScripting;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public static event Action<float> OnPlayerInVisionCone;

    [SerializeField] private int _rayCount = 10; // how many rays we draw all together
    [SerializeField] private float _viewRadius = 40;
    [SerializeField] private LayerMask _blockVisionLayer;
    [SerializeField] private LayerMask _playerLayer;

    [HideInInspector] public float ViewAngle; // gets set by enemy in start
    [HideInInspector] public Vector2 ViewDirection; // gets set by enemy when changed

    private Mesh _mesh;

    private void Awake()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    private void Update()
    {
        DrawVisionCone();
        DetectPlayer();
    }

    public void DetectPlayer()
    {
        // do circle raycast to see if player is in radius
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, _viewRadius, _playerLayer);
        
        // if we detect the player in range...
        if(objectsInRange.Length > 0)
        {
            Transform target = objectsInRange[0].transform;

            // get direction to player
            Vector2 dirToPlayer = target.position - transform.position;

            // convert to angle
            float angleToPlayer = AngleFromDir(dirToPlayer);

            // if player position is in fov
            if(angleToPlayer < (ViewAngle / 2f))
            {
                float distanceToPlayer = dirToPlayer.magnitude;

                // if nothing between guard and player...
                if (!Physics2D.Raycast(transform.position, dirToPlayer, distanceToPlayer, _blockVisionLayer))
                {
                    // tell enemy that player is being detected
                    OnPlayerInVisionCone?.Invoke(distanceToPlayer);

                    // debugging
                    Debug.Log("player detected!");
                }
            }
        }

    }

    public void DrawVisionCone()
    {
        float angleStepSize = ViewAngle / (_rayCount - 1); // -1 so that we get exactly rayCount rays
        float angle = AngleFromDir(ViewDirection) + (ViewAngle / 2f); // start angle is where we are looking + half of fov

        // set up stuff for mesh
        Vector3[] vertices = new Vector3[_rayCount + 1];
        int[] triangles = new int[(_rayCount - 1) * 3];

        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i < _rayCount; i++)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, DirFromAngle(angle), _viewRadius, _blockVisionLayer);

            Vector3 vertex;

            // if no hit...
            if(hitInfo.collider == null)
            {
                vertex = DirFromAngle(angle) * _viewRadius;
            }
            // if hit...
            else
            {
                vertex = transform.InverseTransformPoint(hitInfo.point);
            }

            vertices[vertexIndex] = vertex;

            if(i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;

            angle -= angleStepSize;

        }

        // give vertices and triangles to mesh
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
    }

    private Vector3 DirFromAngle(float angleInDegrees)
    {
        Vector3 direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angleInDegrees), Mathf.Sin(Mathf.Deg2Rad * angleInDegrees), 0);

        return direction;
    }

    private float AngleFromDir (Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(angle < 0)
        {
            angle += 360;
        }

        return angle;
    }
}
