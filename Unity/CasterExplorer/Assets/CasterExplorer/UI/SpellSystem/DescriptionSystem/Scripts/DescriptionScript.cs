using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionScript : MonoBehaviour
{
    public Image IconDescription;
    public TextMeshProUGUI TextDescription;

    public void Description(InventorySlots slot)
    {
        if (slot.Item != null)
        {
            IconDescription.sprite = slot.Item.Icon;
            TextDescription.text = slot.Item.Temperature + "\n" + 
                                    slot.Item.Force + "\n" + 
                                    slot.Item.Amperage + "\n" + 
                                    slot.Item.Gravity + "\n" + 
                                    slot.Item.Light;
        }
    }
}