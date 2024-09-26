using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRazer : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Vector3 bulletOffset = new Vector3(1, 0, 0); // 발사 위치 조절

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 shootingDirection = Vector2.down;
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * bulletSpeed;
    }
}
