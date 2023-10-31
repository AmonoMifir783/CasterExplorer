using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamdraAI : MonoBehaviour
{
    public float patrolSpeed = 2f; // Скорость патрулирования
    public float chaseSpeed = 5f; // Скорость преследования
    public float chaseRange = 10f; // Расстояние обнаружения игрока
    public float patrolWaitTime = 2f; // Время ожидания при патрулировании
    public Transform[] patrolPoints; // Массив точек патрулирования
    private float nextDamageTime = 3f;
    public float attackCooldown = 4f;
    public int damageAmount = 25;
    private float attackTimer = 0f;
    public float rotationSpeed = 5f;
    public float attackRange = 10f;
    public float runRange = 7f;
    public float projectileSpeed = 20f; // Скорость полета плевков
    public GameObject projectilePrefab; // Префаб плевка
    private Transform player; // Ссылка на игрока
    private int currentPatrolIndex; // Индекс текущей точки патрулирования
    private float patrolTimer; // Таймер ожидания при патрулировании
    private bool isChasing; // Флаг преследования игрока
    private bool isRunningAway; // Флаг убегания от игрока
    private Vector3 runAwayPosition; // Позиция, в которую нужно убежать
    private SpellReaction spellReaction;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Находим игрока по тегу
        currentPatrolIndex = 0; // Устанавливаем начальный индекс патрулирования
        patrolTimer = 0f; // Обнуляем таймер ожидания
        isChasing = false; // Инициализируем флаг преследования
        isRunningAway = false; // Инициализируем флаг убегания
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

            // Проверяем, если игрок находится в пределах атаки
            if (distanceToPlayer <= attackRange)
            {
                // Атакуем игрока
                if (Time.time >= nextDamageTime && distanceToPlayer > runRange)
                {
                    LaunchProjectile();
                    nextDamageTime = Time.time + attackCooldown;
                }
                
            }
            else
            {
                // Подходим к игроку, чтобы находиться в пределах атаки
                transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            }
        }
        else
        {
            isChasing = false; // Выключаем режим преследования

            // Если игрок слишком близко, убегаем от него
            
                // Продолжаем патрулирование
                Patrol();
            
        }
        if (distanceToPlayer < runRange)
        {
            RunAwayFromPlayer();
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

    private void LaunchProjectile()
    {
        // Создаем экземпляр плевка
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

        // Направляем плевок на игрока
        Vector3 direction = (player.position - transform.position).normalized;
        projectileRigidbody.velocity = direction * projectileSpeed;

        // Добавьте компонент Collider к плевку, если его нет
        Collider projectileCollider = projectile.GetComponent<Collider>();
        if (!projectileCollider)
        {
            projectileCollider = projectile.AddComponent<SphereCollider>();
        }

        // Нанесение урона игроку при столкновении с плевком
        projectileCollider.isTrigger = true;
        //projectileCollider.gameObject.tag = "Projectile";

        // Добавьте скрипт, обрабатывающий столкновение плевка с игроком
        projectileCollider.gameObject.AddComponent<ProjectileCollision>();
    }

    private void RunAwayFromPlayer()
    {
        // Устанавливаем позицию, в которую нужно убежать
        runAwayPosition = transform.position + (transform.position - player.position).normalized;

        // Убегаем от игрока
        transform.position = Vector3.MoveTowards(transform.position, runAwayPosition, chaseSpeed * Time.deltaTime);

        // Поворачиваем противника в направлении точки, куда убегает
        Vector3 direction = (runAwayPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Проверяем, если игрок стал далеко, прекращаем убегать
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            isRunningAway = false;
        }
    }
}