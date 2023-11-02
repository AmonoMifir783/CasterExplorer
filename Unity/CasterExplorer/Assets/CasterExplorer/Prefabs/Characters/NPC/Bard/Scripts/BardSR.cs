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

    void Start()
    {
        objectCollider = GetComponent<Collider>();
        spellReaction = GetComponent<SpellReaction>();
        bardAi = GetComponent<BardAI>();
    }

    private void Update()
    {
        if (isElectric)
        {
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
        if (!isElectric)
        {
            bardAi.enabled = true;
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
        }
    }

    private void TakeDamage(float damageAmount)
    {
        mobHealth -= (int)damageAmount;
        if (mobHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}