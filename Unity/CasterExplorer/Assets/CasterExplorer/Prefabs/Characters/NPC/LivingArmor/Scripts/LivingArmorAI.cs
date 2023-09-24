using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform patrolPoint1; // Первая точка патрулирования
    public Transform patrolPoint2; // Вторая точка патрулирования
    public float patrolSpeed = 2f; // Скорость патрулирования
    public float chaseSpeed = 5f; // Скорость преследования
    public float attackRange = 1f; // Расстояние для атаки
    public float detectionRange = 10f; // Расстояние, на котором противник замечает игрока
    private float nextDamageTime = 0f;
    public float attackCooldown = 0.5f;
    public int damageAmount = 25;
    private float attackTimer = 0f;
    public float rotationSpeed = 5f;

    private Transform target;
    private Transform player;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool isChasing;

    private void Start()
    {
        // Начинаем с патрулирования к первой точке
        target = patrolPoint1;
        isChasing = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Update()
    {
        // Проверяем, находится ли игрок в зоне видимости
        if (Vector3.Distance(transform.position, player.position) >= detectionRange)
        {
            // Начинаем преследование игрока
            isChasing = true;
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Если противник преследует игрока
        if (isChasing)
        {
            // Проверяем расстояние между противником и игроком для атаки
            if (Vector3.Distance(transform.position, target.position) <= attackRange && Time.time >= nextDamageTime)
            {
                // Атакуем игрока
                Attack();

            }
            else
            {
                // Преследуем игрока
                Chase();
                RotateTowardsPlayer();
            }
        }
        else
        {
            // Продолжаем патрулирование между заданными точками
            Patrol();
        }
    }

    private void Patrol()
    {
        // Двигаемся к текущей целевой точке патрулирования
        transform.position = Vector3.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);

        // Если достигли текущей целевой точки, меняем ее на следующую
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            target = (target == patrolPoint1) ? patrolPoint2 : patrolPoint1;
        }
    }

    private void Chase()
    {
        // Двигаемся к игроку с увеличенной скоростью
        transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
    }
    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void Attack()
    {
        PlayerHealth playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealthScript.TakeDamage(damageAmount);

        // ������������� ����� ����� ���������� ��������� ����� � ������ �������� � 5 ������
        nextDamageTime = Time.time + 2f;
        attackCooldown = Time.time + 0f;
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            nextDamageTime = Time.time + 2f;
            attackCooldown = Time.time + 0f;
        }
        // Проводим атаку на игрока
    }

    // Метод для вызова, когда игрок покидает зону видимости противника
    public void LoseTarget()
    {
        isChasing = false;
        target = patrolPoint1;
    }
}









