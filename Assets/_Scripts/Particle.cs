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
    public int rain = -1;

    // Start is called before the first frame update
    void Start()
    {
        //The particles will explode from the emitter at(0,0, 0), with random forces in ±X ±Y and ±Z
        
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


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currPos);
        if(rain == 1){
            if(Mathf.Abs(currPos.y - prevPos.y) < 0.00001f && Mathf.Abs(currPos.y - r) < 0.00001f)
            {
                currPos.y = r;
                prevPos.y = r;
            
            }
            else
            {
                
                f.y = -m * g * 0.2f;
                f.x = -m * g *  0.3f;
                
            
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
            rain = -rain;
        }
    }


   

}