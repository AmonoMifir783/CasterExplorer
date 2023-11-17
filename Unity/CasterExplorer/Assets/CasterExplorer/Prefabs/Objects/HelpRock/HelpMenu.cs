using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    public GameObject helpMenu;
    private bool isHelpMenuVisible;
    public Button button;
    
    void Start()
    {
        button.onClick.AddListener(NewTry);
    }

    void NewTry()
    {
        Debug.Log("Pain");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider.CompareTag("Help"))
                {
                    isHelpMenuVisible = !isHelpMenuVisible;
                    helpMenu.SetActive(isHelpMenuVisible);

                    if (isHelpMenuVisible)
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        GameObject playerObject = GameObject.Find("Player");
                        playerObject.GetComponent<PlayerMovement>().enabled = false;
                        playerObject.GetComponent<MouseLook>().enabled = false;
                        
                    }
                    else
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        GameObject playerObject = GameObject.Find("Player");
                        playerObject.GetComponent<PlayerMovement>().enabled = true;
                        playerObject.GetComponent<MouseLook>().enabled = true;
                        
                    }
                }
            }
        }
    }
}