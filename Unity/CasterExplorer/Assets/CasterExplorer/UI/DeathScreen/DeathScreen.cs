using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene"); // ��������� ����� � ����� ������
   
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu"); // ��������� ������� ����
    }

    
}