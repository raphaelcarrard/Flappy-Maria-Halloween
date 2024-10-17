using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{

    private GameObject[] background;
    private float lastBackground;

    void Start()
    {
        background = GameObject.FindGameObjectsWithTag("Background");
        lastBackground = background[0].transform.position.x;
        for(int i = 1; i < background.Length; i++){
            if(lastBackground < background[i].transform.position.x){
                lastBackground = background[i].transform.position.x;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Background"){
            Vector3 temp = collision.transform.position;
            float width = ((BoxCollider2D)collision).size.x;
            temp.x = lastBackground + width;
            collision.transform.position = temp;
            lastBackground = temp.x;
        }
    }
}
