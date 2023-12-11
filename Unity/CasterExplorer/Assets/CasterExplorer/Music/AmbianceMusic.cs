using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 1f; // ������������ �������� ��������
    private float targetVolume = 0f; // ������� ��������� �����

    private void OnEnable()
    {
        // ������������� ������� ��������� �� ������������ ��������
        targetVolume = 0.25f;
        // ��������� ������� ��������� �����
        //StartCoroutine(FadeAudio(true));
    }

    private void OnDisable()
    {
        // ������������� ������� ��������� �� ����������� ��������
        targetVolume = 0f;
        // ��������� ������� ������������ �����
        //StartCoroutine(FadeAudio(false));
    }

    private IEnumerator FadeAudio(bool fadeIn)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        while (Time.time - startTime < fadeDuration)
        {
            float elapsedTime = Time.time - startTime;
            float normalizedTime = elapsedTime / fadeDuration;

            if (fadeIn)
            {
                audioSource.volume = Mathf.Lerp(0f, startVolume, normalizedTime);
            }
            else
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0f, normalizedTime);
            }

            yield return null;
        }

        if (!fadeIn)
        {
            audioSource.Stop();
        }
    }

    public void StopMusic()
    {
        StopAllCoroutines();
        audioSource.Stop();
        audioSource.volume = 0f;
    }

    public void RestartMusic()
    {
        StopAllCoroutines();
        audioSource.Play();
        audioSource.volume = 0.25f;
    }
}
