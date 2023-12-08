using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamandraSR : MonoBehaviour
{
    private Collider objectCollider;
    private SpellReaction spellReaction;
    private SalamdraAI salamandraAi;
    private ProjectileCollision projectileCollision;
    public int mobHealth = 150;
    public int maxTemp = 1000000;
    public int minTemp = -273;
    public int maxCurrent = 10000;
    public int minCurrent = 10;
    public int FireDamage;
    public int additionalFireDamage;
    public bool isBurning = false;
    public bool isFrozen = false;
    public bool additionalDamage = false;
    private bool isDamaged = false;
    public int T1;
    public int K1;
    public int k;
    public AudioClip[] damageSounds;
    public AudioClip[] deathSounds;
    public AudioSource audioSource;
    public bool salamandraDead = false;
    public Animator animator;
    public bool isTakingDamage;
    // Start is called before the first frame update
    private void Start()
    {
        objectCollider = GetComponent<Collider>();
        spellReaction = GetComponent<SpellReaction>();
        salamandraAi = GetComponent<SalamdraAI>();
        projectileCollision = GetComponent<ProjectileCollision>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (spellReaction.Temperature < 0)
        {
            k = 1 + (spellReaction.Temperature / minTemp);
            TakeDamage(Mathf.Sqrt(spellReaction.Force) * k);
            Debug.Log("FrozenDamage:" + Mathf.Sqrt(spellReaction.Force) * k);
        }


        if (spellReaction.Force > 0)
        {
            TakeDamage(Mathf.Sqrt(spellReaction.Force));
            //Debug.Log("2");
        }
        //�� ���� ������ 500
        if (spellReaction.Temperature > 20)
        {
            isBurning = true;
            TakeDamage(Mathf.Sqrt(spellReaction.Force));
            //Debug.Log("3");
        }


        if (isBurning)
        {
            K1 = spellReaction.Temperature;
            additionalFireDamage = 1 + (K1 / maxTemp);
            FireDamage = 20 * additionalFireDamage;
            //PlayerHealth playerHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
            //playerHealthScript.TakeDamage(FireDamage);
            Debug.Log("Fire Damage:" + FireDamage);
        }
    }


    // Update is called once per frame
    private void TakeDamage(float damageAmount)
    {
        isTakingDamage = true;
        if (isTakingDamage)
        {
            isTakingDamage = false;
            animator.SetBool("isRunning", false);
            animator.SetBool("isResting", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isDamaging", true);
        }
        if (damageSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, damageSounds.Length);
            audioSource.PlayOneShot(damageSounds[randomIndex]);
        }
        mobHealth -= (int)damageAmount; // Convert float to int and subtract damage from mob's health

        if (mobHealth <= 0)
        {
            salamandraAi.isChasing = false;
            salamandraDead = true;
            if (deathSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, deathSounds.Length);
                audioSource.PlayOneShot(deathSounds[randomIndex]);
            }
            Destroy(gameObject); // Destroy the mob if health reaches 0 or below
        }
    }
}
