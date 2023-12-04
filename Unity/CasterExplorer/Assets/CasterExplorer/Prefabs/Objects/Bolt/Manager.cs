using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Transform target; // Ссылка на объект, который будет перемещаться
    public GameObject lamp;
    public float speed = 0.2f;
    public float targetHeight = 3f;
    private Vector3 initialPosition;
    private float startTime;
    public bool[] check;
    int areEqual = 0;
    private Material material;

    void Start()
    {
        initialPosition = target.position;
        startTime = Time.time; // Исправлено использование времени
        Renderer objectRenderer = lamp.GetComponent<Renderer>();
        material = objectRenderer.material;
    }

    public void VentilAnalitics()
    {
        for (int i = 0; i < check.Length; i++)
        {
            if (transform.GetChild(i).GetComponent<AI_Bolt>().isRedFlag == check[i])
            {
                areEqual++;
                Debug.Log(areEqual);
                Debug.Log(check.Length);
                Debug.Log("Same");
            }
        }
        if (areEqual == check.Length)
        {
            Color baseColor = material.GetColor("_BaseColor");
            baseColor.g = 170f / 255f; // Присваиваем значение 200 в синий канал (диапазон от 0 до 1)
            material.SetColor("_BaseColor", baseColor);
            StartCoroutine(MoveTarget());
        }
        areEqual = 0;
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