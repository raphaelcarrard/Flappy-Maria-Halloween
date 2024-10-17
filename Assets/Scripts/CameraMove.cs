using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public static float offSetX;

    void Update()
    {
        if(PlayerScript.instance != null){
            if(PlayerScript.instance.isAlive){
                MoveCamera();
            }
        }
    }

    void MoveCamera()
    {
        Vector3 temp = transform.position;
        temp.x = PlayerScript.instance.GetPositionX() + offSetX;
        transform.position = temp;
    }
}
