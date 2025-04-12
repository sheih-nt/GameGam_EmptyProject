using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [Header("Settings")]
    public Transform player;
    public float freezeDuration = 2f;
    public float moveSpeed = 10f;
    public int maxHealth = 3;
    
    [Header("Animations")]
    public AnimationClip deathAnimation;
    
    [Header("Components")]
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D hitCollider;
    
    // Private variables
    private Vector3 targetPosition;
    private bool isFrozen = true;
    private float freezeTimer;
    private int currentHealth;
    private bool isDead = false;
    private RuntimeAnimatorController originalController;

    void Start()
    {
        // Get components
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitCollider = GetComponent<Collider2D>();
        originalController = animator.runtimeAnimatorController;
        
        // Initialize
        currentHealth = maxHealth;
        freezeTimer = freezeDuration;
        animator.SetBool("IsSpeaking", true);
    }

    void Update()
    {
        if (isDead) return;

        if (isFrozen)
        {
            freezeTimer -= Time.deltaTime;
            
            if (freezeTimer <= 0)
            {
                targetPosition = player.position;
                isFrozen = false;
                animator.SetBool("IsSpeaking", false);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isFrozen = true;
                freezeTimer = freezeDuration;
                animator.SetBool("IsSpeaking", true);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        
        // Switch to death animation
        if (deathAnimation != null)
        {
            AnimatorOverrideController overrideController = new AnimatorOverrideController(originalController);
            overrideController["DefaultDeath"] = deathAnimation; // Use a placeholder clip name
            animator.runtimeAnimatorController = overrideController;
            animator.Play("DefaultDeath");
        }
        
        if (hitCollider != null) hitCollider.enabled = false;
        Destroy(gameObject, deathAnimation != null ? deathAnimation.length : 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        if (!isFrozen && !isDead)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetPosition, 0.3f);
            Gizmos.DrawLine(transform.position, targetPosition);
        }
    }
}