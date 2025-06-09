using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    [SerializeField] private AnimatedDialog _animatedSuccessDialog;
    [SerializeField] private AnimatedDialog _animatedFailureDialog;
    
    public void openSuccessDialog()
    {
        _animatedSuccessDialog.Open();
    }

    public void openFailureDialog()
    {
        _animatedFailureDialog.Open();
    }
}
