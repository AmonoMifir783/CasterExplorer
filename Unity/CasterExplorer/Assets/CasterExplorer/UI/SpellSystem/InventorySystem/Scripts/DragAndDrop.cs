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
    [SerializeField] private Color ClearColor = new Color(1f, 1f, 1f, 0f); // Цвет пустой ячейки
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
            
        Color currentColor = image.color;
        image.color = new Color(currentColor.r, currentColor.b, currentColor.g, 0.4f); // Изменяем цвет при зажатии
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

        Color currentColor = oldSlot.Icon.GetComponent<Image>().color;
        oldSlot.Icon.GetComponent<Image>().color = new Color(currentColor.r, currentColor.b, currentColor.g, 0.5f);// hoverColor;
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
                ExchangeSlotData(newSlot, oldSlot);
                highlightingSlot.HighlightSlot();

                currentColor = newSlot.Icon.GetComponent<Image>().color;
                newSlot.Icon.GetComponent<Image>().color =  new Color(currentColor.r, currentColor.b, currentColor.g, 0.5f);// hoverColor;      
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

        Color currentColor = image.color;
        image.color = new Color(currentColor.r, currentColor.b, currentColor.g, 0.5f);// hoverColor; // Изменяем цвет при наведении
    }

    public void OnPointerExit(PointerEventData eventData) // При денаведении
    {
        if (oldSlot.isEmpty)
            return;
        if (!oldSlot.isHighlighting)
        {
            Color currentColor = image.color;
            image.color = new Color(currentColor.r, currentColor.b, currentColor.g, 1f); //normalColor; // Возвращаем цвет по умолчанию
        }
    }

    private void NullifySlotData()
    {
        oldSlot.Item = null; // Обнуляем предмет в старом слоте
        oldSlot.isEmpty = true; // Устанавливаем флаг пустоты
        oldSlot.Icon.GetComponent<Image>().color = Color.clear; // Скрываем иконку
        oldSlot.Icon.GetComponent<Image>().sprite = null; // Устанавливаем пустой спрайт
    }

    private void ExchangeSlotData(InventorySlots newslot, InventorySlots oldslot)
    {
        ItemScriptableObject item = newslot.Item; // Сохраняем предмет нового слота
        bool isEmpty = newslot.isEmpty; // Сохраняем флаг пустоты нового слота
        //GameObject icon = newslot.Icon; // Сохраняем иконку нового слота
        newslot.Item = oldslot.Item; // Заменяем предмет нового слота на предмет старого слота

        if (oldslot.isEmpty == false)
        {
            newSlot.SetIcon(oldSlot.Icon.GetComponent<Image>().sprite); // Устанавливаем иконку нового слота из иконки старого слота
            newslot.Icon.GetComponent<Image>().color = oldslot.Icon.GetComponent<Image>().color;
        }
        else
        {
            
            newslot.Icon.GetComponent<Image>().color = ClearColor; //Color.clear; // Скрываем иконку нового слота
            newslot.Icon.GetComponent<Image>().sprite = null; // Устанавливаем пустой спрайт для иконки нового слота
        }

        newslot.isEmpty = oldslot.isEmpty; // Заменяем флаг пустоты нового слота на флаг пустоты старого слота
        oldslot.Item = item; // Заменяем предмет старого слота на предмет нового слота

        if (isEmpty == false)
        {
           oldslot.SetIcon(newslot.Icon.GetComponent<Image>().sprite); // Устанавливаем иконку старого слота из иконки нового слота
        }
        else
        {
            oldslot.Icon.GetComponent<Image>().color = ClearColor; //Color.clear; // Скрываем иконку старого слота
            //oldSlot.Icon.GetComponent<Image>().sprite = null; // Устанавливаем пустую спрайт для иконки старого слота

            // Создание прозрачного спрайта
            Texture2D texture = Texture2D.blackTexture;
            Sprite transparentSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // Установка прозрачного спрайта
            oldslot.Icon.GetComponent<Image>().sprite = transparentSprite;
        }
        oldslot.isEmpty = isEmpty; // Заменяем флаг пустоты старого слота на флаг пустоты нового слота
    }
}