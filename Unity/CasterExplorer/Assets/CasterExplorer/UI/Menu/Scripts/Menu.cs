using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("First Selected Button")]
    [SerializeField] private Button firstSelected;

    protected virtual void OnEnable()
    {
        SetFirstSelected(firstSelected);
    }

    public void SetFirstSelected(Button firstSelectedButton)
    {
        firstSelectedButton.Select();
    }
}
