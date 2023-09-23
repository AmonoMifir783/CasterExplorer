using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    
    public bool PauseGame;
    public GameObject Book;
    public GameObject[] elementsToHide;
    public GameObject playerCamera;

    private void Update()
    {
        // ����������/�������� ������� ���������� ��� ������� ������� "B"
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (PauseGame)
            {
                playerCamera.GetComponent<MouseLook>().enabled = true;
                playerCamera.GetComponent<PlayerMovement>().enabled = true;
                CloseImage();
            }
            else
            {
                playerCamera.GetComponent<MouseLook>().enabled = false;
                playerCamera.GetComponent<PlayerMovement>().enabled = false;
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
