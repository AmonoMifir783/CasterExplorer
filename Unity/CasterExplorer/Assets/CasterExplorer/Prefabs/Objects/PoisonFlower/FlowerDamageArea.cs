using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damageAmount = 10; // Количество урона, которое игрок получает в зоне
    private bool isPlayerInside = false; // Флаг, указывающий, находится ли игрок в зоне
    private float nextDamageTime = 0f; // Время следующего нанесения урона

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true; // Устанавливаем флаг, что игрок вошел в зону
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false; // Устанавливаем флаг, что игрок вышел из зоны
        }
    }

    void Update()
    {
        if (isPlayerInside && Time.time >= nextDamageTime)
        {
            // Вызываем метод TakeDamage() в скрипте персонажа,
            // чтобы нанести урон игроку
            PlayerHealth playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            playerHealthScript.TakeDamage(damageAmount);

            // Устанавливаем новое время следующего нанесения урона с учетом задержки в 5 секунд
            nextDamageTime = Time.time + 5f;
        }
    }
}


