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
    public bool isChasing; // ���� ������������� ������
    private bool isRunningAway; // ���� �������� �� ������
    private Vector3 runAwayPosition; // �������, � ������� ����� �������
    private SpellReaction spellReaction;
    public AudioClip[] seePlayerSounds;
    public AudioClip[] spitSounds;
    public AudioClip[] fearSounds;
    public AudioSource audioSource;
    private bool hasPlayedSeePlayerSound = false;
    private bool hasPlayedFearSound = false;
    public Animator animator;
    public SalamandraSR salamandraSr;
    public bool isAttacking;
    public bool isRunning;
    //public GameObject ToxiinsSalamandra;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // ������� ������ �� ����
        currentPatrolIndex = 0; // ������������� ��������� ������ ��������������
        patrolTimer = 0f; // �������� ������ ��������
        isChasing = false; // �������������� ���� �������������
        isRunningAway = false; // �������������� ���� ��������
        spellReaction = GetComponent<SpellReaction>();
        animator = GetComponentInChildren<Animator>();
        salamandraSr = GetComponent<SalamandraSR>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // ���������� �� ������

        // ��������� ���������� ������ � ������� ������ ����������
        if (distanceToPlayer <= chaseRange)
        {
            if (!salamandraSr.isTakingDamage && !isAttacking)
            {
                animator.SetBool("isResting", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isWalking", false);
                animator.SetBool("isDamaging", false);
                animator.SetBool("isRunning", true);
            }
            isChasing = true; // �������� ����� �������������
            RotateTowardsPlayer();
            // ���������, ���� ����� ��������� � �������� �����
            if (distanceToPlayer <= attackRange)
            {
                isRunning = false;
                // ������� ������
                if (Time.time >= nextDamageTime && distanceToPlayer > runRange)
                {
                    if (!salamandraSr.isTakingDamage && !isRunning && isAttacking)
                    {
                        animator.SetBool("isResting", false);
                        animator.SetBool("isRunning", false);
                        animator.SetBool("isDamaging", false);
                        animator.SetBool("isWalking", false);
                        animator.SetBool("isAttacking", true);
                    }
                    LaunchProjectile();
                    nextDamageTime = Time.time + attackCooldown;
                    hasPlayedFearSound = false;
                }

            }
            else
            {
                isAttacking = false;
                // �������� � ������, ����� ���������� � �������� �����
                transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
            }
        }
        else
        {
            //animator.SetBool("isResting", false);
            //animator.SetBool("isAttacking", false);
            //animator.SetBool("isDamaging", false);
            //animator.SetBool("isRunning", false);
            //animator.SetBool("isWalking", true);
            isChasing = false; // ��������� ����� �������������
            hasPlayedSeePlayerSound = false;
            isRunning = false;

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
        animator.SetBool("isResting", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDamaging", false);
        animator.SetBool("isWalking", true);
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
        isRunning = true;
        if  (!salamandraSr.isTakingDamage && !isAttacking)
        {
            animator.SetBool("isResting", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isDamaging", false);
            animator.SetBool("isRunning", true);
        }
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        if (seePlayerSounds.Length > 0 && audioSource != null && !hasPlayedSeePlayerSound)
        {
            int randomIndex = Random.Range(0, seePlayerSounds.Length);
            audioSource.PlayOneShot(seePlayerSounds[randomIndex]);
            hasPlayedSeePlayerSound = true; // ������������� ���� � true, ����� ���� �� ������������ �����
        }
    }

    private void LaunchProjectile()
    {
        isAttacking = true;
        //if (!salamandraSr.isTakingDamage && !isRunning)
        //{
        //    animator.SetBool("isResting", false);
        //    animator.SetBool("isRunning", false);
        //    animator.SetBool("isDamaging", false);
        //    animator.SetBool("isWalking", false);
        //    animator.SetBool("isAttacking", true);
        //}
        if (spitSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, spitSounds.Length);
            audioSource.PlayOneShot(spitSounds[randomIndex]);
        }

        // ������� ��������� ������
        GameObject projectile = Instantiate(projectilePrefab, transform.position + Vector3.up * 0.75f, Quaternion.identity);
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

        // �������� ������, �������������� ������������ ������ � �������
        projectileCollider.gameObject.AddComponent<ProjectileCollision>();
    }

    private void RunAwayFromPlayer()
    {
        animator.SetBool("isResting", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDamaging", false);
        animator.SetBool("isWalking", true);
        if (fearSounds.Length > 0 && audioSource != null && !hasPlayedFearSound)
        {
            int randomIndex = Random.Range(0, fearSounds.Length);
            audioSource.PlayOneShot(fearSounds[randomIndex]);
            hasPlayedFearSound = true;
        }

        // ������������� �������, � ������� ����� �������
        runAwayPosition = transform.position + (transform.position - player.position).normalized;

        // ������� �� ������
        transform.position = Vector3.MoveTowards(transform.position, runAwayPosition, chaseSpeed * Time.deltaTime);

        // ������������ ���������� � ����������� �����, ���� �������
        Vector3 direction = (runAwayPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-direction.x, 0, -direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // ���������, ���� ����� ���� ������, ���������� �������
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            isRunningAway = false;
        }
    }
}