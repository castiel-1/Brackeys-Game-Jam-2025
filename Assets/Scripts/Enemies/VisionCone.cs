using Unity.VisualScripting;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    [SerializeField] private int _rayCount = 10; // how many rays we draw all together
    [SerializeField] private float viewRadius = 40;

    [HideInInspector] public float ViewAngle; // gets set by enemy in start

    private Mesh mesh;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void DrawVisionCone(Vector2 viewDirection)
    {
        // debugging
        Debug.Log("called draw vision cone");

        float angleStepSize = ViewAngle / (_rayCount - 1); // -1 so that we get exactly rayCount rays
        float angle = AngleFromDir(viewDirection) + (ViewAngle / 2f); // start angle is where we are looking + half of fov

        // set up stuff for mesh
        Vector3[] vertices = new Vector3[_rayCount + 1];
        int[] triangles = new int[(_rayCount - 1) * 3];

        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i < _rayCount; i++)
        {
            Vector3 vertex = DirFromAngle(angle) * viewRadius;

            // debugging
            Debug.Log("vertexindex: " + vertexIndex);
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

        //debugging
        
        if(mesh == null) Debug.Log("mesh is null");
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    private Vector3 DirFromAngle(float angleInDegrees)
    {
        Vector3 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angleInDegrees), Mathf.Sin(Mathf.Deg2Rad * angleInDegrees));

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
