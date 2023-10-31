using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamdraAI : MonoBehaviour
{
    public float patrolSpeed = 2f; // �������� ��������������
    public float chaseSpeed = 5f; // �������� �������������
    public float chaseRange = 10f; // ���������� ����������� ������
    public float patrolWaitTime = 2f; // ����� �������� ��� ��������������
    public Transform[] patrolPoints; // ������ ����� ��������������
    private float nextDamageTime = 3f;
    public float attackCooldown = 4f;
    public int damageAmount = 25;
    private float attackTimer = 0f;
    public float rotationSpeed = 5f;
    public float attackRange = 10f;
    public float runRange = 7f;
    public float projectileSpeed = 20f; // �������� ������ �������
    public GameObject projectilePrefab; // ������ ������
    private Transform player; // ������ �� ������
    private int currentPatrolIndex; // ������ ������� ����� ��������������
    private float patrolTimer; // ������ �������� ��� ��������������
    private bool isChasing; // ���� ������������� ������
    private bool isRunningAway; // ���� �������� �� ������
    private Vector3 runAwayPosition; // �������, � ������� ����� �������
    private SpellReaction spellReaction;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // ������� ������ �� ����
        currentPatrolIndex = 0; // ������������� ��������� ������ ��������������
        patrolTimer = 0f; // �������� ������ ��������
        isChasing = false; // �������������� ���� �������������
        isRunningAway = false; // �������������� ���� ��������
        spellReaction = GetComponent<SpellReaction>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // ���������� �� ������

        // ��������� ���������� ������ � ������� ������ ����������
        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true; // �������� ����� �������������
            RotateTowardsPlayer();

            // ���������, ���� ����� ��������� � �������� �����
            if (distanceToPlayer <= attackRange)
            {
                // ������� ������
                if (Time.time >= nextDamageTime && distanceToPlayer > runRange)
                {
                    LaunchProjectile();
                    nextDamageTime = Time.time + attackCooldown;
                }
                
            }
            else
            {
                // �������� � ������, ����� ���������� � �������� �����
                transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            }
        }
        else
        {
            isChasing = false; // ��������� ����� �������������

            // ���� ����� ������� ������, ������� �� ����
            
                // ���������� ��������������
                Patrol();
            
        }
        if (distanceToPlayer < runRange)
        {
            RunAwayFromPlayer();
        }
    }

    private void Patrol()
    {
        // ���� ��������� ������ ������� ����� ��������������, ������������� ��������� ����� � ���������� ������ ��������
        if (transform.position == patrolPoints[currentPatrolIndex].position)
        {
            currentPatrolIndex++;
            patrolTimer = 0f;

            // ���� �������� ��������� �����, ������������ � ������
            if (currentPatrolIndex >= patrolPoints.Length)
            {
                currentPatrolIndex = 0;
            }
        }

        // ������������ ���������� � ����������� ��������� ����� ��������������
        Vector3 direction = (patrolPoints[currentPatrolIndex].position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // ���� ������ �������� ������ ������� ��������, ������������ � ��������� ����� ��������������
        if (patrolTimer >= patrolWaitTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, patrolSpeed * Time.deltaTime);
        }
        else
        {
            patrolTimer += Time.deltaTime; // ����������� ������ ��������
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
        // ������� ��������� ������
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

        // ���������� ������ �� ������
        Vector3 direction = (player.position - transform.position).normalized;
        projectileRigidbody.velocity = direction * projectileSpeed;

        // �������� ��������� Collider � ������, ���� ��� ���
        Collider projectileCollider = projectile.GetComponent<Collider>();
        if (!projectileCollider)
        {
            projectileCollider = projectile.AddComponent<SphereCollider>();
        }

        // ��������� ����� ������ ��� ������������ � �������
        projectileCollider.isTrigger = true;
        //projectileCollider.gameObject.tag = "Projectile";

        // �������� ������, �������������� ������������ ������ � �������
        projectileCollider.gameObject.AddComponent<ProjectileCollision>();
    }

    private void RunAwayFromPlayer()
    {
        // ������������� �������, � ������� ����� �������
        runAwayPosition = transform.position + (transform.position - player.position).normalized;

        // ������� �� ������
        transform.position = Vector3.MoveTowards(transform.position, runAwayPosition, chaseSpeed * Time.deltaTime);

        // ������������ ���������� � ����������� �����, ���� �������
        Vector3 direction = (runAwayPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // ���������, ���� ����� ���� ������, ���������� �������
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            isRunningAway = false;
        }
    }
}