using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Canister : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CircleCollider2D circleCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        circleCollider.enabled = false;
        animator.enabled = true;
    }

    private void DisableGameObject()
    {
        animator.enabled = false;
        transform.localScale = new Vector2(0.17f, 0.16f);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        circleCollider.enabled = true;
    }
}
