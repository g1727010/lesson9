using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class NewBehaviourScript1 : MonoBehaviour
{
    private const int FRAME_MAX = 10;
    private List<Vector3> Point0 = new List<Vector3>();
    private List<Vector3> Point1 = new List<Vector3>();

    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Update()
    {
        if (FRAME_MAX <= Point0.Count)
        {
            Point0.RemoveAt(0);
            Point1.RemoveAt(0);
        }

        Point0.Add(transform.position);
        Point1.Add(transform.TransformPoint(new Vector3(0.0f, 1.0f, 0.0f)));

        if (2 <= Point0.Count)
        {
            CreateMesh();
        }
    }

    private void CreateMesh()
    {
        mesh.Clear();

        int n = Point0.Count;
        Vector3[] vertexArray = new Vector3[2 * n];
        Vector2[] uvArray = new Vector2[2 * n];
        int[] inderArray = new int[6 * (n - 1)];

        int idV = 0, idI = 0;
        float dUv = 1.0f / ((float)n - 1.0f);

        for (int i = 0; i < n; i++)
        {
            vertexArray[idV + 0] = transform.InverseTransformPoint(Point0[i]);
            vertexArray[idV + 1] = transform.InverseTransformPoint(Point1[i]);

            uvArray[idV + 0].x =
            uvArray[idV + 1].x = 1.0f - dUv * (float)i;
            uvArray[idV + 0].y = 0.0f;
            uvArray[idV + 1].y = 1.0f;

            if (i == n - 1) break;
            inderArray[idI + 0] = idV;
            inderArray[idI + 1] = idV + 1;
            inderArray[idI + 2] = idV + 2;
            inderArray[idI + 3] = idV + 2;
            inderArray[idI + 4] = idV + 1;
            inderArray[idI + 5] = idV + 3;

            idV += 2;
            idI += 6;
        }

        mesh.vertices = vertexArray;
        mesh.uv = uvArray;
        mesh.triangles = inderArray;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}


