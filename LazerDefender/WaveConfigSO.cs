using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

***Script này để tạo ra các wavePrefab***

Thông tin wave:
-1 emptyObject bao gồm các emptyObject con là các điểm mà enemy sẽ đi qua
-Tốc độ đi của enemy
-1 List những enemy sẽ xuất hiện trong 1 wave

Phương thức trả về:
-Size và enemy theo index
-List các điểm enemy đi qua
-Tốc độ và điểm bắt đầu
*/

[CreateAssetMenu(menuName = "Wave config", fileName = "New wave config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 0.2f;


    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }


    public Transform GetStartingWaypoint(){
        return pathPrefab.GetChild(0);
    }

    //kẹp giá trị spawn để ko nhỏ hơn giá trị min (ko cần thiết)
    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance,
                                        timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }


    public float GetMoveSpeed(){
        return moveSpeed;
    }

    public List<Transform> GetWayPoints(){
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform child in pathPrefab){
            waypoints.Add(child);
        }
        return waypoints;
    }
}
