using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardSR : MonoBehaviour
{
    private Collider objectCollider;
    private SpellReaction spellReaction;
    private BardAI bardAi;
    private bool isBardAiDisabled = false;
    private float disableDuration = 5f;
    private float disableTimer = 0f;
    public int mobHealth = 200;
    public bool isElectric = false;

    public Animator animator;
    public bool isTakingDamage;
    public bool bardDead = false;

    public AudioClip[] damageSounds;
    public AudioClip[] electroSounds;
    public AudioSource audioSource;

    void Start()
    {
        objectCollider = GetComponent<Collider>();
        spellReaction = GetComponent<SpellReaction>();
        bardAi = GetComponent<BardAI>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Electric();
        //animator.SetBool("isDead", false);
        //animator.SetBool("isResting", false);
        //animator.SetBool("isAttacking", false);
        //animator.SetBool("isElectro", false);
        //animator.SetBool("isDamaging", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (spellReaction.Force > 0)
        {
            TakeDamage(Mathf.Sqrt(spellReaction.Force));
            Debug.Log("4");
        }
        if (spellReaction.Amperage > 20)
        {
            if (electroSounds.Length > 0 && !isElectric)
            {
                int randomIndex = Random.Range(0, electroSounds.Length);
                audioSource.PlayOneShot(electroSounds[randomIndex]);
            }
            isElectric = true;
            Debug.Log("electro");
        }
    }

    private void TakeDamage(float damageAmount)
    {


        animator.SetBool("isDead", false);
        animator.SetBool("isResting", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isElectro", false);
        animator.SetBool("isDamaging", true);

        if (damageSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, damageSounds.Length);
            audioSource.PlayOneShot(damageSounds[randomIndex]);
        }
        mobHealth -= (int)damageAmount;
        if (mobHealth <= 0)
        {
            bardDead = true;
            Destroy(gameObject);
        }
        Invoke("SetIsDamagingFalse", 0.1f);
    }
    private void Electric()
    {
        if (isElectric)
        {
            animator.SetBool("isResting", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isDead", false);
            animator.SetBool("isDamaging", false);
            animator.SetBool("isElectro", true);
            isBardAiDisabled = true;
            bardAi.enabled = false;
            disableTimer += Time.deltaTime;
            if (disableTimer >= disableDuration)
            {
                isElectric = false;
                isBardAiDisabled = false;
                disableTimer = 0f;
                bardAi.enabled = true;
            }
        }
        else
        {
            //animator.SetBool("isElectro", false);
            //animator.SetBool("isAttacking", false);
            //animator.SetBool("isDead", false);
            //animator.SetBool("isDamaging", false);
            //animator.SetBool("isResting", true);
            isTakingDamage = false;
            bardAi.enabled = true;
        }
    }
    private void SetIsDamagingFalse()
    {
        animator.SetBool("isDamaging", false);
        animator.SetBool("isResting", true);
    }
}