using UnityEngine;

public class Canister : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource refuelSound;
    [SerializeField] private CircleCollider2D circleCollider;

    private void OnEnable()
    {
        circleCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        circleCollider.enabled = false;
        refuelSound.Play();
        animator.enabled = true;
    }

    private void DisableCanister()
    {
        animator.enabled = false;
        transform.localScale = new Vector2(0.17f, 0.17f);
        gameObject.SetActive(false);
    }
}