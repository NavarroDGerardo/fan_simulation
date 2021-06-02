using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float angleZ;
    float dz = 0.4f;
    Vector3[] originalPoints;
    MeshFilter mf;
    bool canRotate = false;

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
        Matrix4x4 r = Transformations.RotateM(angleZ, Transformations.AXIS.AX_Z);
        Matrix4x4 t = Transformations.TranslateM(0, 3.45f, 0);
        Matrix4x4 tLeft = Transformations.TranslateM(0.65f, 0, -0.2f);

        for (int i = 0; i < box.Length; i++)
        {
            Vector4 temp = box[i];
            temp.w = 1;
            outputBox[i] = t * r * tLeft * temp;
        }

        return outputBox;
    }

    Vector3[] TransformBlade(Vector3[] input)
    {
        Vector3[] output = new Vector3[input.Length];
        Matrix4x4 r = Transformations.RotateM(angleZ, Transformations.AXIS.AX_Z);
        Matrix4x4 t = Transformations.TranslateM(0, 3.45f, 0);
        Matrix4x4 tLeft = Transformations.TranslateM(0.65f, 0, -0.2f); 

        for(int i = 0; i < input.Length; i++)
        {
            Vector4 temp = input[i];
            temp.w = 1;
            output[i] =  t * r * tLeft * temp;
        }

        return output;
    }

    void Start()
    {
        A = new Vector3(-0.627f, -0.07f, -0.02f);
        B = new Vector3(0.7f, -0.07f, -0.02f);
        C = new Vector3(0.7f, 0.07f, -0.02f);
        D = new Vector3(-0.627f, 0.07f, -0.02f);
        
        originalBox = new Vector3[]{ A, B, C, D};
        boxC = originalBox;

        float xMin = Mathf.Abs(Mathf.Min(boxC[0].x, boxC[1].x));
        float xMax = Mathf.Max(boxC[0].x, boxC[1].x);
        float yMin = Mathf.Abs(Mathf.Min(boxC[0].y, boxC[2].y));
        float yMax = Mathf.Max(boxC[0].y, boxC[2].y);

        half = new Vector3((xMax + xMin) / 2, (yMax + yMin) / 2, 0f);
        
        center = new Vector3(
            (boxC[0].x + boxC[2].x) / 2,
            (boxC[3].y + boxC[1].y) / 2,
            boxC[0].z
            );

        directions = new Vector3[] {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1)
        };

        mf = gameObject.GetComponent<MeshFilter>();
        originalPoints = mf.mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        center = new Vector3(
            (boxC[0].x + boxC[2].x) / 2,
            (boxC[3].y + boxC[1].y) / 2,
            boxC[0].z
            );

        cDot.transform.position = center;

        Debug.DrawLine(boxC[0], boxC[1], Color.red);
        Debug.DrawLine(boxC[1], boxC[2], Color.red);
        Debug.DrawLine(boxC[3], boxC[2], Color.red);
        Debug.DrawLine(boxC[0], boxC[3], Color.red);

        if (Input.GetKeyDown("space"))
        {
            canRotate = !canRotate;
        }

        if (canRotate)
        {
            angleZ += dz;
            mf.mesh.vertices = TransformBlade(originalPoints);
            boxC = TransformCollider(originalBox);
        }
    }
}
