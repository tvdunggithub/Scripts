using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemEffect
{
    shield, fireLevelUp
}
public class BuffController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 2;
    [SerializeField] ItemEffect itemEffect;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Shooter shooter = other.GetComponent<Shooter>();
        if(shooter != null)
        {
            if(itemEffect == ItemEffect.fireLevelUp)
            {
                shooter.fireLevel++;
                Destroy(gameObject);
            }else
            {
                other.GetComponent<Player>().shield.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = Vector2.down * speed ;
    }
}
