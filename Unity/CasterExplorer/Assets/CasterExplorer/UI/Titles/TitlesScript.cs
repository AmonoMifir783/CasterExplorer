using UnityEngine;
using UnityEngine.UI;

public class TitlesScript : MonoBehaviour
{
    public Image image;

    public void Toggle()
    {
        image.gameObject.SetActive(!image.gameObject.activeSelf);

    }
}