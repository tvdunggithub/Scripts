using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateMin =-180, rotateMax=180;
    private float rotateSpeed;
    void Start()
    {
        rotateSpeed = Random.Range(rotateMin, rotateMax);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
    }
}
