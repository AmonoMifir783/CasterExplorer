using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    public GameObject helpMenu;
    public bool isHelpMenuVisible = false;
    private Coroutine hideCoroutine;

    private void Update()
    {
        if (isHelpMenuVisible && Input.GetKeyDown(KeyCode.Return))
        {
            OffHelp();
        }
    }

    public void GetHelp()
    {
        isHelpMenuVisible = true;
        helpMenu.SetActive(true);
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideHelpMenu());
    }

    private IEnumerator HideHelpMenu()
    {
        yield return new WaitForSeconds(12f);
        helpMenu.SetActive(false);
        isHelpMenuVisible = false;
    }

    public void OffHelp()
    {
        isHelpMenuVisible = false;
        helpMenu.SetActive(false);
    }
}