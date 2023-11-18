using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUp : MonoBehaviour, IDataPersistence
{
    public float pickupRange = 2f; // Дальность подбора
    public LayerMask pickupLayer; // Слой объектов, которые можно подбирать
    public int scrollCount = 5;
    public TextMeshProUGUI ScrollCount;
    public int inventoryCount = 0;
    public TextMeshProUGUI MagisteriyaCount;

    // Update вызывается каждый кадр
    void Update()
    {
        // Проверяем, нажата ли кнопка подбора (например, "E")
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Создаем луч, направленный от позиции игрока вперед
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            // Проверяем, столкнулся ли луч с объектом, который можно подобрать
            if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
            {
                // Получаем ссылку на компонент объекта, который можно подобрать
                PickChest pickableChest = hit.collider.GetComponent<PickChest>();
                PickMagisteriya pickableFruit = hit.collider.GetComponent<PickMagisteriya>();

                // Проверяем, есть ли компонент PickableObject
                if (pickableChest != null)
                {
                    // Вызываем метод подбора предмета
                    pickableChest.PickUpItem();
                }
                if (pickableFruit != null)
                {
                    // Вызываем метод подбора предмета
                    pickableFruit.PickUpItem();
                    Debug.Log("12312");
                }
            }
        }
    }
    public void LoadData(GameData data)
    {
        this.scrollCount = data.scrollCount;
        this.ScrollCount.text = data.ScrollCount;
        this.inventoryCount = data.inventoryCount;
        this.MagisteriyaCount.text = data.MagisteriyaCount;
    }

    public void SaveData(GameData data)
    {
        data.scrollCount = this.scrollCount;
        data.ScrollCount = this.ScrollCount.text;
        data.inventoryCount = this.inventoryCount;
        data.MagisteriyaCount = this.MagisteriyaCount.text;
    }
}