using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] GameObject twoPipesPrefab;
    [SerializeField] float distanceBetweenPipes = 6;
    [SerializeField] float randomHeightRange = 5;
    [SerializeField] float speed = 1;
    float timeBetweenPipes;
    float pipeLifeTime;
    Vector3 spawnerPos;
    [SerializeField] int initPipeNum = 2;

    void Awake(){
        spawnerPos = transform.position;
        timeBetweenPipes = distanceBetweenPipes/speed;
        pipeLifeTime = 15; //2*spawnerPos.x/speed;
    }

    void Start(){
        InitSpawn();
        StartCoroutine(SpawnRepeat());
    }

    void InitSpawn(){
        Vector3 offset = Vector3.zero;
        for (int i = 0; i < initPipeNum; i++)
        {
            offset += distanceBetweenPipes*Vector3.left;
            Spawn(offset);
        }
    }

    IEnumerator SpawnRepeat(){
        while(true){
            Spawn();
            yield return new WaitForSeconds(timeBetweenPipes);
        }
    }

    void Spawn(){
        Spawn(Vector3.zero);
    }

    void Spawn(Vector3 offset){
        GameObject instance = Instantiate(twoPipesPrefab,
                spawnerPos + offset + Random.Range(-randomHeightRange,+randomHeightRange)*Vector3.up,
                Quaternion.identity, transform);
        instance.GetComponent<ConstantSpeed>().speed = speed;
        Destroy(instance,pipeLifeTime);
    }
}