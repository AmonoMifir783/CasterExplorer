using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour

{
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    [SerializeField] private SaveSlotsPauseSave saveSlotsPauseSave;
    [SerializeField] private PauseMenu pauseMenu;
    public SpellSystemScript spellSystemScript;
    public OpenBestiary openBestiary;
    public GameObject pauseMenuUI;
    public OptionsOpen optionsOpen;
    public ConfirmationPopupMenu confirmationPopupMenu;
    public MagisteriyaFruitPickUp magisteriyaFruitPickUp;
    public MagisteriyaFruitUse magisteriyaFruitUse;
    public ChestPickUp chestPickUp;
    public SalamdraAI salamdraAi;
    public Button saveGameButton;
    public Button loadGameButton;

    public bool isPaused = false;
    public GameObject Player;

    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    public PickUp pickUp;

    void Start()
    {
        pickUp = GetComponent<PickUp>();
        pauseMenuUI.SetActive(false);
        spellSystemScript = GetComponent<SpellSystemScript>();
        //saveSlotsPauseSave = GetComponent<SaveSlotsPauseSave>();
        //saveSlotsMenu = GetComponent<SaveSlotsMenu>();
        optionsOpen = GetComponent<OptionsOpen>();
        openBestiary = GetComponent<OpenBestiary>();
        magisteriyaFruitPickUp = GetComponent<MagisteriyaFruitPickUp>();
        magisteriyaFruitUse = GetComponent<MagisteriyaFruitUse>();
        chestPickUp = GetComponent<ChestPickUp>();
        salamdraAi = FindObjectOfType<SalamdraAI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (spellSystemScript.inventoryon)
            {
                spellSystemScript.InventoryOff();
                // pauseMenuUI.SetActive(false);
                Player.GetComponent<MouseLook>().enabled = true;
                Player.GetComponent<PlayerMovement>().enabled = true;
            }
            else if (openBestiary.isOpened)
            {
                openBestiary.CloseImage();
                // pauseMenuUI.SetActive(false);
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
        DisableButtonsDependingOnData();
    }

    public void Resume()
    {
        Player.GetComponent<MouseLook>().enabled = true;
        Player.GetComponent<PlayerMovement>().enabled = true;
        //Player.GetComponent<MagisteriyaFruitPickUp>().enabled = true;
        Player.GetComponent<PickUp>().enabled = true;
        Player.GetComponent<MagisteriyaFruitUse>().enabled = true;
        Player.GetComponent<AudioListener>().enabled = true;
        //Player.GetComponent<ChestPickUp>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        optionsOpen.OptionsMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        spellSystemScript.enabled = true;
        openBestiary.enabled = true;
    }

    void Pause()
    {
        Player.GetComponent<MouseLook>().enabled = false;
        Player.GetComponent<PlayerMovement>().enabled = false;
        //Player.GetComponent<MagisteriyaFruitPickUp>().enabled = false;
        Player.GetComponent<PickUp>().enabled = false;
        Player.GetComponent<MagisteriyaFruitUse>().enabled = false;
        Player.GetComponent<AudioListener>().enabled = false;
        //Player.GetComponent<ChestPickUp>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        spellSystemScript.InventoryOff();
        openBestiary.CloseImage();
        Cursor.visible = true;
        Time.timeScale = 0f;
        //if (Time.timeScale == 0f)
        //{
        //    Time.timeScale = 0f;
        //}
        isPaused = true;
        openBestiary.enabled = false;
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
        if (salamdraAi != null && salamdraAi.isChasing)
        {
            // Disable the save and load buttons
            saveGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
        if (salamdraAi != null && !salamdraAi.isChasing)
        {
            // Disable the save and load buttons
            saveGameButton.interactable = true;
            loadGameButton.interactable = true;
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
    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }

}