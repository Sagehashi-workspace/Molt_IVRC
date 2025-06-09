using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InputMode : MonoBehaviour
{
    // ïœêîÇ…ÉLÅ[ì¸óÕÇï€ë∂Ç∑ÇÈ
    [SerializeField] private SteamVR_Input_Sources handType = SteamVR_Input_Sources.Any;
    [SerializeField] private SteamVR_Action_Boolean triggerAction;
    [SerializeField] private SteamVR_Action_Boolean gripAction;
    [SerializeField] private SteamVR_Action_Boolean primaryButtonAction;
    [SerializeField] private SteamVR_Action_Boolean secondaryButtonAction;
    public string mode = "1";

    public SteamVR_Action_Boolean AButton;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (var key in new[] { KeyCode.R, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 })
            {
                if (Input.GetKeyDown(key))
                {
                    mode = key == KeyCode.R ? "r" : key.ToString().Replace("Alpha", "");
                    return;
                }
            }
        }
        /*
        var actions = new Dictionary<SteamVR_Action_Boolean, string>
        {
            { triggerAction, "1" },
            { primaryButtonAction, "2" },
            { secondaryButtonAction, "3" },
            { gripAction, "r" }
        };

        foreach (var action in actions)
        {
            if (action.Key.GetState(handType))
            {
                mode = action.Value;
                break;
            }
        }
        */
    }
}
