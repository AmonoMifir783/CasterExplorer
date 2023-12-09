using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMessage : MonoBehaviour
{
    public GameObject popupImage;
    public float fadeSpeed = 1f;
    public float displayTime = 15f;
    private bool isFading = false;
    private float fadeTimer = 0f;

    private float startFadeTime = 2f; // ����� ��������� �������� (� ��������)
    private float currentFadeTime = 0f;

    public bool isShowing = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isShowing)
        {
            isShowing = true;
            // �������� ����������� ��������
            popupImage.SetActive(true);
            fadeTimer = displayTime;
            isFading = false;
            currentFadeTime = startFadeTime;
        }
    }

    private void Update()
    {
        // ���� ����������� �������� ������� � �� �������� �� �������
        if (popupImage.activeSelf && !isFading)
        {
            fadeTimer -= Time.deltaTime;

            // ���� ����� ����� ������� Enter
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // ������� ����������� ��������
                FadeOut();
            }

            // ���� ����� �������
            if (fadeTimer <= 0f)
            {
                // ������� ����������� ��������
                FadeOut();
            }
        }

        // ������� ��������� ��������
        if (!isFading && currentFadeTime > 0f)
        {
            currentFadeTime -= Time.deltaTime;
            Color imageColor = popupImage.GetComponent<SpriteRenderer>().color;
            imageColor.a = 1f - (currentFadeTime / startFadeTime);
            popupImage.GetComponent<SpriteRenderer>().color = imageColor;
        }

        // ������� ��������� ��������
        if (isFading)
        {
            Color imageColor = popupImage.GetComponent<SpriteRenderer>().color;
            imageColor.a -= fadeSpeed * Time.deltaTime;
            popupImage.GetComponent<SpriteRenderer>().color = imageColor;

            // ���� �������� ��������� �������
            if (imageColor.a <= 0f)
            {
                // ��������� ����������� ��������
                popupImage.SetActive(false);
                isFading = false;
            }
        }
    }

    private void FadeOut()
    {
        isFading = true;
    }
}