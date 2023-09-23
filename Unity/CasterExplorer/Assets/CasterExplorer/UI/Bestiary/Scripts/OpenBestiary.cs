using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    
    public bool PauseGame;
    public GameObject Book;
    public GameObject[] elementsToHide;

    private void Update()
    {
        // ����������/�������� ������� ���������� ��� ������� ������� "B"
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (PauseGame)
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
        Book.SetActive(true);

        // �������� ��������� ����� �����
        for (int i = 0; i < elementsToHide.Length; i++)
        {
            elementsToHide[i].SetActive(false);
        }
        Time.timeScale = 0f;
        PauseGame = true;
    }

    public void CloseImage()
    {
        // �������� ��������
        Book.SetActive(false);

        // ���������� ������� ����� �����
        for (int i = 0; i < elementsToHide.Length; i++)
        {
            elementsToHide[i].SetActive(true);
        }

        Time.timeScale = 1f;
        PauseGame = false;
        
    }
}
