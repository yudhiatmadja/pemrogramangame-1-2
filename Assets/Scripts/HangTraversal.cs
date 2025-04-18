using UnityEngine;
using System.Collections;

public class HangTraversal : MonoBehaviour
{
    public Transform[] hangPoints;
    public float swingDuration = 0.4f;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    private int currentHangIndex = 0;
    private bool isHanging = false;
    private bool isSwinging = false;
    private PlayerMovement playerMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (isHanging && !isSwinging)
        {
            if (Input.GetKeyDown(KeyCode.D) && currentHangIndex < hangPoints.Length - 1)
            {
                StartCoroutine(SwingAndMove(currentHangIndex + 1));
            }
            else if (Input.GetKeyDown(KeyCode.A) && currentHangIndex > 0)
            {
                StartCoroutine(SwingAndMove(currentHangIndex - 1));
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                StopHanging();
            }
        }

        if (!isHanging && Input.GetKeyDown(KeyCode.H))
        {
            StartHanging(currentHangIndex);
        }
    }

    public void StartHanging(int index)
    {
        isHanging = true;
        currentHangIndex = index;

        if (playerMovement != null)
        {
            playerMovement.IsHanging = true;
        }

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.position = hangPoints[currentHangIndex].position;
        Debug.Log("Mulai gantung di point: " + currentHangIndex);
    }

    public void StopHanging()
    {
        isHanging = false;

        if (playerMovement != null)
        {
            playerMovement.IsHanging = false;
        }

        rb.gravityScale = 2f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        Debug.Log("Lepas gantung");
    }

    IEnumerator SwingAndMove(int nextIndex)
    {
        isSwinging = true;

        Vector3 startPos = hangPoints[currentHangIndex].position;
        Vector3 targetPos = hangPoints[nextIndex].position;

        float t = 0f;
        float swingAngle = 15f;
        Transform dummy = new GameObject("SwingDummy").transform;
        dummy.position = startPos;

        while (t < swingDuration)
        {
            t += Time.deltaTime;
            float normalized = t / swingDuration;

            float angle = Mathf.Sin(normalized * Mathf.PI) * swingAngle;
            dummy.position = Vector3.Lerp(startPos, targetPos, normalized);
            dummy.rotation = Quaternion.Euler(0f, 0f, angle);
            transform.position = dummy.position;
            yield return null;
        }

        Destroy(dummy.gameObject);

        transform.position = targetPos;
        currentHangIndex = nextIndex;
        isSwinging = false;

        Debug.Log("Sampai di hang point: " + currentHangIndex);
    }
}
