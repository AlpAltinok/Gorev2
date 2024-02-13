using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRoadManager : MonoBehaviour
{
    public GameObject roadPrefab; 
    public int initialRoadCount = 5;
    public float roadLength = 10f; 
    public Transform playerTransform; 
    public float despawnDistance = 20f; 

    private List<GameObject> spawnedRoads = new List<GameObject>(); 
    private Vector3 spawnPosition = Vector3.zero; 

    void Start()
    {
  
        for (int i = 0; i < initialRoadCount; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        
        for (int i = 0; i < spawnedRoads.Count; i++)
        {
            if (spawnedRoads[i].transform.position.z + despawnDistance < playerTransform.position.z)
            {
                Destroy(spawnedRoads[i]);
                spawnedRoads.RemoveAt(i);
                SpawnRoad(); 
            }
        }
    }

   
    void SpawnRoad()
    {
        GameObject newRoad = Instantiate(roadPrefab, spawnPosition, Quaternion.identity);
        spawnedRoads.Add(newRoad);
        spawnPosition.z += roadLength;
    }
}
