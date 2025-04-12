using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector2 direction;
    private float speed = 5f;
    private SpriteRenderer spriteRenderer;
    
    private Animator playerAnimator;
    
    private Rigidbody2D playerRb; // Ссылка на Rigidbody2D игрока
    private bool isPlayerFrozen = false;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (player != null)
        {
            spriteRenderer.flipX = player.position.x > transform.position.x;
        }
        playerAnimator = player.GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        StartCoroutine(FreezePlayerForAnimation());
        playerAnimator.SetTrigger("fall");
        
    }
    private void StopPlayerMovement()
    {
        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero; // Останавливаем движение
        }
    }
    private IEnumerator FreezePlayerForAnimation()
    {
        isPlayerFrozen = true;
    
        // Запускаем анимацию заморозки
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("fall");
        }

        // Ожидаем окончания анимации (должно быть синхронизировано с длиной анимации)
        yield return new WaitForSeconds(1f); // Задержка для анимации (поставьте значение соответствующее длительности вашей анимации)

        // Возвращаем возможность двигаться
        isPlayerFrozen = false;
    }
}