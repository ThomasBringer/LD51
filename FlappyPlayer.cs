using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] float jumpHeight;
    float jumpSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpSpeed = Mathf.Sqrt(-2*Physics2D.gravity.y*jumpHeight);
    }

    void Update()
    {
        if (Time.timeScale > 0 && (Input.GetButtonDown("Up") || Input.GetButtonDown("Down") ||
        Input.GetButtonDown("Right") || Input.GetButtonDown("Left")))
        {
            rb.velocity = Vector3.up * jumpSpeed;
            anim.SetTrigger("Flap");
            GameManager.gm.jump.Play();
        }
    }
}
