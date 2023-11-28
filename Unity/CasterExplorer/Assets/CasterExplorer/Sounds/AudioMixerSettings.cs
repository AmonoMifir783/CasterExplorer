using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerSettings : MonoBehaviour
{
    private const float DisabledVolume = -80;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _mixerParameter;
    [SerializeField] private float _minimumVolume;
    private void Start()
    {
        _volumeSlider.SetValueWithoutNotify(GetMixerVolume());
        int currentResolutionIndex = 0;
        LoadSettings(currentResolutionIndex);
    }
    public void UpdateMixerVolume(float volumeValue)
    {
        SetMixerVolume(volumeValue);
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("VolumePreference", _volumeSlider.value);
    }
    public void LoadSettings(int currentResolutionIndex)
    {
      
        if (PlayerPrefs.HasKey("VolumePreference"))
            _volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
        else
            _volumeSlider.value = 1f;

        SetMixerVolume(_volumeSlider.value);

  
    }
    private void SetMixerVolume(float volumeValue)
    {
        float mixerVolume;
        if (volumeValue == 0)
            mixerVolume = DisabledVolume;
        else
            mixerVolume = Mathf.Lerp(_minimumVolume, 0, volumeValue);
        _audioMixer.SetFloat(_mixerParameter, mixerVolume);
    }
    private float GetMixerVolume()
    {
        _audioMixer.GetFloat(_mixerParameter, out float mixerVolume);
        if (mixerVolume == DisabledVolume)
            return 0;
        else
            return Mathf.Lerp(1, 0, mixerVolume / _minimumVolume);
    }
}
