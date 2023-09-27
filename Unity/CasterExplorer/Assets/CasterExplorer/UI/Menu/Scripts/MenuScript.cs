using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene("SampleScene");// в кавычках название сцены на которую осуществляется переход
        
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}