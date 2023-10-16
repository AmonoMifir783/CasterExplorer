using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingArmorSR : MonoBehaviour
{
    private Collider objectCollider;
    private SpellReaction spellReaction;
    private LivingArmorAI livingArmorAi;
    public int mobHealth = 300; 
    public int maxTemp = 1000;
    public int minTemp = 10;
    public int maxCurrent = 1000;
    public int minCurrent = 10;
    private int tempering;
    public int electroDamage;
    private bool isFirstSpellHit = false;
    private bool isTempering = false;
    public bool isElectric = false;
    public bool additionalDamage = false;
    public int T1;
    public int K1;

    private void Start()
    {
        objectCollider = GetComponent<Collider>();
        spellReaction = GetComponent<SpellReaction>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTempering)
        {
            // 

            if (spellReaction.Temperature > minTemp)
            {
                isFirstSpellHit = true;
                T1 = spellReaction.Temperature;
                Debug.Log("1");
            }

            //

            if (isFirstSpellHit && spellReaction.Temperature < 0)
            {
                tempering = 1 + ((T1 * 100) / maxTemp);
                Debug.Log("2");
                TakeDamage(Mathf.Sqrt(spellReaction.Force) * tempering);
                isTempering = true;
            }

            //

            if (spellReaction.Force > 0)
            {
                TakeDamage(Mathf.Sqrt(spellReaction.Force));
                Debug.Log("4");
            }

            //

            if (spellReaction.Amperage > minCurrent)
            {
                isElectric = true;
                Debug.Log("5");
            }
            
            //

        }
        else
        {
            if (isTempering)
            {
                TakeDamage(Mathf.Sqrt(spellReaction.Force) * tempering);
                Debug.Log("3");
            }

            //

            if (isTempering && spellReaction.Amperage > minCurrent)
            {
                isElectric = true;
                TakeDamage(Mathf.Sqrt(spellReaction.Force) * tempering);
                Debug.Log("6");
            }
        }

        //

        if (isElectric)
        {
            K1 = spellReaction.Amperage;
            electroDamage = 1 + ((K1 * 100) / maxCurrent);
            additionalDamage = true;
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