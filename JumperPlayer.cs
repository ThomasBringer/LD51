using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPlayer : MonoBehaviour
{
    Rigidbody2D rb;    
    [SerializeField] float jumpLength;
    [SerializeField] float speed;
    float jumpSpeed;
    float jumpTime;

    bool grounded = true;

    [SerializeField] Transform graphic;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpSpeed = -Physics2D.gravity.y * jumpLength *.5f / speed;
        jumpTime = -2 * jumpSpeed / Physics2D.gravity.y;
    }

    void Update()
    {
        if ((Time.timeScale > 0 && grounded &&
        (Input.GetButton("Up") || Input.GetButton("Down") ||
        Input.GetButton("Right") || Input.GetButton("Left"))))
        {
            Jump();
        }
    }

    void Jump(){
        grounded = false;
        rb.velocity = Vector3.up * jumpSpeed;
        GameManager.gm.jump.Play();
        StartCoroutine(RotateRepeat());
    }

    IEnumerator RotateRepeat(){
        while(rb.velocity.y < 0){
            Rotate();
            yield return null;
        }
        while(rb.velocity.y >= 0){
            Rotate();
            yield return null;
        }
        while(rb.velocity.y < 0){
            Rotate();
            yield return null;
        }
        graphic.rotation = Quaternion.AngleAxis(Mathf.Round(graphic.rotation.eulerAngles.z / 90) * 90, Vector3.forward);
        grounded = true;
        GameManager.gm.tap.Play();
    }

    void Rotate(){
        graphic.Rotate(Vector3.forward, -90*Time.deltaTime/jumpTime);
    }
} 