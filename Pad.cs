using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
    Rigidbody2D rb;    
    [SerializeField] float speed = 1;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        rb.velocity = speed * Input.GetAxisRaw("Horizontal") * Vector2.right;
    }
}