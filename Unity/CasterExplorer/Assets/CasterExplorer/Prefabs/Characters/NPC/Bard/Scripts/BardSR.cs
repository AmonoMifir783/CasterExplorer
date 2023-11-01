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

    // Start is called before the first frame update
    void Start()
    {
        objectCollider = GetComponent<Collider>();
        spellReaction = GetComponent<SpellReaction>();
        bardAi = GetComponent<BardAI>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetComponent<BardAI>().enabled = true;
        if (isBardAiDisabled)
        {
            disableTimer += Time.deltaTime;
            if (disableTimer >= disableDuration)
            {
                GetComponent<BardAI>().enabled = false;
                isBardAiDisabled = false;
                disableTimer = 0f;
                bardAi.enabled = true; // Re-enable the BardAI script
            }
        }
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
            isElectric = true;
            isBardAiDisabled = true;
            bardAi.enabled = false; // Disable the BardAI script
        }
    }

    private void TakeDamage(float damageAmount)
    {
        mobHealth -= (int)damageAmount; // Convert float to int and subtract damage from mob's health
        if (mobHealth <= 0)
        {
            Destroy(gameObject); // Destroy the mob if health reaches 0 or below
        }
    }
}