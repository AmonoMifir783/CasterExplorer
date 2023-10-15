using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellReaction : MonoBehaviour
{
    private Collider ObjectCollider;
    
    public int Temperature;
    public int Force;
    public int Amperage;
    public int Gravity;
    public int Light;
    
    void Start()
    {
        ObjectCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spell"))
        {
            Spell SpellObject = collision.gameObject.GetComponent<Spell>();

            Temperature = SpellObject.Temperature;
            Force = SpellObject.Force;
            Amperage = SpellObject.Amperage;
            Gravity = SpellObject.Gravity;
            Light = SpellObject.Light;
        }  
    }
}