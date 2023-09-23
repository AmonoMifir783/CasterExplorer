using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject image;
    public GameObject[] elementsToHide;

    private bool isImageOpen = false;

    private void Start()
    {
        // �������� ������� ���������� ��� ������� ����
        image.SetActive(false);
    }

    private void Update()
    {
        // ����������/�������� ������� ���������� ��� ������� ������� "B"
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
        // ���������� ��������
        image.SetActive(true);

        // �������� ��������� ����� �����
        for (int i = 0; i < elementsToHide.Length; i++)
        {
            elementsToHide[i].SetActive(false);
        }

        isImageOpen = true;
    }

    public void CloseImage()
    {
        // �������� ��������
        image.SetActive(false);

        // ���������� ������� ����� �����
        for (int i = 0; i < elementsToHide.Length; i++)
        {
            elementsToHide[i].SetActive(true);
        }

        isImageOpen = false;
    }
}
