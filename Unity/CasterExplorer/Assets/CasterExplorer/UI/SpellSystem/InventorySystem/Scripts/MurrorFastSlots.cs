using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MurrorFastSlots : MonoBehaviour
{

    public GameObject InventorySlot1;
    public GameObject InventorySlot2;
    public GameObject InventorySlot3;
    public GameObject InventorySlot4;

    public GameObject FastSlot1;
    public GameObject FastSlot2;
    public GameObject FastSlot3;
    public GameObject FastSlot4;

    public void CloseInventory()
    {
        FastSlot1.GetComponent<InventorySlots>().Item = InventorySlot1.GetComponent<InventorySlots>().Item;
        FastSlot1.GetComponent<InventorySlots>().isEmpty = InventorySlot1.GetComponent<InventorySlots>().isEmpty;
        FastSlot1.GetComponent<InventorySlots>().SetIcon(InventorySlot1.GetComponent<InventorySlots>().Icon.GetComponent<Image>().sprite);
        FastSlot1.GetComponent<InventorySlots>().Icon.GetComponent<Image>().color = InventorySlot1.GetComponent<InventorySlots>().Icon.GetComponent<Image>().color;

        FastSlot2.GetComponent<InventorySlots>().Item = InventorySlot2.GetComponent<InventorySlots>().Item;
        FastSlot2.GetComponent<InventorySlots>().isEmpty = InventorySlot2.GetComponent<InventorySlots>().isEmpty;
        FastSlot2.GetComponent<InventorySlots>().SetIcon(InventorySlot2.GetComponent<InventorySlots>().Icon.GetComponent<Image>().sprite);
        FastSlot2.GetComponent<InventorySlots>().Icon.GetComponent<Image>().color = InventorySlot2.GetComponent<InventorySlots>().Icon.GetComponent<Image>().color;

        FastSlot3.GetComponent<InventorySlots>().Item = InventorySlot3.GetComponent<InventorySlots>().Item;
        FastSlot3.GetComponent<InventorySlots>().isEmpty = InventorySlot3.GetComponent<InventorySlots>().isEmpty;
        FastSlot3.GetComponent<InventorySlots>().SetIcon(InventorySlot3.GetComponent<InventorySlots>().Icon.GetComponent<Image>().sprite);
        FastSlot3.GetComponent<InventorySlots>().Icon.GetComponent<Image>().color = InventorySlot3.GetComponent<InventorySlots>().Icon.GetComponent<Image>().color;

        FastSlot4.GetComponent<InventorySlots>().Item = InventorySlot4.GetComponent<InventorySlots>().Item;
        FastSlot4.GetComponent<InventorySlots>().isEmpty = InventorySlot4.GetComponent<InventorySlots>().isEmpty;
        FastSlot4.GetComponent<InventorySlots>().SetIcon(InventorySlot4.GetComponent<InventorySlots>().Icon.GetComponent<Image>().sprite);
        FastSlot4.GetComponent<InventorySlots>().Icon.GetComponent<Image>().color = InventorySlot4.GetComponent<InventorySlots>().Icon.GetComponent<Image>().color;
    }
}
