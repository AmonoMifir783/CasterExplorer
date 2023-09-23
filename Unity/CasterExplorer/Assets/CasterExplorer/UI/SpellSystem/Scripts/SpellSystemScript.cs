using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpellSystemScript : MonoBehaviour
{
    public bool PauseGame;
    public GameObject SpellSystemMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!PauseGame)
            {
                InventoryOn();
            }
            else
            {
                InventoryOff();
            }
        }
    }

    public void InventoryOn()
    {
        SpellSystemMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseGame = true;

        Cursor.visible = true;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    public void InventoryOff()
    {
        SpellSystemMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;

        Cursor.visible = false;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = false;
    }
}
