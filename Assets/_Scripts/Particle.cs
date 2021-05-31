using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Particle : MonoBehaviour
{
    public float r;
    public float g;
    public float m;
    public float lastfx, lastfz, lastfy;
    public Vector3 f;
    public Vector3 a;
    public Vector3 iniPos;
    public Vector3 prevPos;
    public Vector3 currPos;
    public Vector3 airForce;
    public float restitution;
    public Vector3 color;
    public Vector3 vel;
    public bool rain;

    public Blade fanBlade_1;
    public Blade fanBlade_2;
    public Blade fanBlade_3;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(fanBlade_1.A);
    }
 
 
    void CheckFloor()
    {
        
        if (currPos.y < r)
        {
            //Debug.Log("Floor");
           
            currPos = iniPos;
            prevPos = iniPos;
            
        }
    }

    void CheckFanBlades(Blade blade)
    {
        Vector3 u = blade.B - blade.A;
        Vector3 v = blade.D - blade.A;
        Vector3 w = blade.C - blade.A;

        Vector3 x = transform.position;

        Vector3 a = Vector3.Cross(u, x);
        Vector3 b = Vector3.Cross(v, x);
        Vector3 c = Vector3.Cross(w, x);

        Vector3 ab = blade.A + new Vector3(a.x * u.x, a.y * u.y, a.z * u.z);
        Vector3 ad = blade.A + new Vector3(b.x * v.x, b.y * v.y, b.z * v.z);
        Vector3 ac = blade.A + new Vector3(c.x * w.x, c.y * w.y, c.z * w.z);

        if((blade.A.x <= ab.x && blade.A.y <= ab.y && blade.A.z <= ab.z) && (blade.B.x >= ab.x && blade.B.y >= ab.y && blade.B.z >= ab.z))
        {
            if((blade.A.x <= ad.x && blade.A.y <= ad.y && blade.A.z <= ad.z) && (blade.D.x >= ad.x && blade.D.y >= ad.y && blade.D.z >= ad.z))
            {
                if ((blade.A.x <= ac.x && blade.A.y <= ac.y && blade.A.z <= ac.z) && (blade.C.x >= ac.x && blade.C.y >= ac.y && blade.C.z >= ac.z))
                {
                    Debug.Log("bouncing");
                    prevPos.z = currPos.z;
                    currPos.z = r;
                    f.z = -f.z * restitution;
                    a = f / m;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        CheckFanBlades(fanBlade_1);
        CheckFanBlades(fanBlade_2);
        CheckFanBlades(fanBlade_3);

        //Debug.Log(currPos);
        if (rain){
            if(Mathf.Abs(currPos.y - prevPos.y) < 0.00001f && Mathf.Abs(currPos.y - r) < 0.00001f)
            {
                currPos.y = r;
                prevPos.y = r;
            
            }
            else
            {
                
                f.y = -m * g * 0.2f;
                f.z = m * g *  0.3f;
                
            
                if(currPos.y != prevPos.y)
                {
                    vel = (currPos - prevPos) / Time.deltaTime;
                    
                    f.y = f.y + r * 0.001f * vel.magnitude;
            
                }
            }

            a = f / m;
            Vector3 temp = currPos;
            float dt = Time.deltaTime;
            currPos = 2 * currPos - prevPos + a * dt * dt;
            prevPos = temp;
            CheckFloor();
            transform.localPosition = currPos;
        }
        
        

        if (Input.GetKeyDown("r"))
        {
            rain = !rain;
        }
    }


   

}