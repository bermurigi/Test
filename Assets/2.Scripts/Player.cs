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
        InvokeRepeating("Shoot", 0f, fireRate); // 1�ʸ��� �ڵ� ����
    }

    void Shoot()
    {
        Transform targetZombie = FindClosestZombie();
        if (targetZombie != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = (targetZombie.position - firePoint.position).normalized; // ���� ���� ���
            rb.velocity = direction * bulletSpeed; // �Ѿ� �߻�

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // �Ѿ� ȸ��
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    Transform FindClosestZombie()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy"); // "Enemy" �±� ���� ��� ���� ã��
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
