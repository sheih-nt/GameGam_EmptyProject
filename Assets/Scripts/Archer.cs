using UnityEngine;

public class ArcherEnemy : MonoBehaviour
{
    [Header("Settings")]
    public float AttackDistance = 5f;
    public float ShootingDelay = 2f;
    public GameObject ArrowPrefab;
    public float ArrowSpeed = 7f;
    
    private Transform Player;
    private Animator animator;
    private float AttackTimer;
    private bool IsDead;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsDead) return;
        
        // Проверяем расстояние до Playerа
        if (Vector2.Distance(transform.position, Player.position) <= AttackDistance)
        {
            ПовернутьсяКPlayerу();
            
            if (AttackTimer <= 0)
            {
                Атаковать();
                AttackTimer = ShootingDelay;
            }
            else
            {
                AttackTimer -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }
    
    private void ПовернутьсяКPlayerу()
    {
        // Меняем scale по X для поворота спрайта
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(Player.position.x - transform.position.x) * Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
    
    private void Атаковать()
    {
        animator.SetTrigger("Shooting");
    }
    
    // Вызывается из анимации через Animation Event
    public void ВыпуститьСтрелу()
    {
        if (IsDead) return;
        
        // Создаем стрелу
        GameObject стрела = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
        
        // Направление к Playerу
        Vector2 направление = (Player.position - transform.position).normalized;
        
        // Задаем движение стреле
        стрела.GetComponent<Rigidbody2D>().linearVelocity = направление * ArrowSpeed;
        
        // Поворачиваем стрелу по направлению
        float угол = Mathf.Atan2(направление.y, направление.x) * Mathf.Rad2Deg;
        стрела.transform.rotation = Quaternion.AngleAxis(угол, Vector3.forward);
    }
    
    public void Умереть()
    {
        IsDead = true;
        animator.SetTrigger("Dying");
        Destroy(gameObject, 2f);
    }
    
    // Для визуализации радиуса атаки в редакторе
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);
    }
}