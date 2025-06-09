using System;
using UnityEngine;

public class PlaneSagController : MonoBehaviour
{
    // 分割数（ポリゴン数=128x128x2）
    [SerializeField] private int n_divx = 128;
    [SerializeField] private int n_divz = 128;

    // サイズ
    [SerializeField] private float size_x = 10f;
    [SerializeField] private float size_z = 0.5f;

    // 湾曲程度
    [SerializeField] private float a = -0.07f;
    [SerializeField] private float b = 22f;

    // たるみ量
    private float Sag;

    // 片方の辺を固定するオブジェクト
    public Transform fixedObject;

    private DateTime t_0;

    private Mesh mesh;
    private Vector3[] vertices;

    void Awake()
    {
        CreateMesh();
        Deform(a, b);
    }

    void Start()
    {
        Deform(a, b);
    }

    void Update()
    {
        TimeSpan t = DateTime.Now - t_0;
        double omega = 0.01;
        double A = a * Math.Sin(omega * t.TotalMilliseconds);
        double B = b * Math.Sin(omega * t.TotalMilliseconds);

        Deform(a - Sag / 5, b);
    }

    void CreateMesh()
    {
        // メッシュの初期化
        mesh = new Mesh();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;

        // 頂点数
        int n_vertx = n_divx + 1;
        int n_vertz = n_divz + 1;

        // 頂点とUVの初期化
        vertices = new Vector3[n_vertx * n_vertz];
        Vector2[] uv = new Vector2[n_vertx * n_vertz];
        int k = 0;

        for (int i = 0; i <= n_divz; i++)
        {
            for (int j = 0; j <= n_divx; j++)
            {
                float u = (float)j / n_divx;
                float v = (float)i / n_divz;
                float x = (u - 0.5f) * size_x;
                float z = (v - 0.5f) * size_z;
                float y = 0;  // 初期の高さ
                vertices[k] = new Vector3(x, z, y);
                uv[k++].Set(u, v);
            }
        }

        // 三角形ポリゴンの設定
        var triangles = new int[6 * n_divx * n_divz];
        int l = 0, A = 0, B = 1, C = n_vertx, D = n_vertx + 1;
        for (int i = 0; i < n_divz; i++)
        {
            for (int j = 0; j < n_divx; j++)
            {
                // 1つ目の三角形
                triangles[l++] = A;
                triangles[l++] = C++;
                triangles[l++] = D;
                // 2つ目の三角形
                triangles[l++] = B++;
                triangles[l++] = A++;
                triangles[l++] = D++;
            }
            A++; B++; C++; D++;
        }

        // メッシュ生成
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // 法線とバウンディングボックスの再計算
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void Deform(float a, float b)
    {
        // たるみを反映させるために頂点を変形
        for (int i = 0; i < vertices.Length; i++)
        {
            float x = vertices[i].x;
            float z = vertices[i].z;
            float y = a * x * x + b * z * z;

            // 左端の頂点を固定オブジェクトの位置に固定
            if (i % (n_divx + 1) == 0)  // 左端の頂点
            {
                vertices[i] = fixedObject.position;
            }
            else
            {
                vertices[i] = new Vector3(x, z, y);
            }
        }

        // メッシュに変形を適用
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        //GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    // Sagのプロパティ
    public float ChangeSag
    {
        get { return this.Sag; }  //取得用
        set { this.Sag = value; } //値入力用
    }
}
