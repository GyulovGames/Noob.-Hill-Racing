using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource coinGetSound;
    [SerializeField] private CircleCollider2D circleCollider;

    private void OnEnable()
    {
        circleCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        circleCollider.enabled = false;
        coinGetSound.Play();
        animator.enabled = true;
    }

    private void DisableCoin()
    {
        animator.enabled = false;
        transform.localScale = new Vector2(0.25f, 0.25f);
        gameObject.SetActive(false);
    }   
}