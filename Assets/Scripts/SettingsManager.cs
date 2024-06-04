using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : SingeltonPersistent<SettingsManager>
{
    [SerializeField] SettingsScriptableObject settingsScriptableObject;

    [SerializeField] Scrollbar sensitivitySlider;
    [SerializeField] Scrollbar musicVolumeSlider;
    [SerializeField] Scrollbar sfxVolumeSlider;

    [SerializeField] TextMeshProUGUI sensitivityText;
    [SerializeField] TextMeshProUGUI musicVolumeText;
    [SerializeField] TextMeshProUGUI sfxVolumeText;

    [SerializeField] AudioSource musicPlayer = null;

    private void Update()
    {
        settingsScriptableObject.sensitivity = sensitivitySlider.value * settingsScriptableObject.maxSensitivity;
        settingsScriptableObject.musicVolume = musicVolumeSlider.value * settingsScriptableObject.maxMusicVolume;
        settingsScriptableObject.sfxVolume = sfxVolumeSlider.value * settingsScriptableObject.maxSfxVolume;

        sensitivityText.text = "Sensitivity " + settingsScriptableObject.sensitivity.ToString();
        musicVolumeText.text = "Music volume " + settingsScriptableObject.musicVolume.ToString();
        sfxVolumeText.text = "SFX volume " + settingsScriptableObject.sfxVolume.ToString();

        if(musicPlayer != null)
        {
            musicPlayer.volume = settingsScriptableObject.musicVolume;
        }
    }
}
