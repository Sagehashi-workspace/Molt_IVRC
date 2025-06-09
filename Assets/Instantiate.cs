using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public GameObject Target;
    public int arrowx;
    public int arrowy;
    public int arrowz;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Target,new Vector3(arrowx,arrowy,arrowz), Quaternion.identity);
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.Alpha9)) {
            if (GameObject.Find("Arrow5(Clone)") ==false) {
                Instantiate(Target, new Vector3(arrowx, arrowy, arrowz), Quaternion.identity);
            }
        }
    }


}
