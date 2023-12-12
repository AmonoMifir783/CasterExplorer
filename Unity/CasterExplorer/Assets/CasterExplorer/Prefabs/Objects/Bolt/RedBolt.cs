using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBolt : MonoBehaviour
{
    public int minForce = 15;
    public GameObject rotatingObject;
    public GameObject lamp;

    private Material material;

    Manager manager;

    private float rotationSpeed = 30f; // Скорость вращения
    private float targetRotation = 180f; // Целевой угол поворота
    private float currentRotation = 0f; // Текущий угол поворота

    SpellReaction Link_SpellReaction; // (ссылка на класс SpellReaction)

    void Start()
    {
        Link_SpellReaction = GetComponent<SpellReaction>(); // GetComponent - поиск компонента
        manager = transform.parent.transform.parent.GetComponent<Manager>();
        Renderer objectRenderer = lamp.GetComponent<Renderer>();
        material = objectRenderer.material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int Fo1 = Link_SpellReaction.Force;
        if (collision.gameObject.CompareTag("Spell") && transform.parent.GetComponent<AI_Bolt>().isRedFlag)
        {
            if (Fo1 >= minForce)
            {
                StartCoroutine(RotateObject());
                StartCoroutine(MoveTargetDown());
            }
        }
    }

    IEnumerator RotateObject()
    {

        Debug.Log("Красный!");

        currentRotation = 0f;
        while (currentRotation < targetRotation)
        {

            float rotationAmount = rotationSpeed * Time.deltaTime;
            //Debug.Log(rotationAmount);
            rotatingObject.transform.Rotate(Vector3.forward, rotationAmount);
            currentRotation += rotationAmount;
            yield return null;
        }

        transform.parent.GetComponent<AI_Bolt>().isGreenFlag = true;
        transform.parent.GetComponent<AI_Bolt>().isRedFlag = false;
        manager.VentilAnalitics();

        Color baseColor = material.GetColor("_BaseColor");
        baseColor.g = 170f / 255f; // Присваиваем значение 200 в синий канал (диапазон от 0 до 1)
        material.SetColor("_BaseColor", baseColor);

    }

    IEnumerator MoveTargetDown()
    {

        float elapsedTime = 0f;
        Vector3 startPosition = rotatingObject.transform.position; // Запоминаем текущую позицию перед началом движения
        float targetHeight = 1.2f; // Высота, на которую нужно опустить объект
        float speed = 0.2f; // Скорость движения вниз

        while (elapsedTime < targetHeight / speed)
        {
            float fractionOfJourney = elapsedTime / (targetHeight / speed);
            Vector3 targetPosition = startPosition - Vector3.up * targetHeight;
            rotatingObject.transform.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);
            elapsedTime += Time.deltaTime;
            yield return null;
        }


    }
}