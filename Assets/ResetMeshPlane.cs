using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMeshPlane : MonoBehaviour
{
    [SerializeField] private Cloth cloth; // Clothコンポーネント
    [SerializeField] private int vertexIndex = 0; // 固定したい頂点のインデックス
    [SerializeField] private Transform targetObject; // 頂点が追従するオブジェクト

    private Mesh clothMesh; // Clothのメッシュ
    private Vector3[] vertices; // Clothの頂点配列

    void Start()
    {
        // Clothのメッシュを取得
        clothMesh = cloth.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        vertices = clothMesh.vertices;
    }

    void Update()
    {
        if (vertexIndex < 0 || vertexIndex >= vertices.Length)
        {
            Debug.LogError("頂点インデックスが範囲外です");
            return;
        }

        // 頂点の位置をターゲットオブジェクトの位置に更新
        vertices[vertexIndex] = new Vector3(5.0f,0.0f,5.0f);

        // 更新した頂点をClothメッシュに反映
        clothMesh.vertices = vertices;
        cloth.GetComponent<SkinnedMeshRenderer>().sharedMesh = clothMesh;
    }
}
