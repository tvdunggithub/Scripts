using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

***Script này để di chuyển enemy***

-Khởi tạo đối tượng EnemySpawner để lấy ra turn enemy trong wave hiện tại
-Sau đó lấy từ turn enemy hiện tại list đường đi của enemy
-Khởi tạo điểm ban đầu với wavepointIndex = 0 từ list(gán =  transform.position)
-viết phương thức trong update để di chuyển enemy (transform.position = Vector2.MoveTowards)
*/
public class PathFinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> wavepoints;
    int wavepointIndex = 0;

    void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }


    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        wavepoints = waveConfig.GetWayPoints();
        transform.position = wavepoints[wavepointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }


    void FollowPath(){
        if(wavepointIndex < wavepoints.Count){
            
            Vector3 targetPosition = wavepoints[wavepointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if(transform.position == targetPosition){
                wavepointIndex++;
            }
        }else{
            wavepointIndex = 0;
        }
    }
}
