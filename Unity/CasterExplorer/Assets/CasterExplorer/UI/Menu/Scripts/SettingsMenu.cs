using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    Resolution[] resolutions;
    public GameObject OptionsMenu;
    public MouseLook mouseLookScript;
    public Slider sensitivitySliderX;
    public Slider sensitivitySliderY;

    public GameObject pauseMenuUI;

    private const float DisableVolume = -80;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string mixerParameterone;
    [SerializeField] private string mixerParametertwo;
    [SerializeField] private float minimumVolume;
    //музыка слайдер пока отсутствует, но работает по той же схеме

    void Start()
    {
        volumeSlider.SetValueWithoutNotify(GetMixerVolume());
        musicSlider.SetValueWithoutNotify(GetMixerVolumeMusic());
        mouseLookScript = GetComponent<MouseLook>();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ExitSettings()
    {
        OptionsMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumePreference", volumeSlider.value);
        PlayerPrefs.SetFloat("MusicPreference", musicSlider.value);
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        else
            qualityDropdown.value = 3;

        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            resolutionDropdown.value = currentResolutionIndex;

        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
        if (PlayerPrefs.HasKey("VolumePreference"))
            volumeSlider.value = PlayerPrefs.GetFloat("VolumePreference");
        else
            volumeSlider.value = 1f;

        SetMixerVolume(volumeSlider.value);

        if (PlayerPrefs.HasKey("MusicPreference"))
            musicSlider.value = PlayerPrefs.GetFloat("MusicPreference");
        else
            musicSlider.value = 1f;

        SetMixerVolumeMusic(musicSlider.value);
    }
    public void UpdateMixerVolume(float volumeValue)
    {
        SetMixerVolume(volumeValue);
    }
    public void UpdateMixerVolumeMusic(float volumeValuemusic)
    {
        SetMixerVolumeMusic(volumeValuemusic);
    }
    private void SetMixerVolume(float volumeValue)
    {
        float mixerVolume;
        if (volumeValue == 0)
            mixerVolume = DisableVolume;
        else
            mixerVolume = Mathf.Lerp(minimumVolume, 0, volumeValue);
        audioMixer.SetFloat(mixerParameterone, mixerVolume);
    }
    private float GetMixerVolume()
    {
        audioMixer.GetFloat(mixerParameterone, out float mixerVolume);
        if (mixerVolume == DisableVolume)
            return 0;
        else
            return Mathf.Lerp(1, 0, mixerVolume / minimumVolume);
    }
    private void SetMixerVolumeMusic(float volumeValuemusic)
    {
        float mixerVolumeMusic;
        if (volumeValuemusic == 0)
            mixerVolumeMusic = DisableVolume;
        else
            mixerVolumeMusic = Mathf.Lerp(minimumVolume, 0, volumeValuemusic);
        audioMixer.SetFloat(mixerParametertwo, mixerVolumeMusic);
    }
    private float GetMixerVolumeMusic()
    {
        audioMixer.GetFloat(mixerParametertwo, out float mixerVolumeMusic);
        if (mixerVolumeMusic == DisableVolume)
            return 0;
        else
            return Mathf.Lerp(1, 0, mixerVolumeMusic / minimumVolume);
    }
}