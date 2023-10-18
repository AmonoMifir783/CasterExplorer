using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LivingArmorAI : MonoBehaviour
{
    public float patrolSpeed = 2f; // Скорость патрулирования
    public float chaseSpeed = 5f; // Скорость преследования
    public float chaseRange = 10f; // Расстояние обнаружения игрока
    public float patrolWaitTime = 2f; // Время ожидания при патрулировании
    public Transform[] patrolPoints; // Массив точек патрулирования

    private float nextDamageTime = 0f;
    public float attackCooldown = 0.5f;
    public int damageAmount = 625;
    private float attackTimer = 0f;
    public float rotationSpeed = 5f;
    public float attackRange = 1f;

    private Transform player; // Ссылка на игрока
    private int currentPatrolIndex; // Индекс текущей точки патрулирования
    private float patrolTimer; // Таймер ожидания при патрулировании
    private bool isChasing; // Флаг преследования игрока

    private LivingArmorSR livingArmorSr;
    private SpellReaction spellReaction;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Находим игрока по тегу
        currentPatrolIndex = 0; // Устанавливаем начальный индекс патрулирования
        patrolTimer = 0f; // Обнуляем таймер ожидания
        isChasing = false; // Инициализируем флаг преследования

        livingArmorSr = GetComponent<LivingArmorSR>(); 
        spellReaction = GetComponent<SpellReaction>();

    }
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // Расстояние до игрока
        // Проверяем нахождение игрока в области обзора противника
        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true; // Включаем режим преследования
            RotateTowardsPlayer();

            // Прыгаем на игрока
            if (distanceToPlayer > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            }
            if (Vector3.Distance(transform.position, player.position) <= attackRange && Time.time >= nextDamageTime && !livingArmorSr.isElectric)
            {
                // Атакуем игрока
                Attack();
                Debug.Log((int)(Mathf.Sqrt(damageAmount) * 5));
            }
            if (Vector3.Distance(transform.position, player.position) <= attackRange && Time.time >= nextDamageTime && livingArmorSr.isElectric)
            {
                // Атакуем игрока
                ElectroAttack();
                Debug.Log(livingArmorSr.electroDamage);
            }
        }
        else
        {
            isChasing = false; // Выключаем режим преследования

            // Продолжаем патрулирование
            Patrol();
        }
    }
    private void Patrol()
    {
        // Если противник достиг текущей точки патрулирования, устанавливаем следующую точку и сбрасываем таймер ожидания
        if (transform.position == patrolPoints[currentPatrolIndex].position)
        {
            currentPatrolIndex++;
            patrolTimer = 0f;

            // Если достигли последней точки, возвращаемся к первой
            if (currentPatrolIndex >= patrolPoints.Length)
            {
                currentPatrolIndex = 0;
            }
        }
        // Поворачиваем противника в направлении следующей точки патрулирования
        Vector3 direction = (patrolPoints[currentPatrolIndex].position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Если таймер ожидания достиг нужного значения, перемещаемся к следующей точке патрулирования
        if (patrolTimer >= patrolWaitTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, patrolSpeed * Time.deltaTime);
        }
        else
        {
            patrolTimer += Time.deltaTime; // Увеличиваем таймер ожидания

        }
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
        playerHealthScript.TakeDamage((int)(Mathf.Sqrt(damageAmount) * 5));
        
        // 5
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
    private void ElectroAttack()
    {
        PlayerHealth playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealthScript.TakeDamage(livingArmorSr.electroDamage);

        nextDamageTime = Time.time + 2f;
        attackCooldown = Time.time + 0f;
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            nextDamageTime = Time.time + 2f;
            attackCooldown = Time.time + 0f;
        }
    }
}