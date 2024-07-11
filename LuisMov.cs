using UnityEngine;

public class LuisMov : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    public Joystick joystick;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 initialPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        Vector2 direction = joystick.Direction;
        bool isWalking = direction != Vector2.zero;
        animator.SetBool("BoolW", isWalking);

        if (isWalking)
        {
            MovePlayer(direction);
            SetAnimationParameters(direction);
        }
        else
        {
            rb.velocity = Vector2.zero;
            SetIdleAnimation();
        }

        float Horizontal = direction.x;
        float Vertical = direction.y;

        if (Horizontal != 0 || Vertical != 0)
        {
            animator.SetFloat("UltimoX", Horizontal);
            animator.SetFloat("UltimoY", Vertical);
        }
    }

    private void MovePlayer(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    private void SetAnimationParameters(Vector2 direction)
    {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    private void SetIdleAnimation()
    {
        Vector2 currentDirection = rb.velocity.normalized;
        animator.SetFloat("Horizontal", currentDirection.x);
        animator.SetFloat("Vertical", currentDirection.y);
        animator.SetFloat("Speed", 0);
    }

    public void ResetPlayerPosition()
    {
        transform.position = initialPosition;
    }
}
