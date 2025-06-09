using System.Collections;
using UnityEngine;

using UnityEngine.UI;

public class ForceGaugeController : MonoBehaviour
{
    /*
    [SerializeField] private Slider forceGauge;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image optimalZoneMarker;
    [SerializeField] private Image warningZoneMarker;
    [SerializeField] private Image tearZoneMarker;
    */
    private float warningLimit = 0.20f;
    public float tearLimit = 0.25f;
    private float max = 0.30f;
    private float warningLimitRate;
    private float tearLimitRate;
    private float value = 0.0f;
    /*
    [SerializeField] private Color normalColor = Color.green;
    [SerializeField] private Color warningColer = Color.yellow;
    [SerializeField] private Color tearColor = Color.red;
    */

    [SerializeField] private SerialManager serialManager;


    [SerializeField] private PeelSoundAcceleration_ctr peelSoundAcceleration_Ctr;

    // Start is called before the first frame update
    void Start()
    {
        max = tearLimit + 0.05f;
        SetMarkerPositions();
    }

    // Update is called once per frame
    void Update()
    {
        value = serialManager.Getv() / max;

        UpdateFillColor();
    }
    private void UpdateFillColor()
    {
        if (value >= tearLimitRate)
        {
            peelSoundAcceleration_Ctr.CheckUpper_Threshold = true;
        }
    }

    public void SetMarkerPositions()
    {
        warningLimitRate = warningLimit / max;
        tearLimitRate = tearLimit / max;
        /*
        optimalZoneMarker.rectTransform.anchoredPosition = new Vector2(warningLimitRate * forceGauge.GetComponent<RectTransform>().sizeDelta.x / 2, optimalZoneMarker.rectTransform.anchoredPosition.y);
        Vector2 sd = optimalZoneMarker.GetComponent<RectTransform>().sizeDelta;
        sd.x = warningLimitRate * forceGauge.GetComponent<RectTransform>().sizeDelta.x;
        optimalZoneMarker.GetComponent<RectTransform>().sizeDelta = sd;

        warningZoneMarker.rectTransform.anchoredPosition = new Vector2((tearLimitRate + warningLimitRate) * forceGauge.GetComponent<RectTransform>().sizeDelta.x / 2, warningZoneMarker.rectTransform.anchoredPosition.y);
        Vector2 sd2 = warningZoneMarker.GetComponent<RectTransform>().sizeDelta;
        sd2.x = (tearLimitRate - warningLimitRate) * forceGauge.GetComponent<RectTransform>().sizeDelta.x;
        warningZoneMarker.GetComponent<RectTransform>().sizeDelta = sd2;

        tearZoneMarker.rectTransform.anchoredPosition = new Vector2((1 + tearLimitRate) * forceGauge.GetComponent<RectTransform>().sizeDelta.x / 2, optimalZoneMarker.rectTransform.anchoredPosition.y);
        Vector2 sd3 = tearZoneMarker.GetComponent<RectTransform>().sizeDelta;
        sd3.x = (1 - tearLimitRate) * forceGauge.GetComponent<RectTransform>().sizeDelta.x;
        tearZoneMarker.GetComponent<RectTransform>().sizeDelta = sd3;
        */
    }

    public float Getmax()
    {
        return max;
    }
}