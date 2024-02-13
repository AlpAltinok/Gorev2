using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyObstacle : MonoBehaviour
{
    public GameObject carPrefab;
    public float carSpawnDistance = 50f;
    public Transform[] carLanes;
    public float carSpeed = 100f;
    public Transform playerTransform;
    public float despawnDistance = 5f;

    private GameObject currentCar;
    private Vector3 spawnPosition;

    void Update()
    {

        if (currentCar == null && playerTransform.position.z + carSpawnDistance > transform.position.z)
        {
            SpawnCar();
        }

        if (currentCar != null && currentCar.activeInHierarchy && currentCar.transform.position.z + despawnDistance < playerTransform.position.z)
        {
            currentCar.SetActive(false);
            SpawnCar();
        }
    }


    void SpawnCar()
    {

        Transform randomLane = carLanes[Random.Range(0, carLanes.Length)];


        float spawnX = randomLane.position.x;
        spawnPosition = new Vector3(spawnX, 10f, playerTransform.position.z + carSpawnDistance);
        Quaternion spawnRotation = Quaternion.Euler(0, 180, 0);


        currentCar = Instantiate(carPrefab, spawnPosition, spawnRotation);
        Rigidbody carRigidbody = currentCar.GetComponent<Rigidbody>();


        if (carSpeed < 10f)
        {
            currentCar.tag = "Default";
        }
        else
        {

            currentCar.tag = "Car";

            carRigidbody.velocity = Vector3.back * carSpeed;
        }
    }
}
