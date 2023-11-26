using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardAI : MonoBehaviour
{
    public Transform player;
    public float attackRange = 20f;
    public float attackInterval = 3f;
    public int baseDamage = 100;

    private BardSR bardSr;

    public AudioClip[] attackSounds;
    public AudioClip[] songSounds;
    public AudioSource audioSource;

    public bool singsong = false;
    public Animator animator;

    private float nextAttackTime = 0f;
    public bool isAttacking = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        bardSr = GetComponent<BardSR>();
    }
    private void Update()
    {
        //animator.SetBool("isDead", false);
        //animator.SetBool("isResting", false);
        //animator.SetBool("isAttacking", false);
        //animator.SetBool("isElectro", false);
        //animator.SetBool("isDamaging", false);
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within attack range
        if (distanceToPlayer <= attackRange)
        {
            // Check if enough time has passed since the last attack
            if (Time.time >= nextAttackTime)
            {
                // Attack the player
                float damage = (attackRange - distanceToPlayer) / attackRange;
                int finalDamage = Mathf.RoundToInt(baseDamage * damage);
                //int damage = distanceToPlayer < (attackRange / 2) ? increasedDamage : baseDamage;

                    AttackPlayer(finalDamage);
             
                // Set the next attack time
                nextAttackTime = Time.time + attackInterval;
            }
            else
            {
                
            }
        }
        //if (distanceToPlayer <= 30 && !singsong) 
        //{
        //    singsong = true;
        //    if (songSounds.Length > 0)
        //    {
        //        int randomIndex = Random.Range(0, songSounds.Length);
        //        audioSource.PlayOneShot(songSounds[randomIndex]);
        //    }
        //}
    }

    private void AttackPlayer(int finalDamage)
    {
        
            animator.SetBool("isResting", false);
            animator.SetBool("isDead", false);
            animator.SetBool("isElectro", false);
            animator.SetBool("isDamaging", false);
            animator.SetBool("isAttacking", true);
            if (attackSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, attackSounds.Length);
                audioSource.PlayOneShot(attackSounds[randomIndex]);
            }
            PlayerHealth playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            playerHealthScript.TakeDamage(finalDamage);
        
    }
}
