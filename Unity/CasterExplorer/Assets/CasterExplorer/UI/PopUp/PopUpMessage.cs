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

    private float startFadeTime = 2f; // Время появления картинки (в секундах)
    private float currentFadeTime = 0f;

    public bool isShowing = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isShowing)
        {
            isShowing = true;
            // Показать всплывающую картинку
            popupImage.SetActive(true);
            fadeTimer = displayTime;
            isFading = false;
            currentFadeTime = startFadeTime;
        }
    }

    private void Update()
    {
        // Если всплывающая картинка активна и не исчезает по таймеру
        if (popupImage.activeSelf && !isFading)
        {
            fadeTimer -= Time.deltaTime;

            // Если игрок нажал клавишу Enter
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // Закрыть всплывающую картинку
                FadeOut();
            }

            // Если время истекло
            if (fadeTimer <= 0f)
            {
                // Закрыть всплывающую картинку
                FadeOut();
            }
        }

        // Плавное появление картинки
        if (!isFading && currentFadeTime > 0f)
        {
            currentFadeTime -= Time.deltaTime;
            Color imageColor = popupImage.GetComponent<SpriteRenderer>().color;
            imageColor.a = 1f - (currentFadeTime / startFadeTime);
            popupImage.GetComponent<SpriteRenderer>().color = imageColor;
        }

        // Плавное исчезание картинки
        if (isFading)
        {
            Color imageColor = popupImage.GetComponent<SpriteRenderer>().color;
            imageColor.a -= fadeSpeed * Time.deltaTime;
            popupImage.GetComponent<SpriteRenderer>().color = imageColor;

            // Если картинка полностью исчезла
            if (imageColor.a <= 0f)
            {
                // Отключить всплывающую картинку
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