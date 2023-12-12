using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInspector : MonoBehaviour
{
   public Transform target; // Ссылка на объект, который будет перемещаться
    public GameObject lamp;
    public float speed = 0.2f;
    public float targetHeight = 3f;
    private Vector3 initialPosition;
    private float startTime;
    private Material material;

    void Start() 
    { 
        initialPosition = target.position;
        startTime = Time.time; // Исправлено использование времени 
        // GetComponent - поиска компонента 
        Renderer objectRenderer = lamp.GetComponent<Renderer>();
        material = objectRenderer.material;
    } 

    public void OpenTheDoor()
    {
        int a = transform.GetChild(5).GetComponent<AI_Fire>().currentTemp;
        if (a > transform.GetChild(5).GetComponent<AI_Fire>().minTemp && a < transform.GetChild(5).GetComponent<AI_Fire>().maxTemp)
            {
                Color baseColor = material.GetColor("_BaseColor");
                baseColor.g = 170f / 255f; // Присваиваем значение 200 в синий канал (диапазон от 0 до 1)
                material.SetColor("_BaseColor", baseColor);
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



