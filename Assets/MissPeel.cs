using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissPeel : MonoBehaviour
{
    [SerializeField] private PeelSoundAcceleration_ctr _peelSoundAcceleration_Ctr;
    private bool _MissCheck = false;

    [SerializeField] private SkinRotation_ctr _StopRotation;
    [SerializeField] private Moveborder_ctr _StopMovement;

    [SerializeField] private MeshRenderer _SkinObj;
    [SerializeField] private SkinnedMeshRenderer _MissObj;

    [SerializeField] private GameObject _MissObjPrefab;
    [SerializeField] private OpenUI _openUI;
    [SerializeField] private Usukawa_Move usukawa_Move;

    // Start is called before the first frame update
    void Start()
    {
        _MissCheck = false;
        if(_MissObj.enabled)
            _MissObj.enabled = false;

        _MissObjPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _MissCheck = _peelSoundAcceleration_Ctr.Miss_Point;
        if(Input.GetKeyUp(KeyCode.Escape))
            _MissCheck = true;

        if(_MissCheck)
        {
            HappenMiss();
        }
    }

    void HappenMiss()
    {
        Debug.Log("Miss HappenÅièŒÅj");
        _openUI.openFailureDialog();

        _StopRotation.enabled = false;
        _StopMovement.enabled = false;
        usukawa_Move.enabled = false;
        _SkinObj.enabled = false;
        _MissObj.enabled = true;

        _MissObjPrefab.SetActive(true);
    }
}
