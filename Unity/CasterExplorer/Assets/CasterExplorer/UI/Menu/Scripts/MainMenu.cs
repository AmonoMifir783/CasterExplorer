using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onNewGameClicked()
    {
        SceneManager.LoadScene("SampleScene");// в кавычках название сцены на которую осуществляется переход
        
        Time.timeScale = 1f;
        Cursor.visible = false;
        //DataPersistenceManager.instance.NewGame();
    }

    public void onLoadGameClicked()
    {
        DataPersistenceManager.instance.LoadGame();
    }

    //public void onSaveGameClicked()
    //{
    //    DataPersistenceManager.instance.SaveGame();
    //}



    public void ExitGame()
    {
        Application.Quit();
    }
}