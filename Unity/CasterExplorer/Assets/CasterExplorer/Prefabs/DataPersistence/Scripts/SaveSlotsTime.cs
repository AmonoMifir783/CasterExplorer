using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveSlotsTime : MonoBehaviour
{
    public GameObject display;
    public int hour;
    public int minute;
    public int second;
    public string currentTime;
    // Start is called before the first frame update
    public void Start()
    {
        //currentTime = System.DateTime.Now.ToString("HH:mm:ss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Clock()
    {
        hour = System.DateTime.Now.Hour;
        minute = System.DateTime.Now.Minute;
        second = System.DateTime.Now.Second;
        display.GetComponent<TextMeshPro>().text = hour + ":" + minute + ":" + second;
    }
}
