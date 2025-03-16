using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float detectionRange = 1.5f; // 앞 좀비 감지 범위
    public float climbHeight = 1.2f;    // 좀비 한 마리 높이
    public float climbDuration = 0.5f;  // 올라가는 시간
    public float moveSpeed = 0.5f;      // 왼쪽으로 이동 속도
    public float maxHealth = 100f;      // 최대 체력
    private float currentHealth;        // 현재 체력

    public Slider healthSlider;        
    private Rigidbody2D rb;
    private bool isClimbing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void FixedUpdate()
    {
        if (!isClimbing)
        {
            MoveLeft();
            TryClimbOnZombie();
        }
    }

    void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    void TryClimbOnZombie()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, detectionRange);
        if (hit.collider != null && hit.collider.CompareTag("Enemy") && hit.collider.gameObject != gameObject) // 앞에 좀비가 있는지 확인
        {
            StartCoroutine(ClimbOnZombie(hit.collider.transform));
        }
        else
        {
            isClimbing = false; 
        }
    }

    IEnumerator ClimbOnZombie(Transform targetZombie)
    {
        isClimbing = true;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        Vector3 targetPosition = new Vector3(transform.position.x, targetZombie.position.y + climbHeight, transform.position.z);
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < climbDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / climbDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        rb.isKinematic = false;
        isClimbing = false;
    }

    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    
    void Die()
    {
        Destroy(gameObject); 
    }
}