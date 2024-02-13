using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public List<GameObject> obstaclePrefabs; 
    public int poolSizePerObstacle = 5; 
    public float spawnDistance = 50f; 
    public float spawnInterval = 2f; 
    public Transform[] lanes; 
    public float laneWidth = 2.5f; 
    public Transform playerTransform; 

    private List<List<GameObject>> obstaclePools = new List<List<GameObject>>(); 
    private float lastSpawnTime; 
    private float despawnDistance = 20f; 

    void Start()
    {
    
        foreach (var obstaclePrefab in obstaclePrefabs)
        {
            List<GameObject> obstaclePool = new List<GameObject>();
            for (int i = 0; i < poolSizePerObstacle; i++)
            {
                GameObject obstacle = Instantiate(obstaclePrefab, Vector3.zero, Quaternion.identity);
                obstacle.SetActive(false);
                obstaclePool.Add(obstacle);
            }
            obstaclePools.Add(obstaclePool);
        }
        lastSpawnTime = Time.time;
    }

    void Update()
    {
     
        if (Time.time - lastSpawnTime > spawnInterval)
        {
            if (playerTransform.position.z + spawnDistance > lastSpawnTime)
            {
                SpawnObstacle();
                lastSpawnTime = Time.time;
            }
        }

        for (int i = 0; i < obstaclePools.Count; i++)
        {
            foreach (var obstacle in obstaclePools[i])
            {
                if (obstacle.activeInHierarchy && obstacle.transform.position.z + despawnDistance < playerTransform.position.z)
                {
                    obstacle.SetActive(false);
                }
            }
        }
    }


    void SpawnObstacle()
    {
     
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
        Transform randomLane = lanes[Random.Range(0, lanes.Length)];
        List<GameObject> obstaclePool = GetObstaclePool(obstaclePrefab);
        if (obstaclePool != null)
        {
            
            float spawnX = randomLane.position.x;
            Vector3 spawnPosition = new Vector3(spawnX, 0f, playerTransform.position.z + spawnDistance);
            Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);
            GameObject obstacle = GetPooledObstacle(obstaclePool);
            if (obstacle != null)
            {
                obstacle.transform.position = spawnPosition;
                obstacle.transform.rotation = spawnRotation;
                obstacle.SetActive(true);
            }
        }
    }

  
    GameObject GetPooledObstacle(List<GameObject> obstaclePool)
    {
        foreach (var obstacle in obstaclePool)
        {
            if (!obstacle.activeInHierarchy)
            {
                return obstacle;
            }
        }
        return null;
    }

    List<GameObject> GetObstaclePool(GameObject obstaclePrefab)
    {
        int index = obstaclePrefabs.IndexOf(obstaclePrefab);
        if (index != -1)
        {
            return obstaclePools[index];
        }
        return null;
    }
}
