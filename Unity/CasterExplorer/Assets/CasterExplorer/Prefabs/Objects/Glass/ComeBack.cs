using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComeBack : MonoBehaviour
{
    public Button button;
    public GameObject targetObject;
    public Collider targetCollider;

    private void Start()
    {
        // Добавляем обработчик события нажатия на кнопку
        button.onClick.AddListener(ActivateObject);
    }

    private void ActivateObject()
    {
        // Активируем объект и его триггер
        targetObject.SetActive(true);
        targetCollider.isTrigger = true;
    }
}