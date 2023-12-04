/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool allVentilsRed;
    public int count = 1;
    private RedBolt [] redArray;

    void Start()
    {
        redArray = GetComponentsInChildren.GetComponentsInChildren<RedBolt>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckVentils();
        }
    }

    void CheckVentils()
    {
        allVentilsRed = true;
        foreach (AI_Bolt red in redArray)
        {
            for (int n = 0; n < count; n++)
            {   if (transform.GetChild(n).transform.GetChild(0).GetComponent<RedBolt>().isRedFlag == true)
                {
                    allVentilsRed = false;
                    дверь поднимается
                    break;
                }
            }
        }
        if (allVentilsRed)
        {
            Debug.Log("Выход");
            дверь опускается
        }
    }
}*/