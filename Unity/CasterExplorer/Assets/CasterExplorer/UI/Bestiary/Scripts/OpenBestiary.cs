//using UnityEngine;
//using UnityEngine.UI;

//public class OpenBestiary : MonoBehaviour
//{

//    public bool PauseGame;
//    public GameObject Book;
//    public GameObject[] elementsToHide;
//    public GameObject playerCamera;
//    public GameObject Player;
//    public GameObject FrontUI;
//    public bool isOpened = false;
//    public SpellSystemScript spellSystemScript;
//    public AudioSource audioSource; // Reference to the AudioSource component
//    public AudioClip[] pageSounds;


//    private void Start()
//    {
//        spellSystemScript = GetComponent<SpellSystemScript>();
//    }
//    private void Update()
//    {
//        // ����������/�������� ������� ���������� ��� ������� ������� "B"
//        if (Input.GetKeyDown(KeyCode.B))
//        {
//            if (PauseGame)
//            {
//                playerCamera.GetComponent<MouseLook>().enabled = true;
//                playerCamera.GetComponent<PlayerMovement>().enabled = true;
//                Player.GetComponent<MagisteriyaFruitPickUp>().enabled = true;
//                Player.GetComponent<MagisteriyaFruitUse>().enabled = true;
//                Player.GetComponent<ChestPickUp>().enabled = true;
//                Cursor.lockState = CursorLockMode.Locked;
//                CloseImage();
//                if (pageSounds.Length > 0)
//                {
//                    int randomIndex = Random.Range(0, pageSounds.Length);
//                    audioSource.PlayOneShot(pageSounds[randomIndex]);
//                }
//            }
//            else
//            {
//                playerCamera.GetComponent<MouseLook>().enabled = false;
//                playerCamera.GetComponent<PlayerMovement>().enabled = false;
//                Player.GetComponent<MagisteriyaFruitPickUp>().enabled = false;
//                Player.GetComponent<MagisteriyaFruitUse>().enabled = false;
//                Player.GetComponent<ChestPickUp>().enabled = false;
//                Cursor.lockState = CursorLockMode.None;
//                OpenImage();
//            }
//        }
//    }

//    public void OpenImage()
//    {
//        // ���������� ��������
//        Book.SetActive(true);

//        // �������� ��������� ����� �����
//        for (int i = 0; i < elementsToHide.Length; i++)
//        {
//            elementsToHide[i].SetActive(false);
//        }

//        Time.timeScale = 0f;
//        PauseGame = true;
//        Cursor.visible = true;
//        isOpened = true;
//        spellSystemScript.enabled = false;
//        FrontUI.SetActive(false);
//        if (pageSounds.Length > 0)
//        {
//            int randomIndex = Random.Range(0, pageSounds.Length);
//            audioSource.PlayOneShot(pageSounds[randomIndex]);
//        }
//    }

//    public void CloseImage()
//    {
//        // �������� ��������
//        Book.SetActive(false);

//        // ���������� ������� ����� �����
//        for (int i = 0; i < elementsToHide.Length; i++)
//        {
//            elementsToHide[i].SetActive(true);
//        }

//        Time.timeScale = 1f;
//        PauseGame = false;
//        Cursor.visible = false;
//        isOpened = false;
//        spellSystemScript.enabled = true;
//        FrontUI.SetActive(true);
//    }
//}

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
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip[] pageSounds;
    public Image[] bookImages;
    private int currentImageIndex = 0;

    private void Start()
    {
        spellSystemScript = GetComponent<SpellSystemScript>();
    }

    private void Update()
    {
        // Open/Close the book when pressing "B"
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (PauseGame)
            {
                playerCamera.GetComponent<MouseLook>().enabled = true;
                playerCamera.GetComponent<PlayerMovement>().enabled = true;
               // Player.GetComponent<MagisteriyaFruitPickUp>().enabled = true;
                Player.GetComponent<PickUp>().enabled = true;
                Player.GetComponent<MagisteriyaFruitUse>().enabled = true;
                //Player.GetComponent<ChestPickUp>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                CloseImage();
            }
            else
            {
                playerCamera.GetComponent<MouseLook>().enabled = false;
                playerCamera.GetComponent<PlayerMovement>().enabled = false;
                //Player.GetComponent<MagisteriyaFruitPickUp>().enabled = false;
                Player.GetComponent<MagisteriyaFruitUse>().enabled = false;
                //Player.GetComponent<ChestPickUp>().enabled = false;
                Player.GetComponent<PickUp>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
                OpenImage();
            }
        }

        // Change the book image using arrow keys
        if (PauseGame && isOpened)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ShowPreviousImage();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ShowNextImage();
            }
        }
    }

    public void OpenImage()
    {
        // Activate the book
        Book.SetActive(true);

        // Hide other elements
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

        if (pageSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, pageSounds.Length);
            audioSource.PlayOneShot(pageSounds[randomIndex]);
        }

        // Show the initial book image
        ShowImageAtIndex(currentImageIndex);
    }

    public void CloseImage()
    {
        // Deactivate the book
        Book.SetActive(false);

        // Show other elements
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

    public void ShowNextImage()
    {
        currentImageIndex++;
        if (currentImageIndex >= bookImages.Length)
        {
            currentImageIndex = 0;
        }
        ShowImageAtIndex(currentImageIndex);
        if (pageSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, pageSounds.Length);
            audioSource.PlayOneShot(pageSounds[randomIndex]);
        }
    }

    public void ShowPreviousImage()
    {
        currentImageIndex--;
        if (currentImageIndex < 0)
        {
            currentImageIndex = bookImages.Length - 1;
        }
        ShowImageAtIndex(currentImageIndex);
        if (pageSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, pageSounds.Length);
            audioSource.PlayOneShot(pageSounds[randomIndex]);
        }
    }

    private void ShowImageAtIndex(int index)
    {
        for (int i = 0; i < bookImages.Length; i++)
        {
            if (i == index)
            {
                bookImages[i].gameObject.SetActive(true);
            }
            else
            {
                bookImages[i].gameObject.SetActive(false);
            }
        }
    }
}