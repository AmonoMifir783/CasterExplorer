using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject image;
    public GameObject[] elementsToHide;

    private bool isImageOpen = false;

    private void Start()
    {
        // Скрываем элемент интерфейса при запуске игры
        image.SetActive(false);
    }

    private void Update()
    {
        // Отображаем/скрываем элемент интерфейса при нажатии клавиши "B"
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isImageOpen)
            {
                CloseImage();
            }
            else
            {
                OpenImage();
            }
        }
    }

    public void OpenImage()
    {
        // Отображаем картинку
        image.SetActive(true);

        // Скрываем остальные части сцены
        for (int i = 0; i < elementsToHide.Length; i++)
        {
            elementsToHide[i].SetActive(false);
        }

        isImageOpen = true;
    }

    public void CloseImage()
    {
        // Скрываем картинку
        image.SetActive(false);

        // Отображаем скрытые части сцены
        for (int i = 0; i < elementsToHide.Length; i++)
        {
            elementsToHide[i].SetActive(true);
        }

        isImageOpen = false;
    }
}
