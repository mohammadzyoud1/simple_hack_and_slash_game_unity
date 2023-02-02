using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR;

public class deal_damage : MonoBehaviour
{
    GameObject walk_sound;
    axe_right axe_R;
    axe_left axe_L;
    // Start is called before the first frame update
    void Start()
    {
        walk_sound = GameObject.Find("anime_walk_sound");

        axe_R = GetComponentInChildren<axe_right>();
        axe_L = GetComponentInChildren<axe_left>();
    }

    // Update is called once per frame
    void Update()
    {

    }
  
    private void attack_right_axe()
    {
        axe_R.acitvate_collider();

        
    }
    private void attack_left_axe()
    {
      axe_L.acitvate_collider();

    }
    private void attack_both_axe()
    {
        axe_L.acitvate_collider();
        axe_R.acitvate_collider();


    }
    public void walking()
    {
        GameObject sound = Instantiate(walk_sound, transform.position, Quaternion.identity);
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, .35f);

    }

}
