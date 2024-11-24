using System.Collections.Generic;
using UnityEngine;

public class WheelOfFortuneGenerator : MonoBehaviour
{
    [SerializeField] private protected List<WheelSegment> _segments = new List<WheelSegment>();

    /// <summary>
    /// Draws the wheel based on the defined segments.
    /// </summary>
    public void DrawRawWheelOfFortune()
    {
        //Add up all present values for calculating percentages
        float total = 0;
        foreach (var segment in _segments)
        {
            total += segment.Value;
        }

        float startSegmentAngle = 0f; // Initial angle for the start of the first segment.
        foreach (var segment in _segments)
        {
            // Calculate the percentage and angle of the segment relative to the total.
            float segmentPercentage = segment.Value / total;
            float deltaSegmentAngle = segmentPercentage * 360f;

            // Set the angles for the segment.
            segment.Angles.DeltaAngle = deltaSegmentAngle;
            segment.Angles.StartAngle = startSegmentAngle;

            DrawSegment(deltaSegmentAngle, startSegmentAngle, segment.Color, segment.Label);
            startSegmentAngle += deltaSegmentAngle; // Update the starting angle for the next segment.
        }
    }

    /// <summary>
    /// Draws a segment of the wheel of fortune.
    /// </summary>
    /// <param name="deltaSegmentAngle">The angle span of the segment.</param>
    /// <param name="startSegmentAngle">The starting angle of the segment.</param>
    /// <param name="color">The color of the segment.</param>
    /// <param name="name">The name of the segment.</param>
    private void DrawSegment(float deltaSegmentAngle, float startSegmentAngle, Color color, string name)
    {
        // Create a new GameObject for the segment with its necessary components.
        GameObject segment = new GameObject(name, typeof(MeshFilter), typeof(MeshRenderer));
        segment.transform.parent = transform;
        segment.transform.localRotation = Quaternion.Euler(0, 0, startSegmentAngle);
        segment.transform.localPosition = Vector3.zero;
        segment.gameObject.name = name;

        // Set the scale of the segment
        segment.transform.localScale = new Vector3(4f, 4f, 4f);

        // Configure the mesh for the segment.
        Mesh mesh = new Mesh();
        segment.GetComponent<MeshFilter>().mesh = mesh;
        segment.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Color"));
        segment.GetComponent<MeshRenderer>().material.color = color;

        // Define the resolution and radius of the pie chart.
        int segmentsCount = 50;
        float radius = 1f;

        // Define vertices and triangles for the mesh.
        Vector3[] vertices = new Vector3[segmentsCount + 2];
        int[] triangles = new int[segmentsCount * 3];

        vertices[0] = Vector3.zero; // Center of the wheel.
        float currentAngle = 0f;

        // Generate vertices and triangles for the mesh.
        for (int i = 0; i < segmentsCount + 1; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * radius;

            vertices[i + 1] = new Vector3(x, y, 0);

            // Define triangles to form the segment.
            if (i < segmentsCount)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }

            currentAngle += deltaSegmentAngle / segmentsCount; // Increment angle for the next vertex.
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}