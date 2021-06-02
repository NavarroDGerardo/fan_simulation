using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Emitter : MonoBehaviour
{
    public GameObject ParticlePrefab;
    public int numParticles;
    private GameObject[] particles;

    public GameObject fanBlade_1;
    public GameObject fanBlade_2;
    public GameObject fanBlade_3;

    // Start is called before the first frame update
    void Start()
    {
        particles = new GameObject[numParticles];

        for (int p = 0; p < numParticles; p++)
        {
            GameObject part = Instantiate(ParticlePrefab);
            //Each particle will have a random radius in the range[0.3, 0.9].
            float diam = Random.Range(0.05f, 0.06f); 
            part.transform.localScale = new Vector3(diam, diam, diam);
            //Each particle will have a random material color.
            //The red diffuse channel in the range[0.2, 0.7](thefull red color will be reservedfor collisions). The green and blue diffuse channelsin the range[0.2, 1.0].
            Color c = new Color(0, 0, Random.Range(0.2f, 1f));
            Renderer rend = part.GetComponent<Renderer>();
            rend.material.SetColor("_Color", c);
            particles[p] = part;

            // Get a reference to the Particle.cs script inside the prefab:
            Particle pScript = part.GetComponent<Particle>();
            pScript.color = new Vector3(c.r, c.g, c.b);
            pScript.r = diam / 2f;
            pScript.g = 9.81f;
            pScript.currPos = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(6f, 12f), Random.Range(-2f, -11f));
            pScript.iniPos = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(4f, 16f), Random.Range(-2f, -11f));
            pScript.prevPos = pScript.currPos;
            //Each particle’s mass will equal the radius times 2.
            pScript.m = pScript.r * 2;
            pScript.restitution = 0.5f;
            pScript.f = Vector3.zero;

            //pass the scripts of all the blades
            pScript.fanBlade_1 = fanBlade_1.GetComponent<Blade>();
            pScript.fanBlade_2 = fanBlade_2.GetComponent<Blade>();
            pScript.fanBlade_3 = fanBlade_3.GetComponent<Blade>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        //When two particles collide, they will change theirdiffuse color tored.
        
    }
}