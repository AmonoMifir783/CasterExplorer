using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InventorySlots : MonoBehaviour
{
    public ItemScriptableObject Item; // Публичная переменная для хранения объекта типа ItemScriptableObject
    public bool isEmpty = true; // Публичная переменная для определения, пустой ли слот
    public bool isHighlighting = false; // Публичная переменная для определения, выделен ли слот
    public GameObject Icon; // Публичная переменная для хранения объекта-иконки

    public void Start()
    {
        Icon = transform.GetChild(0).GetChild(0).gameObject; // Получаем объект-иконку, используя дочерний элемент
        Image iconImage = Icon.GetComponent<Image>(); // Получаем компонент Image на объекте-иконке
    
        if (!isEmpty)
        {
            iconImage.sprite = Item.Icon; // Устанавливаем иконку
            iconImage.color = Color.white; // Устанавливаем цвет иконки в белый цвет (полностью непрозрачный)
        }

    }

    public void SetIcon(Sprite _icon)
    {
        Image iconImage = Icon.GetComponent<Image>(); // Получаем компонент Image на объекте-иконке
        iconImage.color = Color.white; // Устанавливаем цвет иконки в белый цвет (полностью непрозрачный)
        iconImage.sprite = _icon; // Устанавливаем изображение иконки с заданным спрайтом (_icon)
    }
    
     
}