using UnityEngine;

public class Стрела : MonoBehaviour
{
    public float времяЖизни = 3f;
    
    private void Start()
    {
        // Автоматическое уничтожение через заданное время
        Destroy(gameObject, времяЖизни);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Просто уничтожаем стрелу при столкновении с чем-либо
        if (!other.CompareTag("Enemy")) // Не уничтожаем при столкновении с другими врагами
        {
            Destroy(gameObject);
        }
    }
}