using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Back : MonoBehaviour
{
    private Vector3 initialPosition;
    private GameObject objectToReset;

    
    private void Start()
    {
        initialPosition = transform.position; // Сохраняем изначальное положение объекта
        objectToReset = GameObject.Find("CylinderDegrees");
    }

    public void ResetObjectPosition()
    {
        objectToReset.transform.position = initialPosition; // Возвращаем объект в изначальное положение
    }
}