using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public int poolSize = 10;
    public float spawnDistance = 50f;
    public float spawnInterval = 2f;
    public Transform[] lanes;
    public float laneWidth = 2.5f;
    public Transform playerTransform;

    private List<GameObject> coinPool = new List<GameObject>();
    private float lastSpawnTime;
    private float despawnDistance = 20f;

    void Start()
    {
        InitializeCoinPool();
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnInterval)
        {
            if (playerTransform.position.z + spawnDistance > lastSpawnTime)
            {
                SpawnCoin();
                lastSpawnTime = Time.time;
            }
        }

        foreach (var coin in coinPool)
        {
            if (coin.activeInHierarchy && coin.transform.position.z + despawnDistance < playerTransform.position.z)
            {
                coin.SetActive(false);
            }
        }
    }

    void InitializeCoinPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab, Vector3.zero, Quaternion.identity);
            coin.SetActive(false);
            coinPool.Add(coin);
        }
    }

    void SpawnCoin()
    {
        Transform randomLane = lanes[Random.Range(0, lanes.Length)];
        float spawnX = randomLane.position.x;
        Vector3 spawnPosition = new Vector3(spawnX, 1.5f, playerTransform.position.z + spawnDistance);
        Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);

        GameObject coin = GetPooledCoin();
        if (coin != null)
        {
            coin.transform.position = spawnPosition;
            coin.transform.rotation = spawnRotation;
            coin.SetActive(true);
        }
    }

    GameObject GetPooledCoin()
    {
        foreach (var coin in coinPool)
        {
            if (!coin.activeInHierarchy)
            {
                return coin;
            }
        }
        return null;
    }
}
