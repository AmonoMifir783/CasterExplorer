using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightingSlot : MonoBehaviour
{

    public GameObject FastSlots;

    public GameObject CreateSlots;


    public InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void HighlightSlot()
    {
        inventoryManager.Inicialization();
        foreach (InventorySlots slot in inventoryManager.Slots)
        {
            if (slot.isHighlighting)
            {   
                slot.Icon.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                slot.isHighlighting = false;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (FastSlots.transform.GetChild(i).GetComponent<InventorySlots>().isHighlighting)
            {
                FastSlots.transform.GetChild(i).GetComponent<InventorySlots>().Icon.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                FastSlots.transform.GetChild(i).GetComponent<InventorySlots>().isHighlighting = false;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (CreateSlots.transform.GetChild(i).GetComponent<InventorySlots>().isHighlighting)
            {
                CreateSlots.transform.GetChild(i).GetComponent<InventorySlots>().Icon.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                CreateSlots.transform.GetChild(i).GetComponent<InventorySlots>().isHighlighting = false;
            }
        }

    }
}
