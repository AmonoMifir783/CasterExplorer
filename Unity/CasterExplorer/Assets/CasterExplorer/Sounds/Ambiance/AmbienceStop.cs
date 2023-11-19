using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceStop : MonoBehaviour
{
    private AmbienceMusicTrigger ambienceMusicTrigger;
    // Start is called before the first frame update

    private void Start()
    {
        ambienceMusicTrigger = FindAnyObjectByType<AmbienceMusicTrigger>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ambienceMusicTrigger.ambienceplay)
        {
            StartCoroutine(FadeOut());
        }
        if (!ambienceMusicTrigger.ambienceplay)
        {
            StartCoroutine(FadeOut());
        }
    }
    private IEnumerator FadeIn()
    {
        for (float volume = 0; volume < 1; volume += 0.01f)
        {
            ambienceMusicTrigger.audioSource.volume = volume;
            yield return new WaitForSeconds(0.01f);
        }
        if (ambienceMusicTrigger.ambianceSounds.Length > 0 && ambienceMusicTrigger.audioSource != null)
        {
            int randomIndex = UnityEngine.Random.Range(0, ambienceMusicTrigger.ambianceSounds.Length);
            ambienceMusicTrigger.audioSource.clip = ambienceMusicTrigger.ambianceSounds[randomIndex];
            ambienceMusicTrigger.audioSource.Play();
        }
    }
    private IEnumerator FadeOut()
    {
        for (float volume = 1; volume > 0; volume -= 0.01f)
        {
            ambienceMusicTrigger.audioSource.volume = volume;
            yield return new WaitForSeconds(0.01f);
        }
        if (ambienceMusicTrigger.ambianceSounds.Length > 0 && ambienceMusicTrigger.audioSource != null)
        {
            int randomIndex = UnityEngine.Random.Range(0, ambienceMusicTrigger.ambianceSounds.Length);
            ambienceMusicTrigger.audioSource.clip = ambienceMusicTrigger.ambianceSounds[randomIndex];
            ambienceMusicTrigger.audioSource.Play();
        }
        ambienceMusicTrigger.audioSource.Stop();
    }
}
