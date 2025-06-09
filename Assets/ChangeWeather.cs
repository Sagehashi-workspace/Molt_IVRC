using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DistantLands.Cozy;
using DistantLands.Cozy.Data;

public class ChangeWeather : MonoBehaviour
{
    //Reference module
    private CozyWeatherModule weatherModule;
    private CozyTimeModule cozyTimeModule;

    [SerializeField] private List<WeatherProfile> weatherProfiles;
    [SerializeField] private int _ChangeWeather = 0;
    [SerializeField] private int _ChangeTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        weatherModule = FindObjectOfType<CozyWeatherModule>();
        if (weatherProfiles != null && weatherProfiles.Count > 0)
        {
            ApplyWeatherProfile(weatherProfiles[0]);
        }
        cozyTimeModule = FindObjectOfType<CozyTimeModule>();
        ApplyCozyTime(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeatherByIndex(_ChangeWeather);
        if (_ChangeTime >= 0 && _ChangeTime < 5)
        {
            switch (_ChangeTime)
            {
                case 0:
                    UpdateTimeToMorning();
                    break;
                case 1:
                    UpdateTimeToNoon();
                    break;
                case 2:
                    UpdateTimeToEvening();
                    break;
                case 3:
                    UpdateTimeToNight();
                    break;
            }
        }
    }

    public void ApplyWeatherProfile(WeatherProfile profile)
    {
        if (weatherModule == null || profile == null)
        {
            Debug.LogWarning("WeatherModule or Profile is not set.");
            return;
        }
        foreach (var weather in weatherModule.ecosystem.forecastProfile.profilesToForecast)
        {
            weather.SetWeatherWeight(0);
        }

        profile.SetWeatherWeight(1.0f);
        weatherModule.PropogateVariables();
    }
    public void ChangeWeatherByIndex(int index)
    {
        if (index < 0 || index >= weatherProfiles.Count)
        {
            Debug.LogWarning("Invalid weather profile index.");
            return;
        }

        ApplyWeatherProfile(weatherProfiles[index]);
    }

    public void ApplyCozyTime(float timePercentage)
    {
        if (cozyTimeModule != null)
        {
            timePercentage = Mathf.Clamp01(timePercentage);

            cozyTimeModule.currentTime = timePercentage;

            float hours = timePercentage * 24;
            int hourPart = Mathf.FloorToInt(hours);
            int minutePart = Mathf.FloorToInt((hours - hourPart) * 60);
            //Debug.Log($"Now Time: {hourPart:D2}:{minutePart:D2}");
        }
        else
        {
            Debug.LogWarning("No set time");
        }
    }
    public void UpdateTimeToMorning()
    {
        ApplyCozyTime(0.25f);
    }

    public void UpdateTimeToNoon()
    {
        ApplyCozyTime(0.5f);
    }

    public void UpdateTimeToEvening()
    {
        ApplyCozyTime(0.75f);
    }

    public void UpdateTimeToNight()
    {
        ApplyCozyTime(0.85f);
    }

    public int ChangeWeatherNumber
    {
        get { return _ChangeWeather; }
        set { _ChangeWeather = value; }
    }
    public int ChangeTimeNumber
    {
        get { return _ChangeTime; }
        set { _ChangeTime = value; }
    }
}
