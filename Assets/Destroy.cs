using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject Arrow;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
   
        if (Input.GetKey(KeyCode.Alpha0)) { Arrow.SetActive(false); }
    }
}
