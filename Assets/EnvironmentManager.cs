using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public ChangeTerrain changeTerrain;
    public ChangeWeather changeWeather;
    public ForceGaugeController forceGaugeController;

    [SerializeField] private List<EnvironmentPreset> environmentPresets;
    // Start is called before the first frame update

    [SerializeField] private int environmentPresetIndex;
    private int preenv;

    void Start()
    {
        preenv = environmentPresetIndex;
        if (environmentPresets != null && environmentPresets.Count > 0)
        {
            ApplyEnvironmentPreset(0);
        }
    }

    void Update()
    {
        if (preenv != environmentPresetIndex)
        {
            ApplyEnvironmentPreset(environmentPresetIndex);
            preenv = environmentPresetIndex;
        }
    }

    public void ApplyEnvironmentPreset(int presetIndex)
    {
        if (presetIndex >= 0 && presetIndex < environmentPresets.Count)
        {
            EnvironmentPreset preset = environmentPresets[presetIndex];
            changeTerrain.ChangeTerrainByIndex(preset.terrainIndex);
            changeWeather.ChangeWeatherByIndex(preset.weatherIndex);
        }
        else
        {
            Debug.LogError("indexŠO‚Å‚·‚æII");
        }
    }

    public int EnviromentIndex
    {
        get { return environmentPresetIndex; }
        set { environmentPresetIndex = value; }
    }
}