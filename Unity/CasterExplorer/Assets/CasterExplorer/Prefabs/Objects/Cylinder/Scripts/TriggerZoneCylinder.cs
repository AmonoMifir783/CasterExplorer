using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
 
public class TriggerZoneCylinder : MonoBehaviour 
{ 
    AI_Freeze aI_Freeze;
    public float angle;

    void Start()
    {
        aI_Freeze = transform.GetChild(0).GetComponent<AI_Freeze>();        
    }

    private void OnTriggerStay(Collider other) 
    { 
        
        if (other.CompareTag("Player") && !aI_Freeze.isFreeze) 
        { 
            Transform childTransform = transform.GetChild(0);
             // Получение дочернего элемента с индексом 2 
            childTransform.Rotate(Vector3.forward, 0.5f); // Вращение дочернего элемента по оси Z на 1 градусов 
            angle = childTransform.localEulerAngles.y;
            Debug.Log("Угол поворота: " + angle);
        } 

        if (other.CompareTag("Player") && angle >= 300 && angle <= 360 && aI_Freeze.isFreeze)
        
        {
            Debug.Log("Power");
        }
    } 
} 

