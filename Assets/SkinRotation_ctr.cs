using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinRotation_ctr : MonoBehaviour
{
    [SerializeField]private Transform target;

    void Update()
    {
        if (target != null)
        {
            // ターゲットの方向を取得
            Vector3 direction = target.position - transform.position;

            // 対象方向に向けて回転
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
