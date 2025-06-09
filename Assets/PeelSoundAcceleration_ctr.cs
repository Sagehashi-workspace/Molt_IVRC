using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PeelSoundAcceleration_ctr : MonoBehaviour
{
    [SerializeField] private float _Vec_threshold = 0.1f;

    public bool CheckUpper_Threshold = false;

    [SerializeField] AddEffect SetVelocity;
    [SerializeField] private SerialManager serialManager;
    [SerializeField] private ForceGaugeController forceGaugeController;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CheckUpper_Threshold);
        // ‘¬“x‚ÌŽæ“¾
        //VelocityEstimator VE = GetComponent<VelocityEstimator>();

        float Velocity = serialManager.Getv() / 0.3f;

        Debug.Log($"Acceleration:{Velocity}");

        if (Velocity > 1.0f)
        {
            CheckUpper_Threshold = true;
            Velocity = 1.0f;
        }

        SetVelocity.SetAcc_Velocity = Velocity;

        //‰¹—Ê‚Ì•Ï‰»
        GetComponent<AudioSource>().volume = Velocity;
    }

    public bool Miss_Point
    {
        set { CheckUpper_Threshold = value; }
        get { return CheckUpper_Threshold; }
    }
}