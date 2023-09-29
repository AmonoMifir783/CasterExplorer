using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene"); // ��������� ����� � ����� ������
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu"); // ��������� ������� ����
    }

    
}