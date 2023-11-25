using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerSettings : MonoBehaviour
{
    private const float DisableVolume = -80;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string mixerParameter;
    [SerializeField] private float minimumVolume;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.SetValueWithoutNotify(GetMixerVolume());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateMixerVolume(float volumeValue)
    {
        SetMixerVolume(volumeValue);
    }
    private void SetMixerVolume(float volumeValue)
    {
        float mixerVolume;
        if (volumeValue == 0)
            mixerVolume = DisableVolume;
        else 
        mixerVolume = Mathf.Lerp(minimumVolume, 0, volumeValue);
        audioMixer.SetFloat(mixerParameter, mixerVolume);
    }
    private float GetMixerVolume()
    {
        audioMixer.GetFloat(mixerParameter, out float mixerVolume);
        if (mixerVolume == DisableVolume)
            return 0;
        else
            return Mathf.Lerp(1, 0, mixerVolume / minimumVolume);
    }
}
