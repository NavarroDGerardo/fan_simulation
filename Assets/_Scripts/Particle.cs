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
    public bool rain = false;
    public Renderer rend;
    public Blade fanBlade_1;
    public Blade fanBlade_2;
    public Blade fanBlade_3;

    // Start is called before the first frame update
    void Start()
    {
        rend = this.GetComponent<Renderer>();
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

    void CheckBox(Blade blade)
    {
        Vector3 d = transform.position - blade.center;
        if (Mathf.Abs(Vector3.Dot(d, blade.directions[0])) <= blade.half.x &&
            Mathf.Abs(Vector3.Dot(d, blade.directions[1])) <= blade.half.y &&
            Mathf.Abs(Vector3.Dot(d, blade.directions[2])) <= blade.half.z)
        {
            Debug.Log("Bounce");
            rend.material.SetColor("_Color", Color.red);
        }
    }

    void CheckFanBlades(Blade blade)
    {
        Vector3 u = blade.boxC[1] - blade.boxC[0];
        Vector3 v = blade.boxC[3] - blade.boxC[0];
        Vector3 w = blade.boxC[2] - blade.boxC[0];

        Vector3 x = transform.position;

        Vector3 a = Vector3.Cross(u, x);
        Vector3 b = Vector3.Cross(v, x);
        Vector3 c = Vector3.Cross(w, x);

        Vector3 ab = blade.boxC[0] + new Vector3(a.x * u.x, a.y * u.y, a.z * u.z);
        Vector3 ad = blade.boxC[0] + new Vector3(b.x * v.x, b.y * v.y, b.z * v.z);
        Vector3 ac = blade.boxC[0] + new Vector3(c.x * w.x, c.y * w.y, c.z * w.z);

        if ((blade.boxC[0].x <= ab.x && blade.boxC[0].y <= ab.y && blade.boxC[0].z <= ab.z) && (blade.boxC[1].x >= ab.x && blade.boxC[1].y >= ab.y && blade.boxC[1].z >= ab.z))
        {
            if((blade.boxC[0].x <= ad.x && blade.boxC[0].y <= ad.y && blade.boxC[0].z <= ad.z) && (blade.boxC[3].x >= ad.x && blade.boxC[3].y >= ad.y && blade.boxC[3].z >= ad.z))
            {
                if ((blade.boxC[0].x <= ac.x && blade.boxC[0].y <= ac.y && blade.boxC[0].z <= ac.z) && (blade.boxC[2].x >= ac.x && blade.boxC[2].y >= ac.y && blade.boxC[2].z >= ac.z))
                {
                    Debug.Log("Bounce");
                    rend.material.SetColor("_Color", Color.red);
                    /*prevPos.z = currPos.z;
                    currPos.z = r;
                    f.z = -f.z * restitution;
                    a = f / m;*/
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        CheckBox(fanBlade_1);
        CheckBox(fanBlade_2);
        CheckBox(fanBlade_3);

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