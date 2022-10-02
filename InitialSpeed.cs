using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSpeed : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 1;
    [SerializeField] float angleRange = 45;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Start(){
        rb.velocity = Quaternion.AngleAxis(Random.Range(-angleRange, angleRange), Vector3.forward) * Vector3.down * speed;
    }
}