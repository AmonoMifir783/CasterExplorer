using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour

{
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    [SerializeField] private SaveSlotsPauseSave saveSlotsPauseSave;
    [SerializeField] private PauseMenu pauseMenu;
    public SpellSystemScript spellSystemScript;
    public GameObject pauseMenuUI;
    public OptionsOpen optionsOpen;
    public ConfirmationPopupMenu confirmationPopupMenu;

    public bool isPaused = false;
    public GameObject Player;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        spellSystemScript = GetComponent<SpellSystemScript>();
        //saveSlotsPauseSave = GetComponent<SaveSlotsPauseSave>();
        //saveSlotsMenu = GetComponent<SaveSlotsMenu>();
        optionsOpen = GetComponent<OptionsOpen>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (spellSystemScript.inventoryon)
            {
                spellSystemScript.InventoryOff();
                //pauseMenuUI.SetActive(false);
                Player.GetComponent<MouseLook>().enabled = true;
                Player.GetComponent<PlayerMovement>().enabled = true;
            }
            else
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
    }

    public void Resume()
    {
        Player.GetComponent<MouseLook>().enabled = true;
        Player.GetComponent<PlayerMovement>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        optionsOpen.OptionsMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        spellSystemScript.enabled = true;
    }

    void Pause()
    {
        Player.GetComponent<MouseLook>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        spellSystemScript.InventoryOff();
        Cursor.visible = true;
        Time.timeScale = 0f;
        //if (Time.timeScale == 0f)
        //{
        //    Time.timeScale = 0f;
        //}
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