using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class win_lose : MonoBehaviour
{
    GameObject[] enemy; 
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("enemy");

    }

    // Update is called once per frame
    void Update()
    {
        if (check() == 0)
        {
            
        }
    }
    public int check()
    {
        enemy = GameObject.FindGameObjectsWithTag("enemy");
        return enemy.Length;
    }
}
