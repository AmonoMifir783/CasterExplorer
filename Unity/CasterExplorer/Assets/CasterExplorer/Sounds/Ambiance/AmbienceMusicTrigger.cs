using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AmbienceMusicTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] ambianceSounds;
    public bool ambienceplay = false;
    private void OnTriggerEnter(Collider other)
    {
        if (!ambienceplay)
        {
            audioSource.Play();
            audioSource.volume = 0;
            StartCoroutine(FadeIn());
        }
        if (ambienceplay)
        {
            audioSource.Play();
            audioSource.volume = 0;
            StartCoroutine(FadeOut());
        }
        //if (!ambienceplay)
        //{
        //    audioSource.Play();
        //    audioSource.volume = 0;
        //    StartCoroutine(FadeOut());
        //}
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (ambienceplay)
    //    {
    //        StartCoroutine(FadeOut());
    //    }
    //}
    private IEnumerator FadeIn()
    {
        for (float volume = 0; volume < 1; volume += 0.01f)
        {
            audioSource.volume = volume;
            yield return new WaitForSeconds(0.01f);
        }
        if (ambianceSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = UnityEngine.Random.Range(0, ambianceSounds.Length);
            audioSource.clip = ambianceSounds[randomIndex];
            audioSource.Play();
        }
    }
    private IEnumerator FadeOut()
    {
        for (float volume = 1; volume > 0; volume -= 0.01f)
        {
            audioSource.volume = volume;
            yield return new WaitForSeconds(0.01f);
        }
        if (ambianceSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = UnityEngine.Random.Range(0, ambianceSounds.Length);
            audioSource.clip = ambianceSounds[randomIndex];
            audioSource.Play();
        }
        audioSource.Stop();
    }
}