using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float laneWidth = 3.5f; 
    private Animator animator;
    private int currentLane = 0; 
    private Rigidbody rb;
    private Vector3 originalScale;
    private bool isJumping = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (CollisionManager.death == false)
        {
            
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.localScale = originalScale;
               
                if (currentLane < 1)
                {
                    currentLane++; 
                    float targetX = transform.position.x + laneWidth;
                    transform.position = Vector3.Lerp(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), 0.5f); 
                }
            }
            
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.localScale = originalScale;
               
                if (currentLane > -1)
                {

                    currentLane--; 
                    float targetX = transform.position.x - laneWidth;
                    transform.position = Vector3.Lerp(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), 0.5f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                animator.SetTrigger("Slide");

            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && isJumping == false)
            {
                transform.localScale = originalScale;
                rb.AddForce(Vector3.up * 25, ForceMode.Impulse);
                animator.SetTrigger("Jump");
                isJumping = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
    
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fly"))
        {
            animator.SetBool("Flying",true);
            Vector3 newPosition = transform.position; 
            newPosition.y = 10f; 
            transform.position = newPosition; 
            GetComponent<Rigidbody>().useGravity = false;
            StartCoroutine(WaitAndPrint());

        }
    }
    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(6);
        animator.SetBool("Flying", false);
        GetComponent<Rigidbody>().useGravity = true;
    }

}
