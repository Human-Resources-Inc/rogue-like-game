using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>())
        {
            StartCoroutine(NextFloor());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>())
        {
            StopCoroutine(NextFloor());
        }
    }

    private IEnumerator NextFloor()
    {
        animator.SetBool("elevatorActive", true);
        yield return new WaitForSeconds(6);
        animator.SetBool("elevatorActive", false);
        GameManager.instance.currentFloor++;
        SceneManager.LoadScene("GameProcess");
        StartCoroutine(GameManager.instance.RecalculateEnemies());
    }
}
