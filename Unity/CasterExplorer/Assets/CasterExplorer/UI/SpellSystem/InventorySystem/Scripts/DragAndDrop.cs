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
    private bool isHighlightedPointerEnter = false; // Флаг подсветки при наведении
    private bool isHighlightedOnPointerDown = false; // Флаг подсветки при нажатии
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
        transform.SetParent(DragSpell.transform); // Изменяем родительский объект для возможности перемещения по экрану  SpellSystem.transform transform.parent.parent
        image.raycastTarget = false;
    }

    public void OnPointerUp(PointerEventData eventData) // При отпускании
    {
        if (oldSlot.isEmpty)
            return;

        //image.color = normalColor; // Возвращаем цвет по умолчанию
        transform.SetParent(oldSlot.transform); // Возвращаем предмет на старый слот
        transform.position = oldSlot.transform.position; // Устанавливаем позицию предмета на позицию слота

        if (eventData.pointerCurrentRaycast.gameObject.name == "UIPanel")
        {
            // Добавьте код для взаимодействия с панелью пользовательского интерфейса
            highlightingSlot.HighlightSlot();
        }
        else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.TryGetComponent<InventorySlots>(out newSlot))
        {
            Debug.Log(newSlot);

            if (!newSlot.isEmpty)
            {
                highlightingSlot.HighlightSlot();
                oldSlot.Icon.GetComponent<Image>().color = hoverColor; 
                isHighlightedOnPointerDown = true;
                oldSlot.isHighlighting = true;
                Debug.Log("непуст");
                
            }
            else
            {
                ExchangeSlotData(newSlot); // Вызываем метод обмена данными с новым пустым слотом 
                highlightingSlot.HighlightSlot();
                Debug.Log("пуст");
            }
        }

        Description.Description(oldSlot);
        image.raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData) // При наведении
    {
        if (oldSlot.isEmpty)
            return;

        if (!isHighlightedPointerEnter)
        {
            image.color = hoverColor; // Изменяем цвет при наведении
            isHighlightedPointerEnter = true; // Устанавливаем флаг подсветки
        }
    }

    public void OnPointerExit(PointerEventData eventData) // При денаведении
    {
        if (oldSlot.isEmpty)
            return;

        if (!isHighlightedOnPointerDown)
        {
            if (isHighlightedPointerEnter)
            {
                image.color = normalColor; // Возвращаем цвет по умолчанию
                isHighlightedPointerEnter = false; // Сбрасываем флаг подсветки
            }
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
            oldSlot.Icon.GetComponent<Image>().sprite = null; // Устанавливаем пустую спрайт для иконки старого слота
        }
        oldSlot.isEmpty = isEmpty; // Заменяем флаг пустоты старого слота на флаг пустоты нового слота
    }

    private void SwapSlots(InventorySlots slot1, InventorySlots slot2)
    {
        ItemScriptableObject item = slot1.Item;
        bool isEmpty = slot1.isEmpty;
        GameObject icon = slot1.Icon;
        slot1.Item = slot2.Item;
        slot1.SetIcon(slot2.Icon.GetComponent<Image>().sprite);
        slot1.isEmpty = slot2.isEmpty;
        slot2.Item = item;
        slot2.SetIcon(icon.GetComponent<Image>().sprite);
        slot2.isEmpty = isEmpty;
    }
}