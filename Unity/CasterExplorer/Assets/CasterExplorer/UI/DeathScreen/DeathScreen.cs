using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene"); // загружаем сцену с игрой заново
   
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu"); // загружаем главное меню
    }

    
}