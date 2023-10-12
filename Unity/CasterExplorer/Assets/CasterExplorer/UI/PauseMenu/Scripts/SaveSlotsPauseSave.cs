using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsPauseSave : Menu

{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [Header("Menu Buttons")]
    [SerializeField] private Button backButton;
    [Header("Confirmation Popup")]
    [SerializeField] private ConfirmationPopupMenu confirmationPopupMenu;
    private SaveSlot[] saveSlots;
    private bool isSavingGame = false;

    private void Awake()
    {
        saveSlots = GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotPauseSaveClicked(SaveSlot saveSlot)
    {
        if (isSavingGame)
        {
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            SaveGameAndLoadScene();
        }

        else if (saveSlot.hasData)
        {
            confirmationPopupMenu.ActivateMenu(
                "Saving game in this slot will override the currently saved data. Are you sure?",
                () =>
                {
                    //DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
                    SaveGameAndLoadScene();
                },
                () =>
                {
                    ActivateMenu(isSavingGame);
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

    public void OnClearClicked(SaveSlot saveSlot)
    {
        confirmationPopupMenu.ActivateMenu(
            "Are you sure you want to delete this saved data?",
            () =>
            {
                DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileId());
                ActivateMenu(isSavingGame);
            },
            () =>
            {
                ActivateMenu(isSavingGame);
            }
        );
    }

    public void OnBackClicked()
    {
        if (isSavingGame)
        {
            pauseMenu.ActivateMenu();
        }
        else
        {
            mainMenu.ActivateMenu();
        }
        DeactivateMenu();
    }

    public void ActivateMenu(bool isSaving)
    {
        gameObject.SetActive(true);
        isSavingGame = isSaving;
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllprofilesGameData();
        backButton.interactable = true;
        GameObject firstSelected = backButton.gameObject;

        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);

            if (profileData == null && isSaving)
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
        SetFirstSelected(firstSelectedButton);
    }

    public void DeactivateMenu()
    {
        gameObject.SetActive(false);
    }

    private void SaveGameAndLoadScene()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("SampleScene");
    }
}
