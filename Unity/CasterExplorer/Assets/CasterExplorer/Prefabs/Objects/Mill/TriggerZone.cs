using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class TriggerZone : MonoBehaviour 
{ 
    AI_Mill aI_Mill;

    void Start()
    {
        aI_Mill = transform.GetChild(0).GetComponent<AI_Mill>();        
    }

    private void OnTriggerStay(Collider other) 
    { 
        
        if (other.CompareTag("Player") && !aI_Mill.isFreeze) 
        { 
            Transform childTransform = transform.GetChild(0).transform.GetChild(1);
             // Получение дочернего элемента с индексом 2 
            childTransform.Rotate(Vector3.forward, 1f); // Вращение дочернего элемента по оси Y на 10 градусов 
        } 
    } 
}