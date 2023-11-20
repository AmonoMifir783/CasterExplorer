using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Fire : MonoBehaviour
{

    public int minTemp = -5;
    public int maxTemp = 15;
    //public bool isFire = false;
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
                Debug.Log("Цепь замкнулась. Объект поднялся");
            }
            else if (F1 >= maxTemp)
            {
                Debug.Log("Цепь замкнулась. Объект опустился");
            }
            else
            {
            Debug.Log("Ничего");
            }
        }
    }

   
}


