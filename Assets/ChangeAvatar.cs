using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAvatar : MonoBehaviour
{
    [SerializeField] private int _ChangeAvatar = 0;
    private int _TempAvatarIndex = 0;

    [SerializeField] private GameObject [] Avatar;

    // Start is called before the first frame update
    void Awake()
    {

        _TempAvatarIndex = _ChangeAvatar;

        if(_ChangeAvatar < 0 ||  _ChangeAvatar >= Avatar.Length)
        {
            Debug.Log("Only Avatar number");
            _ChangeAvatar = 0;
        }

        for (int i = 0; i < Avatar.Length; i++) { Avatar[i].SetActive(false); }
        Avatar[_ChangeAvatar].SetActive(true);
    }
    void Update()
    {
        //Debug.Log(_ChangeAvatar + "script");
        if (_ChangeAvatar != _TempAvatarIndex)
        {
            if (_ChangeAvatar < 0 || _ChangeAvatar >= Avatar.Length)
            {
                Debug.Log("Only Avatar number");
                _ChangeAvatar = 0;
            }

            Avatar[_TempAvatarIndex].SetActive(false);
            Avatar[_ChangeAvatar].SetActive(true);

            _TempAvatarIndex = _ChangeAvatar;
        }
    }
    public int AvatarNumber
    {
        get { return _ChangeAvatar; }
        set { _ChangeAvatar = value; }
    }
}
