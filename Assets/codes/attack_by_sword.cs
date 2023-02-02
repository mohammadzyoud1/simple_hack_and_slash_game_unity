using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class attack_by_sword : MonoBehaviour
{
    ParticleSystem [] particle;

    public player_movie player;
    GameObject sword_sound_attack;
    private void Start()
    {
        particle = GetComponentsInChildren<ParticleSystem>();
        sword_sound_attack = GameObject.FindGameObjectWithTag("sword_attack_sound");
        player = FindObjectOfType<player_movie>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
          
       
            player.animato.SetTrigger("attack");
        }
        else
        {
    

            player.animato.ResetTrigger("attack");

        }
        
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            
            if (other.GetComponentInParent<enemy_attck>().cheack_ability_to_damage()) {
                foreach(var g in particle)
                {
                    if (g.CompareTag("player_sword_effect"))
                    {
                        g.Play();
                    }
                }
                GameObject the_sound = Instantiate(sword_sound_attack, transform.position, Quaternion.identity);
                the_sound.GetComponent<AudioSource>().Play();
                other.gameObject.GetComponentInParent<enemy_attck>().health_reduce(20);
                other.gameObject.GetComponentInParent<enemy_attck>().make_able_to_damage_false();
                Destroy(the_sound, 1);
            }
            
           
        }
    }
   
    

}
