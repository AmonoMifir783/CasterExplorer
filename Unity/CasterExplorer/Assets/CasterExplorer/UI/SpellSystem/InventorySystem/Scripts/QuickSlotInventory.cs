using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickslotInventory : MonoBehaviour
{
    // Объект, у которого дети являются слотами
    public Transform quickslotParent;
    public int currentQuickslotID = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;
    public ShootSpell ShootSpell;
    public bool isShooted = false;

    public int Temperature;
    public int Force;
    public int Amperage;
    public int Gravity;
    public int Light;

    private void Start()
    {
        ShootSpell = FindObjectOfType<ShootSpell>();
        //for (int i = 0; i < 4; i++)
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1 + i))
        //    {
        //        isShooted = true;
        //    }
        //}
    }

    void Update()
    {
        // Используем цифры
        for (int i = 0; i < 4; i++)
        {
            // Если мы нажимаем на клавиши 1 по 4, то...
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                isShooted = true;
                quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                currentQuickslotID = i;
                quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;

                if (quickslotParent.GetChild(currentQuickslotID).gameObject.GetComponent<InventorySlots>().Item != null)
                {
                    Temperature = quickslotParent.GetChild(currentQuickslotID).gameObject.GetComponent<InventorySlots>().Item.Temperature;
                    Force = quickslotParent.GetChild(currentQuickslotID).gameObject.GetComponent<InventorySlots>().Item.Force;
                    Amperage = quickslotParent.GetChild(currentQuickslotID).gameObject.GetComponent<InventorySlots>().Item.Amperage;
                    Gravity = quickslotParent.GetChild(currentQuickslotID).gameObject.GetComponent<InventorySlots>().Item.Gravity;
                    Light = quickslotParent.GetChild(currentQuickslotID).gameObject.GetComponent<InventorySlots>().Item.Light;
                }
            }
        }

        // Используем предмет по нажатию на левую кнопку мыши
        if (Input.GetKeyDown(KeyCode.Mouse0) && isShooted)
        {
            if (Time.timeScale == 1)
            {
                if (quickslotParent.GetChild(currentQuickslotID).gameObject.GetComponent<InventorySlots>().Item != null)
                {
                    ShootSpell.Shoot(Temperature, Force, Amperage, Gravity, Light);
                }
            }
        }
    }
}
