using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform InventoryPanel;
    public List<InventorySlots> Slots = new List<InventorySlots>();

    public int i = 0;

    public void Inicialization()
    {
        while (i < InventoryPanel.childCount)
        {
            if (InventoryPanel.GetChild(i).GetComponent<InventorySlots>() != null)
            {
                Slots.Add(InventoryPanel.GetChild(i).GetComponent<InventorySlots>());
                i++;
            }
        }
    }
    

}
