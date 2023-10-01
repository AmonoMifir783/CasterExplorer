using UnityEngine;

public class MagisteriyaFruitPickUp : MonoBehaviour
{
     // Количество здоровья, добавляемое при использовании предмета
    public int inventoryCount = 0; // Количество предметов в инвентаре
    


    private void PickUpItem()
    {
        inventoryCount++; // Увеличиваем количество предметов в инвентаре при подборе
        Debug.Log("Item picked up! Current inventory count: " + inventoryCount);

        // Здесь можно добавить код для отображения количества предметов в инвентаре на экране.
    }

   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
           // Destroy(gameObject);
        }
        
    }
}