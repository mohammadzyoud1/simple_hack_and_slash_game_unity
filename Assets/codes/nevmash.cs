using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class nevmash : MonoBehaviour
{
    enemy_attck enemy;
    private NavMeshAgent nav;

    private GameObject player;
    float speed;
    Animator anim;
   
   
     float tracking_distance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<enemy_attck>();
        anim =GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        anim.SetFloat("Blend", 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.is_dead) { 
           
        speed = nav.velocity.magnitude;
        tracking_distance = Vector3.Distance(player.transform.position, transform.position);
        if (tracking_distance < 10) {

                float speed = 15 * Time.deltaTime;
                Vector3 direction = Vector3.RotateTowards(transform.forward, player.transform.position-transform.position, speed, 0f);
                transform.rotation = Quaternion.LookRotation(direction);
   
             
            nav.SetDestination(player.transform.position);
        }
        anim.SetFloat("Blend", speed);
    
     
        }
       
    }
    public bool check_distance() { 
    if(tracking_distance <= nav.stoppingDistance + .4f)
        {
            return true;
        }
        else
        {

            return false;
        }
    }
}
