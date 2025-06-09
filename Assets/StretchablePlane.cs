using UnityEngine;

public class StretchablePlane : MonoBehaviour
{
    public Transform anchor; // 固定側オブジェクト
    public Transform target; // 動く側オブジェクト

    private Vector3 initialScale;
    private Mesh mesh;
    private Vector3[] originalVertices;

    void Start()
    {
        // 初期スケールを記録
        initialScale = transform.localScale;

        // Meshと頂点の初期状態を取得
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
    }

    void Update()
    {
        // AnchorとTargetの位置を取得
        Vector3 anchorPosition = anchor.position;
        Vector3 targetPosition = target.position;

        // AnchorからTargetへのベクトルを計算
        Vector3 direction = targetPosition - anchorPosition;
        float distance = direction.magnitude;

        // PlaneのスケールをX軸方向に距離に合わせて調整
        transform.localScale = new Vector3(distance / 10f, initialScale.y, initialScale.z);

        // Planeの位置をAnchorとTargetの中間に設定
        transform.position = (anchorPosition + targetPosition) / 2;

        // Planeの回転をAnchorとTargetの方向に合わせる
        transform.right = direction.normalized;
    }
}
