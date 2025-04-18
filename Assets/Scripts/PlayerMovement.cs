using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private BoxCollider2D boxCollider;
    [SerializeField] private float jumpForce = 7f;
    private float wallJumpCooldown;
    private Rigidbody2D rb;
    public float gravity = 9.8f;
    public LineRenderer lineRenderer;
    public int resolution = 50;
    public float horizontalInput;
    [SerializeField] private HealthBar healthBar;
    public int maxHealth = 100;
    public int currentHealth;
    private Vector3 respawnPoint;
    [SerializeField] private GameObject gameOverUI;



    public bool IsHanging { get; set; } = false; // Flag untuk mematikan kontrol saat hanging

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        respawnPoint = transform.position;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        if (healthBar.slider == null)
        {
            Debug.LogWarning("Slider BELUM di-assign di HealthBar!");
        }
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
        

    }
    private void Update()
    {
        if (IsHanging) return; // Stop semua kontrol saat sedang hanging

        horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Flip karakter
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Jump();
            DrawTrajectory();
        }

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if (wallJumpCooldown < 0.2f)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                Jump();

            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 2;
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }

        if (transform.position.y < -10f) // Jatuh terlalu jauh
        {
            TakeDamage(20);
            AudioManager.instance.PlaySound("hurt");
            transform.position = respawnPoint;
        }

        if (currentHealth <= 0)
        {
            GameOver(); 
        }
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        respawnPoint = newCheckpoint;
        Debug.Log("Checkpoint updated: " + newCheckpoint);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.SetValue(currentHealth);

        Debug.Log("Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Object.FindAnyObjectByType<GameOverController>();
        }
    }


    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        AudioManager.instance.PlaySound("jump");
        anim.SetTrigger("jump");
    }
    void DrawTrajectory()
    {
        float timeStep = 0.1f;
        Vector3[] positions = new Vector3[resolution];

        for (int i = 0; i < resolution; i++)
        {
            float t = i * timeStep;
            float x = rb.linearVelocity.x * t;
            float y = jumpForce * t - 0.5f * gravity * t * t;
            positions[i] = transform.position + new Vector3(x, y, 0);
        }

        lineRenderer.positionCount = resolution;
        lineRenderer.SetPositions(positions);
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            0.1f,
            groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            new Vector2(transform.localScale.x, 0),
            0.1f,
            wallLayer);
        return raycastHit.collider != null;
    }


    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetValue(currentHealth);

        Debug.Log("Increased Health: " + currentHealth);
    }

    private void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            AudioManager.instance.PlaySound("lose");
        }

        body.linearVelocity = Vector2.zero;
        body.gravityScale = 0; 
    }

    
}
