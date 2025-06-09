using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    //分割数（ポリゴン数=128x128x2）
    [SerializeField] private int n_divx = 128;
    [SerializeField] private int n_divz = 128;

    //サイズ
    [SerializeField] private float size_x = 10f;
    [SerializeField] private float size_z = 0.5f;

    //湾曲程度
    [SerializeField] private float a = -0.07f;
    [SerializeField] private float b = 22f;
    
    private DateTime t_0;

    private float Sag = 0.0f;
    private bool EndCheck = false;
    // Start is called before the first frame update
    void Awake()
    {
        Deform(0, b);
    }

    void Start()
    {

        //a = 0.8f;
        //b = 0.8f;
        Deform(0, b);
        //t_0 = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan t = DateTime.Now - t_0;
        double omega = 0.01;
        //a = 1.0f;
        //b = 0.0f;

        //Debug.Log("Sag :" + Sag);
        float a_1 = a - Sag / 5;

        if (EndCheck)
        {
            double A = a_1 * Math.Sin(omega * t.TotalMilliseconds);
            double B = b * Math.Sin(omega * t.TotalMilliseconds);

            Deform((float)a_1, (float)B);
        }
        else
        {
            Deform(a_1, b);
        }
    }
    void Deform(float a, float b)
    {
        // 現在のTransformを保存
        Vector3 originalScale = this.transform.localScale;
        Vector3 originalPosition = this.transform.localPosition;
        Quaternion originalRotation = this.transform.localRotation;

        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        //SkinnedMeshRenderer MeshRenderer = this.GetComponent<SkinnedMeshRenderer>();

        //頂点
        int n_vertx = n_divx + 1;
        int n_vertz = n_divz + 1;
        Vector3[] vertices = new Vector3[n_vertx * n_vertz]; // 頂点
        Vector2[] uv = new Vector2[n_vertx * n_vertz]; // uv座標
        int k = 0;
        for (int i = 0; i <= n_divz; i++)
        {
            for (int j = 0; j <= n_divx; j++)
            {
                float u = (float)j / n_divx;
                float v = (float)i / n_divz;
                float x = (u - 0.5f) * size_x;
                float z = (v - 0.5f) * size_z;
                float y = a * x * x + b * z * z;
                vertices[k] = new Vector3(x, z, y);
                uv[k++].Set(u, v);
            }
        }

        //三角形ポリゴン
        var triangles = new int[6 * n_divx * n_divz];
        int l = 0, A = 0, B = 1, C = n_vertx, D = n_vertx + 1;
        for (int i = 0; i < n_divz; i++)
        {
            for (int j = 0; j < n_divx; j++)
            {
                //1
                triangles[l++] = A;
                triangles[l++] = C++;
                triangles[l++] = D;
                //2
                triangles[l++] = B++;
                triangles[l++] = A++;
                triangles[l++] = D++;
            }
            A++; B++; C++; D++;
        }

        //メッシュ生成
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        meshFilter.sharedMesh = mesh;

        //MeshRenderer.sharedMesh = mesh;

        // 法線とバウンディングボックスを再計算
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        this.GetComponent<MeshCollider>().sharedMesh = mesh;

        // Transformを再適用
        this.transform.localScale = originalScale;
        this.transform.localPosition = originalPosition;
        this.transform.localRotation = originalRotation;

    }
    public float ChangeSag
    {

        get { return this.Sag; }  //取得用
        set { this.Sag = value; } //値入力用
    }

    public bool EndPoint
    {

        get { return this.EndCheck; }  //取得用
        set { this.EndCheck = value; } //値入力用
    }
}

