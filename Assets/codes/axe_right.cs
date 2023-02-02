using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe_right : MonoBehaviour
{
    GameObject axe_sound;
    BoxCollider box_collider;
    // Start is called before the first frame update
    void Start()
    {
        box_collider = GetComponent<BoxCollider>();
        axe_sound = GameObject.FindGameObjectWithTag("axe_sound");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)

    {

        if (other.CompareTag("Player"))
        {
            GameObject sound = Instantiate(axe_sound, transform.position, Quaternion.identity);
            sound.GetComponent<AudioSource>().Play();
            Destroy(sound, 1);
            other.gameObject.GetComponent<player_movie>().player_health_reduce(10);

        }

    }
    public void acitvate_collider()
    {
        box_collider.enabled = true;
        StartCoroutine(collideroff());
    }
    IEnumerator collideroff ()
    {
        yield return new WaitForSeconds(0.4f);
        box_collider.enabled=false;

    }
   
}
