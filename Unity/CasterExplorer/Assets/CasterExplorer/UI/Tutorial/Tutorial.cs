using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Image image;

    public void Toggle()
    {
        image.gameObject.SetActive(!image.gameObject.activeSelf);
        
    }
}