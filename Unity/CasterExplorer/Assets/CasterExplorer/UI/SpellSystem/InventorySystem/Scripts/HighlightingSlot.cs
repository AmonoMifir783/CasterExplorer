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
                Color currentColor = slot.Icon.GetComponent<Image>().color;
                slot.Icon.GetComponent<Image>().color =  new Color(currentColor.r, currentColor.b, currentColor.g, 1f);
                slot.isHighlighting = false;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (FastSlots.transform.GetChild(i).GetComponent<InventorySlots>().isHighlighting)
            {
                Color currentColor = FastSlots.transform.GetChild(i).GetComponent<InventorySlots>().Icon.GetComponent<Image>().color;
                FastSlots.transform.GetChild(i).GetComponent<InventorySlots>().Icon.GetComponent<Image>().color = new Color(currentColor.r, currentColor.b, currentColor.g, 1f);
                FastSlots.transform.GetChild(i).GetComponent<InventorySlots>().isHighlighting = false;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (CreateSlots.transform.GetChild(i).GetComponent<InventorySlots>().isHighlighting)
            {
                Color currentColor = CreateSlots.transform.GetChild(i).GetComponent<InventorySlots>().Icon.GetComponent<Image>().color;
                CreateSlots.transform.GetChild(i).GetComponent<InventorySlots>().Icon.GetComponent<Image>().color = new Color(currentColor.r, currentColor.b, currentColor.g, 1f);
                CreateSlots.transform.GetChild(i).GetComponent<InventorySlots>().isHighlighting = false;
            }
        }

    }
}
