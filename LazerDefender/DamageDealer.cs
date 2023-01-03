using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Chứa thông số damage và Hit() để hủy enemy khi chạm vào player
public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 25;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}

