using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickActivator : MonoBehaviour
{
    [SerializeField] float brickProbability = .3f;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(Random.Range(0f, 1f) <= brickProbability);            
        }
    }
}