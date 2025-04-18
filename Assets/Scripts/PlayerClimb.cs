using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public float climbSpeed = 4f;
    public Rigidbody2D rb;
    private bool isClimbing = false;
    private float inputVertical;

    void Update()
    {
        inputVertical = Input.GetAxisRaw("Vertical");

        if (isClimbing)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, inputVertical * climbSpeed);
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 1f; // normal gravity
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }
}
