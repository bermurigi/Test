using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    //이동
    public float moveSpeed = 1.5f;
    public float jumpForce = 5f;
    public float detectDistance = 100.0f;
    private bool isJumping = false;
    private bool isStopped = false;
    public float stopDistance = 1.5f;
    private Rigidbody2D rb;
    public int laneNumber; // 현재 좀비가 속한 라인
    private int enemyLayer;



    //공격
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    private bool canAttack = true;
    private float lastAttackTime = 0f;
    public float attackDamage = 10f;
    private Transform targetBuilding;





    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        gameObject.layer = LayerMask.NameToLayer($"Lane{laneNumber}");


        enemyLayer = LayerMask.GetMask("Lane" + laneNumber.ToString());



    }
    void Update()
    {

        CheckFront();

    }
    void FixedUpdate()
    {
        if (!isStopped && !isJumping)
        {
            MoveLeft();

        }
        FindNearestBuildingAndAttack();
    }


    void MoveLeft()
    {

        if (transform.position.x > -0.8)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }


    void FindNearestBuildingAndAttack()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Hero");
        Transform closestBuilding = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject building in buildings)
        {
            float distanceToBuilding = Vector2.Distance(transform.position, building.transform.position);
            if (distanceToBuilding < closestDistance)
            {
                closestDistance = distanceToBuilding;
                closestBuilding = building.transform;
            }
        }


        if (closestBuilding != null && closestDistance <= attackRange && canAttack)
        {
            Attack(closestBuilding);
        }
    }


    void Attack(Transform building)
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {


            building.gameObject.GetComponent<Health>().TakeDamage(attackDamage);

            lastAttackTime = Time.time;
            canAttack = false;
            StartCoroutine(ResetAttackCooldown());
        }
    }


    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void CheckFront()
    {


        // 레이캐스트 발사
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.left, detectDistance, enemyLayer);


        Debug.DrawRay(transform.position, Vector2.left * detectDistance, Color.red);

        // 모든 충돌체를 순차적으로 확인
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                // 자기 자신은 제외하고 두 번째 충돌부터 처리
                if (hit.collider.gameObject != gameObject)
                {

                    Jump();
                    break;
                }
            }
        }
    }
    

    // 점프 처리
    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);


            StartCoroutine(ResetJump());
        }
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(2f);
        isJumping = false;
    }







    
}