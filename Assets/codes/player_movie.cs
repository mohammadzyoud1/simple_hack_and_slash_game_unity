using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class player_movie : MonoBehaviour
{
    GameObject dead_sound;
    win_lose win_Lose_script;
    int collected_coins;
    ParticleSystem [] particle;
    GameObject sound_money;
    shield_code shield_Code;
     float current_health = 100;
      float maxhealth = 100;
    GameObject sword;
   public Animator animato;
   public float jumpvalue = 3;
    float jumpvelocity = 0;
    private Quaternion x, z;
    float ground = 10;
     float speed = 3f;
    public float speedfalling= 2;
    float angle;
    float isrun = 0;
    Boolean isdead=false;
    public Transform cam;
    // Transform transfor;          
    CharacterController controlling;
   
    // Start is called before the first frame update
    void Start()
    {
        win_Lose_script = FindObjectOfType<win_lose>();
        dead_sound = GameObject.FindWithTag("player_dead");
        collected_coins = 0;
        sound_money = GameObject.FindGameObjectWithTag("coin_sound");
        GameObject.Find("Canvas").transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text="Gold= "+collected_coins;
        particle = GetComponentsInChildren<ParticleSystem>();
        shield_Code = FindObjectOfType<shield_code>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sword = GameObject.FindGameObjectWithTag("wapon sword");
        animato = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animato.SetFloat("speed", 0);
        controlling = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (win_Lose_script.check() == 0)
        {
            this.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = "You winner ,collected Gold " + collected_coins;
        }
        if (!isdead ) { 

        float Rl = Input.GetAxis("Horizontal");
        float UD = Input.GetAxis("Vertical");
        Vector3 mov = new Vector3(Rl, 0, UD).normalized;

        if (controlling.isGrounded)
        {
            if (Input.GetAxis("Jump") > 0)
            {
                jumpvelocity = jumpvalue;
                controlling.Move(new Vector3(0, jumpvelocity, 0) * Time.deltaTime);

            }
        }
        else
        {

                jumpvelocity -= ground * speedfalling * Time.deltaTime;
            controlling.Move(new Vector3(0, jumpvelocity, 0) * Time.deltaTime);

        }

        if (mov.magnitude >= 0.1f)
        {
            angle = Mathf.Atan2(Rl, UD) * Mathf.Rad2Deg + cam.eulerAngles.y;
        
            Vector3 y = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            Vector3 c = new Vector3(transform.position.x, angle, transform.position.z);
            x = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            z = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
            transform.rotation = Quaternion.Lerp(x, z, 0.1f / Time.deltaTime);

            mov = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isrun = 1;
                animato.SetFloat("speed", 0.66666f);
                speed = speed + 5;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isrun = 0;
                speed = speed - 5;
            }
            if (isrun == 0)
            {
                animato.SetFloat("speed", 0.33333333f);
            }
            else {; }
            controlling.Move(new Vector3(mov.x * speed, mov.y, mov.z * speed) * Time.deltaTime);

        }
        else
        {
            animato.SetFloat("speed", 0);
        }
    }
        if (current_health <= 0)
        {
           
            if (!isdead)
            {
                GameObject sound_die = Instantiate(dead_sound, transform.position, Quaternion.identity);
                if (!sound_die.GetComponent<AudioSource>().isPlaying)
                {
                    sound_die.GetComponent<AudioSource>().Play();
                    Destroy(sound_die, sound_die.GetComponent<AudioSource>().clip.length);
                }
            }
            isdead = true;
            animato.SetBool("died", true);
            
            
            foreach(var y in particle)
            {
                if (y.CompareTag("player_death_effect"))
                {
                    if (!y.isPlaying)
                    {
                        
                        y.Play();
                    }
                    
                }
            }
            GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = "You lost ,Game over";
        }

    }
    public void attack_by_sword()
    {
        sword.GetComponent<BoxCollider>().gameObject.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(repeat());
    }
    IEnumerator repeat()
    {
        yield return new WaitForSeconds(0.5f);
        sword.GetComponent<BoxCollider>().gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    public void player_health_reduce(int value)
    {
        if (!shield_Code.shield_on())
        {
            if (!isdead)
            {
                current_health -= value;
                GameObject.Find("Canvas").GetComponent<canvas_screen>().change_health_bar(current_health, maxhealth);

                animato.SetTrigger("get_hit");
                foreach (var y in particle)
                {
                    if (y.CompareTag("player_health_reduce"))
                    {
                        y.Play();

                    }
                }
                
            }
        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("coin"))
        {
            collected_coins += 1;
            GameObject.Find("Canvas").transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = "Gold= " + collected_coins;



            Destroy(other.gameObject);
            GameObject coin__sound = Instantiate(sound_money, transform.position, Quaternion.identity);
            coin__sound.GetComponent<AudioSource>().Play();
            Destroy(coin__sound, 1);
        }
        if (other.CompareTag("mid_kit"))
        {
            if (current_health != maxhealth)
            {
                current_health += 15;
                if (current_health > maxhealth)
                {
                    current_health = maxhealth;
                }
               GameObject.Find("Canvas").GetComponent<canvas_screen>().change_health_bar(current_health, maxhealth);
                Destroy(other.gameObject);
                foreach( var t in particle)
                {
                    if (t.CompareTag("health_increase_effect"))
                    {
                        if (!t.isPlaying)
                        {
                            t.Play();
                        }
                    }
                }

            }
        }
    }




}