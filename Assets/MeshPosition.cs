using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshPosition : MonoBehaviour
{
    [SerializeField] private Cloth cloth; // Clothコンポーネント
    [SerializeField] private int vertexIndex = 0; // 固定したい頂点のインデックス
    [SerializeField] private Transform targetObject; // 頂点が追従するオブジェクト

    private Mesh clothMesh; // Clothのメッシュ
    private Vector3[] originalVertices; // Clothの頂点配列
    private Vector3[] modifiedVertices; // 更新された頂点配列

    void Start()
    {
        // Clothのメッシュを取得
        clothMesh = cloth.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        // 元の頂点情報を保存
        originalVertices = clothMesh.vertices;
        // 更新用に頂点配列をコピー
        modifiedVertices = (Vector3[])originalVertices.Clone();
    }

    void Update()
    {
        if (vertexIndex < 0 || vertexIndex >= modifiedVertices.Length)
        {
            Debug.LogError("頂点インデックスが範囲外です");
            return;
        }

        // 頂点の位置をターゲットオブジェクトの位置に更新
        modifiedVertices[vertexIndex] = cloth.transform.InverseTransformPoint(targetObject.position);

        // 更新した頂点をClothメッシュに反映
        clothMesh.vertices = modifiedVertices;
        cloth.GetComponent<SkinnedMeshRenderer>().sharedMesh = clothMesh;
    }

    // メッシュを元の状態にリセットする
    public void ResetMesh()
    {
        clothMesh.vertices = originalVertices;
        clothMesh.RecalculateNormals(); // 必要に応じて法線を再計算
        cloth.GetComponent<SkinnedMeshRenderer>().sharedMesh = clothMesh;
    }

    void OnDisable()
    {
        // 再生終了時にメッシュを元に戻す
        ResetMesh();
    }
}
