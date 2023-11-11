using UnityEngine;
using UnityEngine.UI;

public class OpenBestiary : MonoBehaviour
{
    
    public bool PauseGame;
    public GameObject Book;
    public GameObject[] elementsToHide;
    public GameObject playerCamera;
    public GameObject Player;
    public GameObject FrontUI;
    public bool isOpened = false;
    public SpellSystemScript spellSystemScript;


    private void Start()
    {
        spellSystemScript = GetComponent<SpellSystemScript>();
    }
    private void Update()
    {
        // ����������/�������� ������� ���������� ��� ������� ������� "B"
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (PauseGame)
            {
                playerCamera.GetComponent<MouseLook>().enabled = true;
                playerCamera.GetComponent<PlayerMovement>().enabled = true;
                Player.GetComponent<MagisteriyaFruitPickUp>().enabled = true;
                Player.GetComponent<MagisteriyaFruitUse>().enabled = true;
                Player.GetComponent<ChestPickUp>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                CloseImage();
            }
            else
            {
                playerCamera.GetComponent<MouseLook>().enabled = false;
                playerCamera.GetComponent<PlayerMovement>().enabled = false;
                Player.GetComponent<MagisteriyaFruitPickUp>().enabled = false;
                Player.GetComponent<MagisteriyaFruitUse>().enabled = false;
                Player.GetComponent<ChestPickUp>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
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
        Cursor.visible = true;
        isOpened = true;
        spellSystemScript.enabled = false;
        FrontUI.SetActive(false);
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
        Cursor.visible = false;
        isOpened = false;
        spellSystemScript.enabled = true;
        FrontUI.SetActive(true);
    }
}
