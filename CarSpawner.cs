using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] carPrefabs;
    [SerializeField] float[] carPrefabsIncidence;

    [SerializeField] int[] tilesNumBetweenCars;
    [SerializeField] float[] tilesNumBetweenCarsIncidence;

    [SerializeField] float minCarSpeed = 1;
    [SerializeField] float maxCarSpeed = 6;
    float carLifeTime;
    [SerializeField] float initCarDist = 24;

    [SerializeField] int playerRow = 5;

    void Awake()
    {
        carLifeTime = 15; //2*transform.position.x * maxCarSpeed;
    }

    void Start()
    {
        for (int i = 0; i < 32; i++) RowSpawner(i);        
    }

    void RowSpawner(int row){
        
        Vector2 spawnDir = Vector2.left;
        Vector2 spawnerPos = (Vector2)transform.position + row * Vector2.up;
        float carSpeed = Random.Range(minCarSpeed, maxCarSpeed);

        if (Random.Range(0,2)==0)
        {
            spawnDir = Vector2.right;
            spawnerPos = new Vector2(-spawnerPos.x, spawnerPos.y);
        }

        if(row < playerRow - 1 || row > playerRow + 1)
            InitSpawn(spawnerPos, spawnDir, carSpeed);
        StartCoroutine(SpawnRepeat(spawnerPos, spawnDir, carSpeed));
    }

    void InitSpawn(Vector2 spawnerPos, Vector2 spawnDir, float carSpeed){

        Vector2 offset = Vector2.zero;
        float dist = 0;

        while(true){
            int tilesNum = tilesNumBetweenCars.RandomUnnormalized(tilesNumBetweenCarsIncidence);
            
            dist += tilesNum;
            if(dist > initCarDist) return;

            offset += Vector2.Scale(-spawnDir, tilesNum * Vector2.left);

            Spawn(spawnerPos, spawnDir, carSpeed, offset);
        }


        // while(true){
        //     int spikesNum = spikeNums.RandomUnnormalized(spikeNumsIncidence);
        //     dist += spikesNum;

        //     if(dist > initSpikeDist) return;            

        //     for (int i = 0; i < spikesNum; i++)
        //     {
        //         Spawn(offset);
        //         offset += Vector2.left;
        //     }

        //     int tilesNum = tilesNumBetweenSpikes.RandomUnnormalized(tilesNumBetweenSpikesIncidence);
        //     int tilesToSkip = tilesNum + spikesNum - 1;
        //     dist += tilesToSkip;
        //     if(dist > initSpikeDist) return;

        //     offset += tilesToSkip * Vector2.left;
        // }
    }

    IEnumerator SpawnRepeat(Vector2 spawnerPos, Vector2 spawnDir, float carSpeed){
        while(true){
            int tilesNum = tilesNumBetweenCars.RandomUnnormalized(tilesNumBetweenCarsIncidence);
            yield return new WaitForSeconds(tilesNum / carSpeed);
            Spawn(spawnerPos, spawnDir, carSpeed);
        }
    }

    void Spawn(Vector2 spawnerPos, Vector2 spawnDir, float carSpeed){
        Spawn(spawnerPos, spawnDir, carSpeed, Vector2.zero);
    }

    void Spawn(Vector2 spawnerPos, Vector2 spawnDir, float carSpeed, Vector2 offset){
        GameObject instance = Instantiate(
                carPrefabs.RandomUnnormalized(carPrefabsIncidence),
                spawnerPos + offset,
                Quaternion.identity, transform);

        instance.transform.localScale = new Vector2(spawnDir.x, 1);
        ConstantSpeed constantSpeed = instance.GetComponent<ConstantSpeed>();
        constantSpeed.speed = carSpeed;
        constantSpeed.dir = spawnDir;
        Destroy(instance,carLifeTime);
    }
}
