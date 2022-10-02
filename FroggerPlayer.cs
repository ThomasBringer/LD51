using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer rdrr;

    // [SerializeField] float clampDown = -5.5f;
    [SerializeField] float clampHorizontal = 8;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        rdrr = GetComponent<SpriteRenderer>();
        input = Vector2.zero;
    }

    Vector2 input;
    Vector2 newInput;

    [SerializeField] Sprite up;
    [SerializeField] Sprite down;
    [SerializeField] Sprite right;

    void Update()
    {
        if(Time.timeScale == 0) return;

        newInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (newInput.y > .75f && !(input.y > .75f))
        {
            rdrr.sprite = up;
            transform.localScale = new Vector2(1, 1);
            Translate(Vector2.up);
        }
        else if ((newInput.y < -.75f && !(input.y < -.75f)))
        {
            rdrr.sprite = down;
            transform.localScale = new Vector2(1, 1);
            Translate(Vector2.down);
        }
        else if ((newInput.x > .75f && !(input.x > .75f)))
        {
            rdrr.sprite = right;
            transform.localScale = new Vector2(1, 1);
            Translate(Vector2.right);
        }
        else if ((newInput.x < -.75f && !(input.x < -.75f)))
        {
            rdrr.sprite = right;
            transform.localScale = new Vector2(-1, 1);
            Translate(Vector2.left);
        };
        input = newInput;
    }

    void Translate(Vector2 offset){
        GameManager.gm.tap.Play();
        Vector2 target = (Vector2) transform.position + offset;
        transform.position = new Vector2(Mathf.Clamp(target.x, -clampHorizontal, +clampHorizontal), target.y);
                                        // Mathf.Clamp(target.y, clampDown, Mathf.Infinity));
    }
}