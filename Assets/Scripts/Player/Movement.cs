using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private KeyCode spurtKeyCode;
    [SerializeField] private float spurtForce;
    [SerializeField] private FinderObjectFromSpace checkGround;


    private Vector2 normal;
    private Vector2 direction;
    private float bufferGravityScale;
    private bool facingRight = true;
    private bool hasGround;
    private bool hasDoubleJump;

    public bool HasDoubleJump { get => hasDoubleJump; }


    private void Awake()
    {
        bufferGravityScale = rb.gravityScale;
    }


    private void OnEnable()
    {
        checkGround.OnChangeSpace += CheckGroundAndDoubleJump;
    }


    private void OnDisable()
    {
        checkGround.OnChangeSpace -= CheckGroundAndDoubleJump;
    }


    private void Update()
    {
        UpdateGravityScale();
        Jump();
        Spurt();
    }


    private void FixedUpdate()
    {
        Move();
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        normal = collision.contacts[0].normal;
    }


    private Vector2 GetDirection(Vector2 forward)
    {
        return forward - Vector2.Dot(forward, normal) * normal;
    }


    private void Move()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        if (Input.GetButton("Horizontal"))
        {
            Vector2 offset;
            if (hasGround == true)
            {
                direction = GetDirection(new Vector2(moveDirection, 0).normalized);
                offset = direction * speed * Time.deltaTime;
            }
            else
            {
                offset = new Vector2(moveDirection * speed * Time.deltaTime, 0);
            }

            rb.velocity += offset;

            if (moveDirection > 0.0f && facingRight == false || moveDirection < 0.0f && facingRight == true)
                Flip();
        }
    }


    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }


    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && (hasGround == true || hasDoubleJump == true))
        {
            if (hasGround == false)
            {
                hasDoubleJump = false;
            }
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    private void Spurt()
    {
        if (Input.GetKeyDown(spurtKeyCode))
        {
            Vector2 direction = facingRight == true ? Vector2.right : Vector2.left;
            rb.AddForce(direction * spurtForce, ForceMode2D.Impulse);
        }
    }


    private void CheckGroundAndDoubleJump(bool value)
    {
        hasGround = value;

        if (value == true)
            hasDoubleJump = true;
    }


    private void UpdateGravityScale()
    {
        if (direction.y != 0.0)
            rb.gravityScale = 0;
        if (direction.y == 0.0 || hasGround == false)
            rb.gravityScale = bufferGravityScale;
    }
}
