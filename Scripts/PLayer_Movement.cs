using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PLayer_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 5f;



    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
            
        if (Input.GetButtonDown("Jump"))
            Jump();
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Hit 0");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
        }
        bool isWalking = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        animator.SetBool("walking", isWalking);
    }
    
    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        
        transform.position = Vector3.MoveTowards(transform.position, transform.position+dir, speed*Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        
    }
    // Start is called before the first frame update


    
}
