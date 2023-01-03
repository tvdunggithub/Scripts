using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Khởi tạo đạn
public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;
    [SerializeField] Transform[] firePoint;
    [SerializeField] bool isPlayer;
    public int fireLevel = 1;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;


    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    //Dùng StopCoroutine để cưỡng chế kết thúc vòng lặp vô hạn
    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    //Khỏi tạo đạn cách nhau 1 khoảng thời gian và sau số time xác định tự hủy 
    //Chơi nhạc khi khởi tạo đạn và di chuyển đạn
    IEnumerator FireContinuously()
    {
        while(true)
        {
            if(fireLevel == 1)
            {
                GameObject instance = Instantiate(projectilePrefab, 
                                            firePoint[0].transform.position, 
                                            Quaternion.identity);

                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                if(rb != null)
                {
                    rb.velocity = transform.up * projectileSpeed;
                }
                Destroy(instance, projectileLifetime);
            }
            if(fireLevel == 2)
            {
                GameObject instance1 = Instantiate(projectilePrefab, 
                                            firePoint[1].transform.position, 
                                            Quaternion.identity);
                GameObject instance2 = Instantiate(projectilePrefab, 
                                            firePoint[2].transform.position, 
                                            Quaternion.identity);

                Rigidbody2D rb1 = instance1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = instance2.GetComponent<Rigidbody2D>();
                if(rb1 != null && rb2 != null)
                {
                    rb1.velocity = transform.up * projectileSpeed;
                    rb2.velocity = transform.up * projectileSpeed;
                }
                Destroy(instance1, projectileLifetime);
                Destroy(instance2, projectileLifetime);
            }
            if(fireLevel >= 3 && isPlayer)
            {
                GameObject instance = Instantiate(projectilePrefab, 
                                            firePoint[0].transform.position, 
                                            Quaternion.identity);
                GameObject instance1 = Instantiate(projectilePrefab, 
                                            firePoint[1].transform.position, 
                                            Quaternion.identity);
                GameObject instance2 = Instantiate(projectilePrefab, 
                                            firePoint[2].transform.position, 
                                            Quaternion.identity);
                
                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                Rigidbody2D rb1 = instance1.GetComponent<Rigidbody2D>();
                Rigidbody2D rb2 = instance2.GetComponent<Rigidbody2D>();
                if(rb1 != null && rb2 != null && rb != null)
                {
                    rb.velocity = transform.up * projectileSpeed;
                    rb1.velocity = transform.up * projectileSpeed;
                    rb2.velocity = transform.up * projectileSpeed;
                }
                Destroy(instance, projectileLifetime);
                Destroy(instance1, projectileLifetime);
                Destroy(instance2, projectileLifetime);
            }
            if(fireLevel == 4 && !isPlayer)
            {
                List<GameObject> instances = new List<GameObject>();
                List<Rigidbody2D> rbs = new List<Rigidbody2D>();
                for(int i = 0; i < 4; i++)
                {
                    GameObject instance = Instantiate(projectilePrefab, 
                                            firePoint[i].transform.position, 
                                            firePoint[i].transform.rotation);
                    instances.Add(instance);
                }
                for(int i = 0; i < 4; i++)
                {
                    Rigidbody2D rb = instances[i].GetComponent<Rigidbody2D>();
                    if(rb != null)
                    {
                        rbs.Add(rb);
                        rbs[i].velocity = firePoint[i].transform.up * projectileSpeed;
                        Destroy(instances[i], projectileLifetime);
                    }
                }
            }
            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                                            baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, 
                                            minimumFiringRate, float.MaxValue);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}

