using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider ambientVolumeSlider;
    [SerializeField] private Slider sFXVolumeSlider;
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Dropdown qualityDropdown;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Button applyButton;
    [SerializeField] private AudioMixer master;
    [SerializeField] float mVolume;
    [SerializeField] float sVolume;
    [SerializeField] float aVolume;


    void Awake()
    {
        master.GetFloat("Master", out mVolume);
        masterVolumeSlider.value = mVolume;
        master.GetFloat("SFX", out sVolume);
        sFXVolumeSlider.value = sVolume;
        master.GetFloat("Ambient", out aVolume);
        ambientVolumeSlider.value = aVolume;
        resolutionDropdown.ClearOptions();
        List<string> resolutions = new List<string>();
        for (int index = 0; index < Screen.resolutions.Length; index++)
        {
            resolutions.Add(string.Format("{0} x {1}", Screen.resolutions[index].width, Screen.resolutions[index].height));
        }
        resolutionDropdown.AddOptions(resolutions);
        // Build quality levels
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());
    }

    void OnEnable()
    {
        //masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume", masterVolumeSlider.maxValue);
        fullscreenToggle.isOn = Screen.fullScreen;
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        //applyButton.interactable = false;
    }

    void Update()
    {
        mVolume = masterVolumeSlider.value;
        aVolume = ambientVolumeSlider.value;
        sVolume = sFXVolumeSlider.value;
        master.SetFloat("Master", mVolume);
        master.SetFloat("Ambient", aVolume);
        master.SetFloat("SFX", sVolume);
    }

    public void Window()
    {
        if (fullscreenToggle.isOn == Screen.fullScreen)
        {
            fullscreenToggle.isOn = false;
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else if (fullscreenToggle.isOn != Screen.fullScreen)
        {
            fullscreenToggle.isOn = true;
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }

    public void ResDrop()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(1024, 576, fullscreenToggle.isOn);
                break;
            case 1:
                Screen.SetResolution(1156, 648, fullscreenToggle.isOn);
                break;
            case 2:
                Screen.SetResolution(1280, 720, fullscreenToggle.isOn);     
                break;
            case 3:
                Screen.SetResolution(1366, 768, fullscreenToggle.isOn);
                break;
            case 4:
                Screen.SetResolution(1600, 900, fullscreenToggle.isOn);
                break;
            case 5:            
                Screen.SetResolution(1920, 1080, fullscreenToggle.isOn);
                break;
        }
    }

    public void QualityDrop()
    {
        switch (qualityDropdown.value)
        {
            case 0:
                QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                QualitySettings.SetQualityLevel(1);
                break;
            case 2:
                QualitySettings.SetQualityLevel(2);
                break;
            case 3:
                QualitySettings.SetQualityLevel(3);
                break;
            case 4:
                QualitySettings.SetQualityLevel(4);
                break;
            case 5:
                QualitySettings.SetQualityLevel(5);
                break;
        }
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Master", mVolume);
        PlayerPrefs.SetFloat("SFX", sVolume);
        PlayerPrefs.SetFloat("Ambient", aVolume);
    }

    public void ShowSettingsPanel() { settingsPanel.SetActive(true); }
}