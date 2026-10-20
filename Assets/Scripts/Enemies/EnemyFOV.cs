using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class EnemyFOV : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 180f)]
    private float viewAngle = 90f;

    [SerializeField]
    [Range(4, 128)]
    private int resolution = 60;

    [SerializeField]
    private float viewRadius = 8f;
    [SerializeField]
    private Vector3 offsetFromGround;
    [SerializeField]
    private Material fovMaterial;
    [SerializeField]
    private LayerMask obstacleMask;

    private Mesh mesh;
    private MeshFilter meshFilter;

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        mesh = new Mesh();
        meshFilter.mesh = mesh;

        GetComponent<MeshRenderer>().material = fovMaterial;
    }

    void LateUpdate()
    {
        GenerateMesh();
    }

    // Create enemy sight
    void GenerateMesh()
    {
        float halfAngle = viewAngle * 0.5f;

        Vector3[] vertices = new Vector3[resolution + 2];
        int[] triangles = new int[resolution * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i <= resolution; i++)
        {
            float angle = (viewAngle * i / resolution) - halfAngle;

            float radians = angle * Mathf.Deg2Rad;

            Vector3 direction = new Vector3(Mathf.Sin(radians), 0f, Mathf.Cos(radians));

            float distance = viewRadius;

            // Stop the ray at the first wall
            if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out RaycastHit hit, viewRadius, obstacleMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.tag == "Player")
                {
                    // Game over
                    GameManager.instance.GameOver();
                }

                distance = hit.distance;
            }

            vertices[i + 1] = direction * distance;
        }

        for (int i = 0; i < resolution; i++)
        {
            int t = i * 3;

            triangles[t] = 0;
            triangles[t + 1] = i + 1;
            triangles[t + 2] = i + 2;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        transform.localPosition = new Vector3(offsetFromGround.x, offsetFromGround.y, offsetFromGround.z);
    }
}
