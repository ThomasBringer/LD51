using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;

    void OnTriggerEnter2D(Collider2D player){
        Instantiate(explosionPrefab, player.transform.position + Vector3.forward, Quaternion.identity);
        GameManager.gm.explode.Play();
        Destroy(player.gameObject);
        GameManager.gm.Playing = false;
    }
}