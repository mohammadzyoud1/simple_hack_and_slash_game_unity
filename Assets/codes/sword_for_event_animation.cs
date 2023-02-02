using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_for_event_animation : MonoBehaviour
{
    
    player_movie player;
    // Start is called before the first frame update
    void Start()
    {
      
        player=FindObjectOfType<player_movie>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void attack_by_sword()
    {
        player.attack_by_sword();

    }
}
