using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post : MonoBehaviour
{
    Vector3[] originalPoints;
    MeshFilter mf;

    //checkCollision
    public Vector3[] originalBox;
    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public Vector3 D;
    public Vector3[] boxC;

    public Vector3 center;
    public Vector3 half;
    public Vector3[] directions;

    public GameObject cDot;

    Vector3[] TransformCollider(Vector3[] box)
    {
        Vector3[] outputBox = new Vector3[box.Length];
        Matrix4x4 r = Transformations.RotateM(90, Transformations.AXIS.AX_Y);

        for (int i = 0; i < box.Length; i++)
        {
            Vector4 temp = box[i];
            temp.w = 1;
            outputBox[i] = r * temp;
        }

        return outputBox;
    }

    Vector3[] TransformPost(Vector3[] input)
    {
        Vector3[] output = new Vector3[input.Length];
        Matrix4x4 r = Transformations.RotateM(90, Transformations.AXIS.AX_Y);

        for (int i = 0; i < input.Length; i++)
        {
            Vector4 temp = input[i];
            temp.w = 1;
            output[i] = r * temp;
        }

        return output;
    }

    void Start()
    {
        mf = gameObject.GetComponent<MeshFilter>();
        originalPoints = mf.mesh.vertices;
        mf.mesh.vertices = TransformPost(originalPoints);

        A = new Vector3(0, 0, -0.07f);
        B = new Vector3(0, 0, 0.07f);
        C = new Vector3(0, 3.35f, 0.04f);
        D = new Vector3(0, 3.35f, -0.04f);

        originalBox = new Vector3[] { A, B, C, D };
        boxC = TransformCollider(originalBox);

        float xMin = Mathf.Abs(Mathf.Min(boxC[0].x, boxC[1].x));
        float xMax = Mathf.Max(boxC[0].x, boxC[1].x);
        float yMin = Mathf.Abs(Mathf.Min(boxC[0].y, boxC[2].y));
        float yMax = Mathf.Max(boxC[0].y, boxC[2].y);

        half = new Vector3((xMax + xMin) / 2, (yMax + yMin) / 2, 0.11f);

        center = new Vector3(
            (boxC[0].x + boxC[2].x) / 2,
            (boxC[3].y + boxC[1].y) / 2,
            0.06f
            );

        cDot.transform.position = center;

        directions = new Vector3[] {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1)
        };
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(boxC[0], boxC[1], Color.red);
        Debug.DrawLine(boxC[1], boxC[2], Color.red);
        Debug.DrawLine(boxC[3], boxC[2], Color.red);
        Debug.DrawLine(boxC[0], boxC[3], Color.red);
    }
}
