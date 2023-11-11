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
    public AudioClip[] openSounds;
    public AudioClip[] closeSounds;
    public AudioSource audioSource;

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
        // Add code here to open the visual state of the mob

        if (openSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, openSounds.Length);
            audioSource.PlayOneShot(openSounds[randomIndex]);
        }
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
        // Add code here to close the visual state of the mob

        if (closeSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, closeSounds.Length);
            audioSource.PlayOneShot(closeSounds[randomIndex]);
        }
    }

    void Attack()
    {
        //transform.LookAt(player);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        // Add code here to attack the player
    }
}