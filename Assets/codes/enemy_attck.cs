using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class enemy_attck : MonoBehaviour
{
    win_lose win_lsoe_script;
    ParticleSystem [] particle;
     bool able_to_take_damage;
     float max_enemy_health = 100;
     float current_enemy_health = 100;
   float wait_cool_attack;
    nevmash nevmashscript;

   private Boolean is_attack_time;
   public bool is_dead;
    Animator anim;
    GameObject enemy_die_sound;
    

    // Start is called before the first frame update
    void Start()
    {
        win_lsoe_script = FindObjectOfType<win_lose>();
        enemy_die_sound = GameObject.FindGameObjectWithTag("enemy_dead");
        particle= GetComponentsInChildren<ParticleSystem>();
        wait_cool_attack = 2f;
        able_to_take_damage = true;
        is_attack_time =true;
        is_dead = false;
    
        anim=GetComponentInChildren<Animator>();    
   
        nevmashscript = GetComponent<nevmash>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_dead) 
        { 

   
        if(nevmashscript.check_distance())
            {
               
                if (is_attack_time)
                {
                   
                    anim.SetInteger("attacknum", Random.Range(0, 3));
              
                    anim.SetTrigger("isattack");
                    is_attack_time=false;
                    StartCoroutine(attcking_even());
                }
            }
       
        }
    
 
        if (current_enemy_health <= 0)
        {
            if (!is_dead)
            {
                win_lsoe_script.check();
                GameObject die_sound = Instantiate(enemy_die_sound, transform.position, Quaternion.identity);
                if (!die_sound.GetComponent<AudioSource>().isPlaying)
                {
                    die_sound.GetComponent<AudioSource>().Play();
                    Destroy(die_sound, die_sound.GetComponent<AudioSource>().clip.length);
                }
            }
            is_dead = true;
            anim.SetBool("isdied", true);
            foreach(var t in particle)
            {
                if (t.CompareTag("enemy_death_effect"))
                {
                    if (!t.isPlaying)
                    {
                        t.Play();
                    }
                }
            }
            
            
            StartCoroutine(die());
            


        }

    }
    IEnumerator attcking_even()
    {
    
        yield return new WaitForSeconds(wait_cool_attack);
        is_attack_time = true;
        
    }
    IEnumerator die()
    {
        
        
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
    IEnumerator return_to_take_damage()
    {
     
        yield return new WaitForSeconds(.7f);
        able_to_take_damage = true;
    }
    
    public  void make_able_to_damage_false()
    { 
        StartCoroutine(return_to_take_damage());
    }
    public bool cheack_ability_to_damage()
    {
        if (able_to_take_damage)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void  health_reduce(int much)
    {
        if (!is_dead)
        {
            
            current_enemy_health -= much;
           
            transform.Find("Canvas_enemy").GetChild(1).GetComponent<Image>().fillAmount =current_enemy_health/max_enemy_health;
           
            anim.SetTrigger("take_damage");
        }
    }
   
   
    
}

