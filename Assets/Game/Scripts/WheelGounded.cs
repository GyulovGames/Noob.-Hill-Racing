using UnityEngine;

public class WheelGrounded : MonoBehaviour
{
    [HideInInspector] public bool wheelGrounded;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        wheelGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        wheelGrounded = false;
    }
}