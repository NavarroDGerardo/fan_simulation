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
    public bool bounce = true;
    public int count = 0;

    public Post post;

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
           
            

            if(count == 0){
                prevPos.y = currPos.y;
                currPos.y = r;
                f.y = -f.y * 0.2f;
                f.z = f.z * 0.1f;
                a = f / m;
                count++;
            }else{
                currPos = iniPos;
                prevPos = iniPos;
                count--;
                bounce = true;
            }
            
            
            
            
        }
        
    }

    void CheckBox(Blade blade)
    {
        Vector3 d = transform.position - blade.center;
        if (Mathf.Abs(Vector3.Dot(d, blade.directions[0])) <= blade.half.x &&
            Mathf.Abs(Vector3.Dot(d, blade.directions[1])) <= blade.half.y &&
            Mathf.Abs(Vector3.Dot(d, blade.directions[2])) <= blade.half.z)
        {
            //rend.material.SetColor("_Color", Color.red);
            prevPos.z = currPos.z;
            currPos.z = r;
            f.z = -f.z * 1.3f;
            bounce = false;
            a = f / m;
        }
    }

    void ChechBoxPost(Post p)
    {
        Vector3 d = transform.position - p.center;
        if (Mathf.Abs(Vector3.Dot(d, p.directions[0])) <= p.half.x &&
            Mathf.Abs(Vector3.Dot(d, p.directions[1])) <= p.half.y &&
            Mathf.Abs(Vector3.Dot(d, p.directions[2])) <= p.half.z)
        {
            //rend.material.SetColor("_Color", Color.red);
            prevPos.z = currPos.z;
            currPos.z = r;
            f.z = -f.z * restitution * 5f;
            bounce = false;
            a = f / m;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckBox(fanBlade_1);
        CheckBox(fanBlade_2);
        CheckBox(fanBlade_3);
        ChechBoxPost(post);

        if (rain){
            if(Mathf.Abs(currPos.y - prevPos.y) < 0.00001f && Mathf.Abs(currPos.y - r) < 0.00001f)
            {
                currPos.y = r;
                prevPos.y = r;
            
            }
            else
            {
                if(bounce){
                    f.z = m * g *  0.3f;
                }
                f.y = -m * g * 0.2f;
                
                
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