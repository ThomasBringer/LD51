using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{   
    [SerializeField] GameObject spikePrefab;

    [SerializeField] int[] spikeNums;
    [SerializeField] float[] spikeNumsIncidence;

    [SerializeField] int[] tilesNumBetweenSpikes;
    [SerializeField] float[] tilesNumBetweenSpikesIncidence;

    [SerializeField] float speed = 6;
    float timeFor1Tile;
    float spikeLifeTime;
    Vector2 spawnerPos;
    [SerializeField] float initSpikeDist = 24;

    void Awake(){
        spawnerPos = transform.position;
        timeFor1Tile = 1/speed;
        spikeLifeTime = 15; //2*spawnerPos.x * timeFor1Tile;
    }

    void Start(){
        InitSpawn();
        StartCoroutine(SpawnRepeat());
    }

    void InitSpawn(){
        Vector2 offset = Vector2.zero;
        float dist = 0;

        while(true){
            int tilesNum = tilesNumBetweenSpikes.RandomUnnormalized(tilesNumBetweenSpikesIncidence);
            // int tilesToSkip = tilesNum + spikesNum - 1;
            dist += tilesNum;
            if(dist > initSpikeDist) return;

            offset += tilesNum * Vector2.left;
            
            int spikesNum = spikeNums.RandomUnnormalized(spikeNumsIncidence);
            dist += spikesNum;

            if(dist > initSpikeDist) return;            

            for (int i = 0; i < spikesNum; i++)
            {
                Spawn(offset);
                offset += Vector2.left;
            }

        }

    }

    IEnumerator SpawnRepeat(){
        while(true){
            int spikesNum = spikeNums.RandomUnnormalized(spikeNumsIncidence);
            Vector2 offset = Vector2.zero;
            for (int i = 0; i < spikesNum; i++)
            {
                Spawn(offset);
                offset += Vector2.right;
            }

            int tilesNum = tilesNumBetweenSpikes.RandomUnnormalized(tilesNumBetweenSpikesIncidence);
            int tilesToSkip = tilesNum + spikesNum - 1;
            yield return new WaitForSeconds(tilesToSkip * timeFor1Tile);
        }
    }

    void Spawn(){
        Spawn(Vector2.zero);
    }

    void Spawn(Vector2 offset){
        GameObject instance = Instantiate(spikePrefab,
                spawnerPos + offset,
                Quaternion.identity, transform);
        instance.GetComponent<ConstantSpeed>().speed = speed;
        Destroy(instance,spikeLifeTime);
    }
}