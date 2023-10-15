using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float SpellLife = 3;
    public GameObject ExplosionEffect;
    private Collider spellCollider;
    bool checkOnCollisionEnter = false;


    public int Temperature;
    public int Force;
    public int Amperage;
    public int Gravity;
    public int Light;


    void Start()
    {
        spellCollider = GetComponent<Collider>();
    }

    private void Awake()
    {
        if (!checkOnCollisionEnter)
        {
            Destroy(gameObject, SpellLife);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (!collision.gameObject.CompareTag("Spell"))
            {
                GameObject explosion = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
                spellCollider.enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                
                var emission = transform.GetChild(0).GetComponent<ParticleSystem>().emission;
                emission.rateOverTime = new ParticleSystem.MinMaxCurve(0);

                Destroy(explosion, 1.5f);
                Destroy(gameObject, 1.5f);

                checkOnCollisionEnter = true;
            }
        }
    }
}