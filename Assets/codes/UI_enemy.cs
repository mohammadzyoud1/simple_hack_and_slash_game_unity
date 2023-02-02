using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_enemy : MonoBehaviour
{
    Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera=Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.transform);
    }
}
