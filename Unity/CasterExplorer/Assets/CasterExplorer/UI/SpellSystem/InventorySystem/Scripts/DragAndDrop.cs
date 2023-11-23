using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InventorySlots oldSlot; // Ссылка на старый слот инвентаря
    [SerializeField] private Transform player; // Ссылка на игрока
    [SerializeField] private Color normalColor = Color.white; // Цвет по умолчанию
    [SerializeField] private Color hoverColor = new Color(1f, 1f, 1f, 0.5f); // Цвет при наведении
    private Image image; // Компонент изображения
    private InventorySlots newSlot; // Ссылка на новый слот инвентаря
    private HighlightingSlot highlightingSlot; // Ссылка на скрипт HighlightingSlot
    public GameObject DragSpell;
    public DescriptionScript Description;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Находим игрока по тегу
        oldSlot = transform.GetComponentInParent<InventorySlots>(); // Получаем ссылку на родительский слот инвентаря
        image = GetComponentInChildren<Image>(); // Получаем компонент изображения
        highlightingSlot = FindObjectOfType<HighlightingSlot>(); // Находим скрипт HighlightingSlot 
        Description = FindObjectOfType<DescriptionScript>(); // Находим скрипт DescriptionScript 
        DragSpell = GameObject.Find("DragSpell");
    }

    public void OnDrag(PointerEventData eventData) // При перетаскивании
    {
        if (oldSlot.isEmpty)
            return;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += eventData.delta; // Перемещаем предмет вместе с мышью
    }

    public void OnPointerDown(PointerEventData eventData) // При зажатии
    {
        if (oldSlot.isEmpty)
            return;

        image.color = new Color(1f, 1f, 1f, 0.4f); // Изменяем цвет при нажатии
        transform.SetParent(DragSpell.transform); // Изменяем родительский объект для возможности перемещения по экрану
        image.raycastTarget = false;
        oldSlot.isHighlighting = true;
    }

    public void OnPointerUp(PointerEventData eventData) // При отпускании
    {
        if (oldSlot.isEmpty)
            return;
        transform.SetParent(oldSlot.transform); // Возвращаем предмет на старый слот
        transform.position = oldSlot.transform.position; // Устанавливаем позицию предмета на позицию слота
        highlightingSlot.HighlightSlot();
        oldSlot.Icon.GetComponent<Image>().color = hoverColor;
        oldSlot.isHighlighting = true;

        if (eventData.pointerCurrentRaycast.gameObject.name == "SpellSystem")
        {

        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.TryGetComponent<InventorySlots>(out newSlot))
        {
            if (!newSlot.isEmpty)
            {
                oldSlot.isHighlighting = true;
            }
            else
            {
                ExchangeSlotData(newSlot);
                highlightingSlot.HighlightSlot();
                newSlot.Icon.GetComponent<Image>().color = hoverColor;
                oldSlot.isHighlighting = true;
            }
        }

        Description.Description(oldSlot);
        image.raycastTarget = true;
    }
    public void OnPointerEnter(PointerEventData eventData) // При наведении
    {
        if (oldSlot.isEmpty)
            return;
            image.color = hoverColor; // Изменяем цвет при наведении
    }

    public void OnPointerExit(PointerEventData eventData) // При денаведении
    {
        if (oldSlot.isEmpty)
            return;
        if (!oldSlot.isHighlighting)
        {
            image.color = normalColor; // Возвращаем цвет по умолчанию
        }
    }

    private void NullifySlotData()
    {
        oldSlot.Item = null; // Обнуляем предмет в старом слоте
        oldSlot.isEmpty = true; // Устанавливаем флаг пустоты
        oldSlot.Icon.GetComponent<Image>().color = Color.clear; // Скрываем иконку
        oldSlot.Icon.GetComponent<Image>().sprite = null; // Устанавливаем пустой спрайт
    }

    private void ExchangeSlotData(InventorySlots newSlot)
    {
        ItemScriptableObject item = newSlot.Item; // Сохраняем предмет нового слота
        bool isEmpty = newSlot.isEmpty; // Сохраняем флаг пустоты нового слота
        GameObject icon = newSlot.Icon; // Сохраняем иконку нового слота
        newSlot.Item = oldSlot.Item; // Заменяем предмет нового слота на предмет старого слота

        if (oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot.Icon.GetComponent<Image>().sprite); // Устанавливаем иконку нового слота из иконки старого слота
        }
        else
        {
            newSlot.Icon.GetComponent<Image>().color = Color.clear; // Скрываем иконку нового слота
            newSlot.Icon.GetComponent<Image>().sprite = null; // Устанавливаем пустой спрайт для иконки нового слота
        }

        newSlot.isEmpty = oldSlot.isEmpty; // Заменяем флаг пустоты нового слота на флаг пустоты старого слота
        oldSlot.Item = item; // Заменяем предмет старого слота на предмет нового слота

        if (isEmpty == false)
        {
            oldSlot.SetIcon(icon.GetComponent<Image>().sprite); // Устанавливаем иконку старого слота из иконки нового слота
        }
        else
        {
            oldSlot.Icon.GetComponent<Image>().color = Color.clear; // Скрываем иконку старого слота
            //oldSlot.Icon.GetComponent<Image>().sprite = null; // Устанавливаем пустую спрайт для иконки старого слота
        }

        oldSlot.isEmpty = isEmpty; // Заменяем флаг пустоты старого слота на флаг пустоты нового слота
    }
}