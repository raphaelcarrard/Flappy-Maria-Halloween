using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDestruction : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Destruction"){
            Destroy(collision.gameObject);
        }
        if(collision.tag == "RingGroup"){
            Destroy(collision.gameObject);
        }
    }
}
