using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private float spawnTime;
    private float timer;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject zombiePrefab;

    private void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
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
        int i = Random.Range(1, 4);
        GameObject unit = Instantiate(zombiePrefab, spawnPoints[i].position, Quaternion.identity);
        unit.GetComponent<Enemy>().laneNumber = i;
    }
}
