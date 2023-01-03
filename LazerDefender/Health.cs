using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-Dùng cho cả enemy và player
-Chứa các thông số heal, isPlayer, score của enemy
*/
public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool isPlayer;
    [SerializeField] int score = 50;
    [SerializeField] GameObject fireLevelUpBuff;
    [SerializeField] GameObject shieldBuff;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    int dropChance;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    //Nhận damage, rung màn hình, hủy enemy or hủy đạn khi chạm vào
    //Player ko có damegeDealer nên khi chạm vào sẽ ko bị hủy (damageDealer = null)
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            //nhận damage
            TakeDamage(damageDealer.GetDamage());
            //khởi tạo hiệu ứng, chơi nhạc, rung màn hình
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            //phá hủy đạn, enemy khi chạm vào player
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    //Nếu là player thì rung màn hình
    void ShakeCamera()
    {
        if(cameraShake != null && isPlayer)
        {
            cameraShake.Play();
        }
    }


    //Trừ máu và hủy khi về 0
    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0 && isPlayer)
        {
            Die();
            levelManager.LoadGameOver();
        }else if(health <= 0)
        {
            Die();
        }
    }

    //Tính điểm, Drop item khi enemy bị phá hủy
    void Die()
    {
        if(!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
            dropChance = Random.Range(0,100);
            if(dropChance <= 4)
            {
                Instantiate(fireLevelUpBuff, transform.position, Quaternion.identity);
            }else if(dropChance <= 9)
            {
                Instantiate(shieldBuff, transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }

    //Khởi tạo đối tượng particleSystem và phá hủy trong khoảng time dc setup
    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }

    }

}

