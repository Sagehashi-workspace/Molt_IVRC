using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretchable : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Transform child;
    [SerializeField] private new SkinnedMeshRenderer renderer;
    [SerializeField] private float ren = 33f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(parent.position, child.position);
        renderer.SetBlendShapeWeight(0, (dist - 2) * ren);
    }
}
