using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpeedLoop : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float loopDist;
    [SerializeField] Vector3 dir;

    float x = 0;

    void Update(){
        float dx = speed * Time.deltaTime;
        x += dx;

        float translation = dx;
        if(x >= loopDist){
            translation -= loopDist;
            x=0;
            // transform.Translate(loopDist*(-dir))
        }
        transform.Translate(translation * dir);
    }
}
