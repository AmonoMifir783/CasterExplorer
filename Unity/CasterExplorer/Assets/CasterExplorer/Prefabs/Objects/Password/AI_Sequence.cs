using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Sequence : MonoBehaviour
{
    SpellReaction Link_SpellReaction;
    public int minForce = 20;
    public bool Press = false;
    RightSequence rightSequence;
    public int number = 0;

    void Start()
    {
        Link_SpellReaction = GetComponent<SpellReaction>();
        rightSequence = transform.parent.GetComponent<RightSequence>();
    }

    private void OnCollisionEnter(Collision collision) 
    { 
        int F1 = Link_SpellReaction.Force;

        //Debug.Log(F1);

        if (collision.gameObject.CompareTag("Spell")) 
        { 
            if (F1 >= minForce)
            {
                Press = true;
                StartCoroutine(ResetFreezeState());
                
                rightSequence.SequencwAnalitics(number);
                
            }
        } 
    } 

   IEnumerator ResetFreezeState()
    {
        yield return new WaitForSeconds(10f);
        Press = false;
    }

}
