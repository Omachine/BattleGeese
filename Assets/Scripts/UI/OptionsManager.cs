using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    void Start()
    {
        // Volume Control
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;
        AudioListener.volume = savedVolume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Graphics Quality
        SetupQualityDropdown();

        // Fullscreen Toggle
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // Resolution Settings
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private void SetupQualityDropdown()
    {
        qualityDropdown.ClearOptions();
        List<string> options = new List<string>();

        string[] qualityNames = QualitySettings.names;
        int currentQualityIndex = QualitySettings.GetQualityLevel();

        for (int i = 0; i < qualityNames.Length; i++)
        {
            options.Add(qualityNames[i]);
        }

        qualityDropdown.AddOptions(options);
        qualityDropdown.value = currentQualityIndex;
        qualityDropdown.RefreshShownValue();
        qualityDropdown.onValueChanged.AddListener(SetQuality);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
