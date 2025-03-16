using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private float spawnTime;
    private float timer;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject zombiePrefab;

    private void Start()
    {
        spawnPoint=GetComponent<Transform>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer>spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(zombiePrefab,spawnPoint.position,Quaternion.identity);
    }
}
