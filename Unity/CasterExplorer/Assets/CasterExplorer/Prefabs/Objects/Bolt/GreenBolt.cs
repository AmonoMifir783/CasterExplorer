using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBolt : MonoBehaviour
{
    public int minForce = 15;
    public GameObject rotatingObject;
    
    Manager manager;
    private float rotationSpeed = -30f; // Скорость вращения с измененным знаком для против часовой стрелки
    private float targetRotation = -180f; // Целевой угол поворота с измененным знаком для против часовой стрелки
    private float currentRotation = 0f; // Текущий угол поворота
    SpellReaction Link_SpellReaction; // (ссылка на класс SpellReaction)



    void Start()
    {
        Link_SpellReaction = GetComponent<SpellReaction>(); // GetComponent - поиск компонента
        manager =  transform.parent.transform.parent.GetComponent<Manager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        int Fo1 = Link_SpellReaction.Force;
        if (collision.gameObject.CompareTag("Spell") && transform.parent.GetComponent<AI_Bolt>().isGreenFlag)
        {
            if (Fo1 >= minForce)
            {
                StartCoroutine(RotateObject());
                StartCoroutine( MoveTargetUp());
                
            }
        }
    }

    IEnumerator RotateObject()
    {
        Debug.Log("Зелёный болт!");
       
        currentRotation = 0f;
        while (currentRotation > targetRotation) // Изменено условие на currentRotation > targetRotation для против часовой стрелки
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            rotatingObject.transform.Rotate(Vector3.forward, rotationAmount);
            currentRotation += rotationAmount;
            yield return null;
        }
       
        transform.parent.GetComponent<AI_Bolt>().isGreenFlag = false;
        transform.parent.GetComponent<AI_Bolt>().isRedFlag = true;
        manager.VentilAnalitics(); 

    }

    
    IEnumerator MoveTargetUp()
    {
   
    float elapsedTime = 0f;
    Vector3 startPosition = rotatingObject.transform.position; // Запоминаем текущую позицию перед началом движения
    float targetHeight = 1.2f; // Высота, на которую нужно поднять объект
    float speed = 0.2f; // Скорость движения вверх

    while (elapsedTime < targetHeight / speed)
    {
        float fractionOfJourney = elapsedTime / (targetHeight / speed);
        Vector3 targetPosition = startPosition + Vector3.up * targetHeight; // Поднимаем объект вверх, меняя знак на "+"
        rotatingObject.transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    
    }
}