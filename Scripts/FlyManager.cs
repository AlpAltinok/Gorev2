using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyManager : MonoBehaviour
{

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 120 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this);
            
        }
    }
}
