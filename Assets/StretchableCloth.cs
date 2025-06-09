using UnityEngine;

public class StretchableCloth : MonoBehaviour
{
    public Transform anchor1;  // 固定側オブジェクト
    public Transform anchor2;  // 動く側オブジェクト

    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] modifiedVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        modifiedVertices = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        // 両端のアンカー間の距離を計算
        Vector3 direction = anchor2.position - anchor1.position;
        float distance = direction.magnitude;

        // アンカーの中間点を計算
        Vector3 midpoint = (anchor1.position + anchor2.position) / 2;

        // 頂点の位置を更新
        for (int i = 0; i < originalVertices.Length; i++)
        {
            // オリジナルの頂点の位置
            Vector3 vertex = originalVertices[i];

            // 新しい頂点の位置を計算
            Vector3 worldPos = transform.TransformPoint(vertex);
            float yFactor = Mathf.Sin((worldPos - midpoint).magnitude * Mathf.PI / distance) * (distance / 2);
            worldPos.y = Mathf.Lerp(worldPos.y, yFactor, 0.5f);

            // ローカル座標に変換して保存
            modifiedVertices[i] = transform.InverseTransformPoint(worldPos);
        }

        // メッシュに変更を適用
        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
