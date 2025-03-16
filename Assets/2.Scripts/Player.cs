using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;     
    public float bulletSpeed = 5f;  
    public float fireRate = 1f;     

    void Start()
    {
        InvokeRepeating("Shoot", 0f, fireRate); // 1초마다 자동 공격
    }

    void Shoot()
    {
        Transform targetZombie = FindClosestZombie();
        if (targetZombie != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = (targetZombie.position - firePoint.position).normalized; // 좀비 방향 계산
            rb.velocity = direction * bulletSpeed; // 총알 발사

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 총알 회전
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    Transform FindClosestZombie()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy"); // "Enemy" 태그 가진 모든 좀비 찾기
        Transform closestZombie = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject zombie in zombies)
        {
            float distance = Vector2.Distance(transform.position, zombie.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestZombie = zombie.transform;
            }
        }

        return closestZombie;
    }
}
