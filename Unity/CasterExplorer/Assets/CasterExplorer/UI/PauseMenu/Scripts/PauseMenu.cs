using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour

{
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    [SerializeField] private SaveSlotsPauseSave saveSlotsPauseSave;
    [SerializeField] private PauseMenu pauseMenu;
    public GameObject pauseMenuUI;

    bool isPaused = false;
    public GameObject Player;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Player.GetComponent<MouseLook>().enabled = true;
        Player.GetComponent<PlayerMovement>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        Player.GetComponent<MouseLook>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        
    }


    public void OnSaveGameClicked()
    {
        saveSlotsPauseSave.ActivateMenu(false);
        //this.DeactivateMenu();
    }



    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        //this.DeactivateMenu();
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        DisableButtonsDependingOnData();
    }

    private void DisableButtonsDependingOnData()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            //continueGameButton.interactable = false;
            //loadGameButton.interactable = false;
        }
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void Settings()
    {
        // ��� �������� ��������
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        isPaused = false;
    }


}