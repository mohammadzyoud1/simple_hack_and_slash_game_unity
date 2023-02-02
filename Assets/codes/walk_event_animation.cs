using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk_event_animation : MonoBehaviour
{
    player_movie player;
    GameObject sound_walk;
    // Start is called before the first frame update
    void Start()
    {
        player=FindObjectOfType<player_movie>();    
        sound_walk = GameObject.FindGameObjectWithTag("sound_walk");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void sound_walking()
    { 
        /*if (player.checked_if_grounded())
        {
            GameObject sound = Instantiate(sound_walk, transform.position, Quaternion.identity);
            sound.GetComponent<AudioSource>().Play();
            Destroy(sound, 0.5f);
        }*/
        
        

    }
}
