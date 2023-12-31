using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsOpen : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    public GameObject OptionsMenu;

    public GameObject pauseMenuUI;

    public void OnOptionsClicked()
    {
        OptionsMenu.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
}
