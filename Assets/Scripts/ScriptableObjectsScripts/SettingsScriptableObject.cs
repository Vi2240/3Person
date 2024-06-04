using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsScriptableObject", menuName = "ScriptableObjects/Settings")]

public class SettingsScriptableObject : ScriptableObject
{
    public float sensitivity = 1;
    public float maxSensitivity = 100;

    public float musicVolume;
    public float maxMusicVolume = 1;

    public float sfxVolume;
    public float maxSfxVolume = 1;
}
