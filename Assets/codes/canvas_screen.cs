using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvas_screen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void change_health_bar(float current_health,float max_health)
    {
        transform.GetChild(1).GetComponent<Image>().fillAmount = current_health / max_health;

    }
}
