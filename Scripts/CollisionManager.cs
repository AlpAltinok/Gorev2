using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CollisionManager : MonoBehaviour
{
    private Animator animator;
    public static bool death;
    [SerializeField] GameObject buton;

    private void Start()
    {
        death = false;
        animator = GetComponent<Animator>();
        buton.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Engel"))
        {
            animator.SetTrigger("Death");
            death = true;
            buton.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            animator.SetTrigger("Death");
            death = true;
            buton.SetActive(true);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(0);
    }
}
