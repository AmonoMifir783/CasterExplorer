using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeGlass : MonoBehaviour
{
    private bool isActive = true;
    private bool isTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && isTrigger && other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Debug.Log("Yep!");
        }
    }
 
   
}