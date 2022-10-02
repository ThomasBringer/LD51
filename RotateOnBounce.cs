using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnBounce : MonoBehaviour
{
    [SerializeField] float rotateTime = 1;
    bool rotating = false;

    void OnCollisionEnter2D(){
        if(!rotating) StartCoroutine(RotateRepeat());
    }

    IEnumerator RotateRepeat(){
        rotating = true;
        float savedTime = Time.time;
        while(Time.time - savedTime <= rotateTime){
            transform.Rotate(Vector3.forward, -90*Time.deltaTime/rotateTime);
            yield return null;        
        }
        transform.rotation = Quaternion.AngleAxis(Mathf.Round(transform.rotation.eulerAngles.z / 90) * 90, Vector3.forward);
        rotating = false;
    }
}