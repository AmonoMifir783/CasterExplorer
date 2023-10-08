using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;
    private void Start()
    {
        DisableButtonsDependingOnData();
    }

    private void DisableButtonsDependingOnData()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }

    public void onNewGameClicked()
    {
        //SceneManager.LoadScene("SampleScene");// в кавычках название сцены на которую осуществляется переход

        //Time.timeScale = 1f;
        //Cursor.visible = false;
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void onContinueGameClicked()
    {
        //DataPersistenceManager.instance.LoadGame();
        DisableMenuButtons();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("SampleScene");
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        DisableButtonsDependingOnData();
    }
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}