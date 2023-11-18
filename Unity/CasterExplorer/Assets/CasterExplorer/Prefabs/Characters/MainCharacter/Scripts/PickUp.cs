using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUp : MonoBehaviour, IDataPersistence
{
    public float pickupRange = 2f; // ��������� �������
    public LayerMask pickupLayer; // ���� ��������, ������� ����� ���������
    public int scrollCount = 5;
    public TextMeshProUGUI ScrollCount;
    public int inventoryCount = 0;
    public TextMeshProUGUI MagisteriyaCount;

    // Update ���������� ������ ����
    void Update()
    {
        // ���������, ������ �� ������ ������� (��������, "E")
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ������� ���, ������������ �� ������� ������ ������
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            // ���������, ���������� �� ��� � ��������, ������� ����� ���������
            if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
            {
                // �������� ������ �� ��������� �������, ������� ����� ���������
                PickChest pickableChest = hit.collider.GetComponent<PickChest>();
                PickMagisteriya pickableFruit = hit.collider.GetComponent<PickMagisteriya>();

                // ���������, ���� �� ��������� PickableObject
                if (pickableChest != null)
                {
                    // �������� ����� ������� ��������
                    pickableChest.PickUpItem();
                }
                if (pickableFruit != null)
                {
                    // �������� ����� ������� ��������
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