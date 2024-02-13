using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
   
    
     private void Update()
        {
            transform.RotateAround(transform.position, Vector3.up, 150 * Time.deltaTime);
        }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            ScoreManager.score += 1000;
        }
    }
}
