using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellReaction : MonoBehaviour
{
   
    
    public int Temperature;
    public int Force;
    public int Amperage;
    public int Gravity;
    public int Light;
    
    

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spell"))
        {
            Spell SpellObject = collision.gameObject.GetComponent<Spell>();

            Temperature = SpellObject.Temperature;
            Force = SpellObject.Force;
            Amperage = SpellObject.Amperage;
            Gravity = SpellObject.Gravity;
            Light = SpellObject.Light;
            //Debug.Log("2");
        }  
    }
}