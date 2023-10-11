using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;
    bool isPaused = false;
    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene"); // ��������� ����� � ����� ������
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public void LoadGame()
    {
        saveSlotsMenu.ActivateMenu(true);
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        isPaused = false;
    }

    
}