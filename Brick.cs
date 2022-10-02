using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    int hp = 3;

    SpriteRenderer rdrr;

    [SerializeField] Sprite[] sprites;

    void Awake(){
        rdrr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(){
        hp--;
        if(hp <= 0){
            GameManager.gm.hit.Play();
            Destroy(gameObject);
        }
        else
            GameManager.gm.tap.Play();
        rdrr.sprite = sprites[Mathf.Clamp(2 - hp,0,1)];
    }
}