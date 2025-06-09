using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR.InteractionSystem;

public class StopScript : MonoBehaviour
{
    [SerializeField] private SkinPeel_ctr skinPeel_Ctr;
    [SerializeField] private Moveborder_ctr moveborder_Ctr;
    [SerializeField] private PeelSoundAcceleration_ctr peelSoundAcceleration_Ctr;
    [SerializeField] private MissPeel missPeel;
    [SerializeField] private Usukawa_Move usukawa_Move;
    [SerializeField] private Transform _Border;

    public SerialManager serialmanager;
    private Vector3 tmpposition = Vector3.zero;
    private float length_rate = 0.0f;
    [SerializeField] private float arm_L = 20.0f;
    // Start is called before the first frame update
    void Awake()
    {
        skinPeel_Ctr.enabled = false;
        moveborder_Ctr.enabled = false;
        peelSoundAcceleration_Ctr.enabled = false;
        missPeel.enabled = false;
        usukawa_Move.enabled = false;
        tmpposition = _Border.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (serialmanager.check_length)
        {
            length_rate = calc_rate();
            _Border.localPosition = new Vector3(tmpposition.x + length_rate * serialmanager.GetLength() / 100f, tmpposition.y, tmpposition.z);
            //_Border.localPosition = new Vector3(tmpposition.x + serialmanager.GetLength() / 100f, tmpposition.y, tmpposition.z);

            skinPeel_Ctr.enabled = true;
            moveborder_Ctr.enabled = true;
            peelSoundAcceleration_Ctr.enabled = true;
            missPeel.enabled = true;
            serialmanager.check_length = false;
            usukawa_Move.enabled = true;
        }
    }

    private float calc_rate()
    {
        return arm_L / serialmanager.GetLength();
    }

    public float Getrate()
    {
        return length_rate;
    }
}