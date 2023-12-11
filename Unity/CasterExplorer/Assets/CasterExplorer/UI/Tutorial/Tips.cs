using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public bool isShown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isShown)
        {
            isShown = true;
            StartCoroutine(ShowText1());
        }
    }

    private IEnumerator ShowText1()
    {
        yield return new WaitForSeconds(2f);
        text1.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        text1.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(ShowText2());
    }

    private IEnumerator ShowText2()
    {
        yield return new WaitForSeconds(2f);
        text2.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        text2.gameObject.SetActive(false);
    }
}