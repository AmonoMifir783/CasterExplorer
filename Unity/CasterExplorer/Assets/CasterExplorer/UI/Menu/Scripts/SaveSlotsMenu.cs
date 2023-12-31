using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    // Start is called before the first frame update
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;
    [Header("Confirmation Popup")]
    [SerializeField] private ConfirmationPopupMenu confirmationPopupMenu;

    public GameObject pauseMenuUI;



    private SaveSlot[] saveSlots;
    private bool isLoadingGame = false;
    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void onSaveSlotClicked(SaveSlot saveSlot)
    {
        //DisableMenuButtons();

        if (isLoadingGame)
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            SaveGameAndLoadScene();
        }
        else if (saveSlot.hasData)
        {
            confirmationPopupMenu.ActivateMenu(
                "Starting a New Game with this slot will override the currently saved data. Are you sure?",
                () =>
                {
                    DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
                    DataPersistenceManager.instance.NewGame();
                    SaveGameAndLoadScene();
                },
                () =>
                {
                    this.ActivateMenu(isLoadingGame);
                }
                );
        }
        else
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            DataPersistenceManager.instance.NewGame();
            SaveGameAndLoadScene();
        }
        Time.timeScale = 1f;
    }

    private void SaveGameAndLoadScene()
    {
        DataPersistenceManager.instance.SaveGame();

        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void OnClearClicked(SaveSlot saveSlot)
    {
       // DisableMenuButtons();
        confirmationPopupMenu.ActivateMenu(
            "Are you sure you want to delete this saved data?",
            () => {
                DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileId());
                ActivateMenu(isLoadingGame);
            },
            () => {
                ActivateMenu(isLoadingGame);
            }
        );
    }

    public void onBackClicked()
    {
        mainMenu.ActivateMenu();
        //pauseMenuUI.SetActive(true);
        this.DeactivateMenu();
    }

    public void onBackPauseClicked()
    {
        pauseMenu.ActivateMenu();
        pauseMenuUI.SetActive(true);
        this.DeactivateMenu();
        
    }


    public void ActivateMenu(bool isLoadingGame)
    {
        this.gameObject.SetActive(true);
        this.isLoadingGame = isLoadingGame;

        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllprofilesGameData();

        backButton.interactable = true;

        GameObject firstSelected = backButton.gameObject;
        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null && isLoadingGame)
            {
                saveSlot.gameObject.SetActive(false);   
            }
            else
            {
                saveSlot.gameObject.SetActive(true);
                if (firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }
            }
        }
        Button firstSelectedButton = firstSelected.GetComponent<Button>();
        this.SetFirstSelected(firstSelectedButton);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void DisableMenuButtons()
    {
        foreach (SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }

}
