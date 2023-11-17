using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Force : MonoBehaviour
{

    public int minForce = 15;
    public bool isForce = false;
    SpellReaction Link_SpellReaction; // (ссылка на класс SpellReaction)
   

    // Start is called before the first frame update
    void Start()
    {
        Link_SpellReaction = GetComponent<SpellReaction>(); // GetComponent - поиска компонента
    }



    private void OnCollisionEnter(Collision collision) 
    { 
        Debug.Log(Link_SpellReaction.Force);
        int Fo1 = Link_SpellReaction.Force;

        if (collision.gameObject.CompareTag("Spell")) 
        { 
            if (Fo1 <= minForce)
            {
                isForce = true;
                StartCoroutine(ResetForceState());
            }
        } 
    } 
   IEnumerator ResetForceState()
    {
        yield return new WaitForSeconds(5f);
        isForce = false;
    }
}


