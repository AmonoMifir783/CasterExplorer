using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Fire : MonoBehaviour
{

    public int minTemp = 15;
    public bool isFire = false;
    SpellReaction Link_SpellReaction; // (ссылка на класс SpellReaction)
   

    // Start is called before the first frame update
    void Start()
    {
        Link_SpellReaction = GetComponent<SpellReaction>(); // GetComponent - поиска компонента
    }



    private void OnCollisionEnter(Collision collision) 
    { 
        Debug.Log(Link_SpellReaction.Temperature);
        int F1 = Link_SpellReaction.Temperature;

        if (collision.gameObject.CompareTag("Spell")) 
        { 
            if (F1 <= minTemp)
            {
                isFire = true;
                StartCoroutine(ResetFireState());
            }
        } 
    } 
   IEnumerator ResetFireState()
    {
        yield return new WaitForSeconds(5f);
        isFire = false;
    }
}


