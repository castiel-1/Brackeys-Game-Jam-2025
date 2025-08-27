using System;
using Unity.VisualScripting;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public event Action<float> OnPlayerInVisionCone;

    [SerializeField] private int _rayCount = 10; // how many rays we draw all together
    [SerializeField] private LayerMask _blockVisionLayer;
    [SerializeField] private LayerMask _playerLayer;

    [HideInInspector] public float ViewAngle; // gets set by enemy in start
    [HideInInspector] public float ViewDistance; // gets set by enemy in start
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
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, ViewDistance, _playerLayer);
        
        // if we detect the player in range...
        if(objectsInRange.Length > 0)
        {
            Transform target = objectsInRange[0].transform;

            // accounting for collider and sprite offset when using player position and converting to worldspace
            CapsuleCollider2D col = target.GetComponent<CapsuleCollider2D>();
            Vector2 colliderCenter = (Vector2)target.TransformPoint(col.offset);

            // get direction to player
            Vector2 dirToPlayer = colliderCenter - (Vector2) transform.position;

            // debugging
            Debug.DrawRay(transform.position, dirToPlayer, Color.white);

            // convert to angle
            float angleToPlayer = Vector2.Angle(ViewDirection, dirToPlayer);

            // if player position is in fov
            if(angleToPlayer < (ViewAngle / 2f))
            {
                float distanceToPlayer = dirToPlayer.magnitude;

                // collider y size to world space
                float colliderHeight = col.size.y * target.lossyScale.y;

                Vector2 dirToHead = dirToPlayer + new Vector2(0, colliderHeight/2f);
                Vector2 dirToFeet = dirToPlayer - new Vector2(0, colliderHeight/2f);

                // debugging
                Debug.DrawRay(transform.position, dirToPlayer, Color.blue);
                Debug.DrawRay(transform.position, dirToHead, Color.yellow);
                Debug.DrawRay(transform.position, dirToFeet, Color.green);

                // if nothing between guard and player head or player feet...
                if (!Physics2D.Raycast(transform.position, dirToHead, distanceToPlayer, _blockVisionLayer) ||
                    !Physics2D.Raycast(transform.position, dirToFeet, distanceToPlayer, _blockVisionLayer))
                {
                    // tell enemy that player is being detected
                    OnPlayerInVisionCone?.Invoke(distanceToPlayer);
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
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, DirFromAngle(angle), ViewDistance, _blockVisionLayer);

            Vector3 vertex;

            // if no hit...
            if(hitInfo.collider == null)
            {
                vertex = DirFromAngle(angle) * ViewDistance;
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

        return angle;
    }
}
