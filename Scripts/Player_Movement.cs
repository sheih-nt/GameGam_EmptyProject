using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Animator animator;
    
    Vector2 movement;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D или ←/→
        movement.y = Input.GetAxisRaw("Vertical");   // W/S или ↑/↓
        
        movement = movement.normalized;
        
        bool isD = Input.GetKey(KeyCode.D);
        animator.SetBool("D", isD);
        
        bool isA = Input.GetKey(KeyCode.A);
        animator.SetBool("A", isA);
        
        bool isW = Input.GetKey(KeyCode.W);
        animator.SetBool("W", isW);
        
        bool isS = Input.GetKey(KeyCode.S);
        animator.SetBool("S", isS);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}