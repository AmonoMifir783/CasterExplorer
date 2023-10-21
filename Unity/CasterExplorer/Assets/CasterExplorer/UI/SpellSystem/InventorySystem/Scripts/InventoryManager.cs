using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject FastSlot1;
    public GameObject FastSlot2;
    public GameObject FastSlot3;
    public GameObject FastSlot4;


    //FastSlot1.GetComponent<InventorySlots>().Item;


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
