using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield_code : MonoBehaviour
{
    ParticleSystem[] particle; 
     bool vaild=false;
    // Start is called before the first frame update
    void Start()
    {
      particle=GetComponentsInChildren<ParticleSystem>();    
    }

    // Update is called once per frame
    void Update()
    {
        shield_on();
        
    }
    public bool  shield_on()
    {
        if (Input.GetMouseButton(1))
        {
             vaild=true;
            foreach(var y in particle)
            {
                if (y.CompareTag("shield_effect"))
                {
                    if (!y.isPlaying)
                    {
                        y.Play();
                    }
                   
                }
            }

        }
        if (Input.GetMouseButtonUp(1))
        {
            foreach (var y in particle)
            {
                if (y.CompareTag("shield_effect"))
                {
                    if (y.isPlaying)
                    {
                        y.Stop();
                    }
                    
                }
            }

            vaild = false;
        }
        return vaild;

    }
}
