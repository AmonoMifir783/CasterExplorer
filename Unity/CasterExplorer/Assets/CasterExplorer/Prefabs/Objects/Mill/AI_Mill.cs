using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Mill : MonoBehaviour
{

    public int minTemp = -5;
    public bool isFreeze = false;
    SpellReaction Link_SpellReaction; // (ссылка на класс SpellReaction)
    public GameObject TriggerZone; // (отображает триггерную зону)

    // Start is called before the first frame update
    void Start()
    {
        Link_SpellReaction = GetComponent<SpellReaction>(); // GetComponent - поиска компонента
    }



    private void OnCollisionEnter(Collision collision) 
    { 
        Debug.Log(Link_SpellReaction.Temperature);
        int T1 = Link_SpellReaction.Temperature;

        if (collision.gameObject.CompareTag("Spell")) 
        { 
            if (T1 <= minTemp)
            {
                isFreeze = true;
                StartCoroutine(ResetFreezeState());
            }
        } 
    } 
   IEnumerator ResetFreezeState()
    {
        yield return new WaitForSeconds(5f);
        isFreeze = false;
    }
}