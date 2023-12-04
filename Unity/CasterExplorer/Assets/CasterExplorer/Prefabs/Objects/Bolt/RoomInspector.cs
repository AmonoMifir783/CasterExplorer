using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInspector : MonoBehaviour
{
   public Transform target; // Ссылка на объект, который будет перемещаться

    public float speed = 0.2f;
    public float targetHeight = 3f;
    private Vector3 initialPosition;
    private float startTime;
    public bool BatteryEnergy = false;
    public bool Key = false;    

    void Start() 
    { 
        initialPosition = target.position;
        startTime = Time.time; // Исправлено использование времени 
        // GetComponent - поиска компонента 
    } 

    public void OpenTheDoor()
    {
        int a = transform.GetChild(5).GetComponent<AI_Fire>().currentTemp;
        if (a > transform.GetChild(5).GetComponent<AI_Fire>().minTemp && a < transform.GetChild(5).GetComponent<AI_Fire>().maxTemp)
            {
                StartCoroutine(MoveTarget());
            } 
    }

    IEnumerator MoveTarget()
    {
        float elapsedTime = 0f;
        while (elapsedTime < targetHeight / speed)
        {
            float fractionOfJourney = elapsedTime / (targetHeight / speed);
            target.position = Vector3.Lerp(initialPosition, initialPosition + Vector3.up * targetHeight, fractionOfJourney);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    
}



