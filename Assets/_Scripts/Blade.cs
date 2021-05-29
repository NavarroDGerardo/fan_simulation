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
    Vector3[] boxC;

    Vector3[] TransformBlade(Vector3[] input, Vector3[] box)
    {
        Vector3[] output = new Vector3[input.Length];
        Vector3[] outputBox = new Vector3[box.Length];
        Matrix4x4 r = Transformations.RotateM(angleZ, Transformations.AXIS.AX_Z);
        Matrix4x4 t = Transformations.TranslateM(0, 3.45f, 0);
        Matrix4x4 tLeft = Transformations.TranslateM(0.65f, 0, -0.2f); 

        for(int i = 0; i < input.Length; i++)
        {
            Vector4 temp = input[i];
            temp.w = 1;
            output[i] =  t * r * tLeft * temp;
        }

        for(int i = 0; i < box.Length; i++)
        {
            Vector4 temp = box[i];
            temp.w = 1;
            outputBox[i] = t * r * tLeft * temp;
        }

        boxC = outputBox;
        return output;
    }

    void Start()
    {
        A = new Vector3(-0.627f, -0.07f, -0.02f);
        B = new Vector3(0.7f, -0.07f, -0.02f);
        C = new Vector3(0.7f, 0.07f, -0.02f);
        D = new Vector3(-0.627f, 0.07f, -0.02f);
        originalBox = new Vector3[]{ A, B, C, D};

        boxC = new Vector3[4];

        mf = gameObject.GetComponent<MeshFilter>();
        originalPoints = mf.mesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            canRotate = !canRotate;
        }

        if (canRotate)
        {
            angleZ += dz;
            mf.mesh.vertices = TransformBlade(originalPoints, originalBox);
        }
    }
}
