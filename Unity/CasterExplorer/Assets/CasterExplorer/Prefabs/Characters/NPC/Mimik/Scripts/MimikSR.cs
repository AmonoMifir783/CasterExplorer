using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimikSR : MonoBehaviour
{
    private Collider objectCollider;
    private SpellReaction spellReaction;
    private MimkAI mimikAi;
    public int mobHealth = 300;
    public bool isBurning = false;
    public int maxTemp = 1000000;
    public int Kt;
    public int Kopen = 1;
    public float Kclose = 0.05f;
    // Start is called before the first frame update
    private void Start()
    {
        objectCollider = GetComponent<Collider>();
        spellReaction = GetComponent<SpellReaction>();
        mimikAi = GetComponent<MimkAI>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (spellReaction.Force > 0 && spellReaction.Temperature < 20 && !mimikAi.isOpen)
        {
            TakeDamage(Mathf.Sqrt(spellReaction.Force) * Kclose);
            Debug.Log("CloseForce: " + Mathf.Sqrt(spellReaction.Force) * Kclose);
        }
        if (spellReaction.Force > 0 && spellReaction.Temperature > 20 && !mimikAi.isOpen)
        {
            Kt = 1 + (spellReaction.Temperature / maxTemp);
            TakeDamage(Mathf.Sqrt(spellReaction.Force) * Kt * Kclose);
            Debug.Log("CloseBurning: " + Mathf.Sqrt(spellReaction.Force) * Kt * Kclose);
        }
        if (spellReaction.Force > 0 && spellReaction.Temperature < 20 && mimikAi.isOpen)
        {
            TakeDamage(Mathf.Sqrt(spellReaction.Force) * Kopen);
            Debug.Log("OpenForce: " + Mathf.Sqrt(spellReaction.Force) * Kopen);
        }
        if (spellReaction.Force > 0 && spellReaction.Temperature > 20 && mimikAi.isOpen)
        {
            Kt = 1 + (spellReaction.Temperature / maxTemp);
            TakeDamage(Mathf.Sqrt(spellReaction.Force) * Kt * Kopen);
            Debug.Log("OpenBurning: " + Mathf.Sqrt(spellReaction.Force) * Kt * Kopen);
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
