using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsOpen : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject OptionsMenu;

    public void OnOptionsClicked()
    {
        OptionsMenu.SetActive(true);
    }
}
