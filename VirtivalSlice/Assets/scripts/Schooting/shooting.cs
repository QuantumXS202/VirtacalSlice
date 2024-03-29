﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed;

    private float lastFire;

    public float fireDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
        (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
        (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed, 0);
    }
}
