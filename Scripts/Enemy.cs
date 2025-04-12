using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 2f;
    private Animator animator;
    private bool isDeath;

    private Collider2D enemyCollider;
    private Collider2D playerCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform firePoint;

    private bool canShoot = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isDeath = false;

        spriteRenderer = GetComponent<SpriteRenderer>();

        enemyCollider = GetComponent<Collider2D>();
        playerCollider = player.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(enemyCollider, playerCollider);

        // Запускаем цикл стрельбы
        StartCoroutine(ShootingCycle());
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < 10f)
        {
            if (isDeath) return;
            
            FollowPlayer();
            spriteRenderer.flipX = player.position.x > transform.position.x;
            
            if (Input.GetMouseButtonDown(0) && Mathf.Abs(transform.position.x - player.position.x) < 2f)
            {
                StartCoroutine(DieAfterDelay());
            }
            
            if (canShoot)
            {
                ShootFireball();
                canShoot = false; // Сброс, чтобы выстрел был однократным
            }
        }
        
    }

    private IEnumerator DieAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isDeath = true;
        animator.SetBool("Death", isDeath);
        Destroy(gameObject, 1f);
    }

    private void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private IEnumerator ShootingCycle()
    {
        while (!isDeath)
        {
            
            yield return new WaitForSeconds(3f); // Пауза до сле
            canShoot = true;  // Момент выстрела
            animator.SetTrigger("Shoot");
        }
    }

    private void ShootFireball()
    {
        if (fireballPrefab == null || firePoint == null) return;

        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Fireball fb = fireball.GetComponent<Fireball>();
        if (fb != null)
        {
            Vector2 dir = (player.position - firePoint.position).normalized;
            fb.SetDirection(dir); 
        }
    }

}
