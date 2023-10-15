using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightingSlot : MonoBehaviour
{
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
    }
}
