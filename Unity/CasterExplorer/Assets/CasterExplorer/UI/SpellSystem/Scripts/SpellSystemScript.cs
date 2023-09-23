using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpellSystemScript : MonoBehaviour
{
    public bool PauseGame;
    public GameObject SpellSystemMenu;
    public GameObject playerCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!PauseGame)
            {
                playerCamera.GetComponent<MouseLook>().enabled = false;
                playerCamera.GetComponent<PlayerMovement>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                InventoryOn();
            }
            else
            {
                playerCamera.GetComponent<MouseLook>().enabled = true;
                playerCamera.GetComponent<PlayerMovement>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
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
