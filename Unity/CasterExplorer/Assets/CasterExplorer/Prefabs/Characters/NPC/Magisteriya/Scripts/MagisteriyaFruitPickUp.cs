using UnityEngine;

public class MagisteriyaFruitPickUp : MonoBehaviour
{
     // ���������� ��������, ����������� ��� ������������� ��������
    public int inventoryCount = 0; // ���������� ��������� � ���������
    


    private void PickUpItem()
    {
        inventoryCount++; // ����������� ���������� ��������� � ��������� ��� �������
        Debug.Log("Item picked up! Current inventory count: " + inventoryCount);

        // ����� ����� �������� ��� ��� ����������� ���������� ��������� � ��������� �� ������.
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