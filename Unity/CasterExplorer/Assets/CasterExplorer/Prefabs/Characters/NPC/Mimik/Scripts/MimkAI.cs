using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimkAI : MonoBehaviour
{
    public Transform player;
    public float detectionDistance = 3f;
    public float attackDistance = 15f;
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;

    public bool isOpen = false;
    private bool isAttacking = false;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isOpen && distanceToPlayer <= detectionDistance)
        {
            Open();
        }
        else if (isOpen && distanceToPlayer > attackDistance)
        {
            Close();
        }

        if (isAttacking)
        {
            Attack();
            RotateTowardsPlayer();
        }
    }

    public void Open()
    {
        isOpen = true;
        isAttacking = true;
        // Добавьте здесь код для открытия визуального состояния моба
    }
    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public void Close()
    {
        isOpen = false;
        isAttacking = false;
        // Добавьте здесь код для закрытия визуального состояния моба
    }

    void Attack()
    {
        //transform.LookAt(player);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        // Добавьте здесь код для атаки игрока
    }
}
