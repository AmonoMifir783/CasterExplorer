using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowNumber : MonoBehaviour, IDataPersistence
{
    // Start is called before the first frame update
    public TextMeshPro textMeshPro;
    public bool numberShow = false;
    private SaperId saperId;
    // ���������� ��� ��������� ������� � ���������
    void Start()
    {
        saperId = GetComponent<SaperId>();  
    }
    private void OnTriggerEnter(Collider other)
    {
        // ���������, �������� �� ������, �������� � ���������, �������
        if (other.CompareTag("Player"))
        {
            // ������ ������� �������
            textMeshPro.enabled = true;
            numberShow = true;
        }
    }
    public void LoadData(GameData data)
    {
        if (saperId != null && data.saperNumbers.ContainsKey(saperId.id) && data.saperNumbers[saperId.id] == numberShow)
        {
            textMeshPro.enabled = true;
        }
    }

    public void SaveData(GameData data)
    {
       
    }
}
