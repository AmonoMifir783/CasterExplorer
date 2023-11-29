using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFastSlots : MonoBehaviour
{
    public InventorySlots inventorySlots;
    public MurrorFastSlots murrorFastSlots;
    // Start is called before the first frame update
    void Start()
    {
        inventorySlots = GetComponent<InventorySlots>();
        murrorFastSlots = FindAnyObjectByType<MurrorFastSlots>();
        murrorFastSlots.CloseInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
