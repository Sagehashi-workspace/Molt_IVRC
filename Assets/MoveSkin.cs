using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MoveSkin : MonoBehaviour
{
    float speed = 0.3f;
    // 入力値の範囲
    double minInput = -0.291; // スタート地点のx座標
    double maxInput = -0.51; // ゴール地点のx座標

    // 出力値の範囲
    double minOutput = 0;
    double maxOutput = 100;

    public GameObject headParent;
    public GameObject head;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    // private float m_weight;
    private float progress;

    // ここに対応するblendshapeのindex番号を入れたかったが取得がうまくいかない
    // private int key1_Index;
    // private int key2_Index;
    // private int key3_Index;
    // private int key4_Index;
    // private int key5_Index;

    // 変数key1~key8を宣言　各BlendShapeのValue
    private float key1 = 0;
    private float key2 = 0;
    private float key3 = 0;
    private float key4 = 0;
    private float key5 = 0;
    private float key6 = 0;
    private float key7 = 0;

    // Start is called before the first frame update
    void Start()
    {
        headParent = GameObject.Find("IVRC2024_v2.1");
        head = headParent.transform.Find("hyouhi").gameObject;
        skinnedMeshRenderer = head.GetComponent<SkinnedMeshRenderer>();

        // 対応するbelendShapeのindex番号を取得するが、上手くいってないので使ってない(10/22)
        // key1_Index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("Key1");
        // key2_Index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("key2");
        // key3_Index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("key3");
        // key4_Index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("key4");
        // key5_Index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex("key5");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position -= speed * transform.right * Time.deltaTime;
        }

        // Aキー（左移動）
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += speed * transform.right * Time.deltaTime;
        }
        Vector3 posi = this.transform.position;
        // Debug.Log("x = " + posi.x);


        double output = ((posi.x - minInput) / (maxInput - minInput)) * (maxOutput - minOutput) + minOutput;

        // 出力値は0から100の範囲になります
        // Debug.Log(output);

        //BlendShapeの数と寸法から調整
        progress = (float)output;
        // Debug.Log("progress"+progress);

        // 最大最小調整
        if (progress < 0)
        { progress = 0; }
        if (progress > 100)
        { progress = 100; }

        Debug.Log("progress" + progress);

        // // progressの値によってkey1~key8に値を代入
        // // progress(0~700)に対応してBlendShapeを制御　寸法バラバラ
        // progress=(float)output*7;
        // if (progress <= 100)
        // {
        //     key1 = progress;
        // }
        // else if (progress <= 200)
        // {
        //     key2 = progress - 100;
        // }
        // else if (progress <= 300)
        // {
        //     key3 = progress - 200;
        // }
        // else if (progress <= 400)
        // {
        //     key4 = progress - 300;
        // }
        // else if (progress <= 500)
        // {
        //     key5 = progress - 400;
        // }
        // else if (progress <= 600)
        // {
        //     key6 = progress - 500;
        // }
        // else if (progress <= 700)
        // {
        //     key7 = progress - 600;
        // }

        // 各BlendShape(Key)の長さが12.5 :  12.5 :  25 :    25 :     12.5 : 6.25 :  6.25のに合わせて調整

        if (progress <= 12.5)
        {
            // 対応区間に対してBlendShapeの値が0~100になるように掛けて調整
            key1 = (int)Math.Round(progress) * 8;
        }
        else if (progress <= 25)
        {
            key2 = (int)Math.Round(progress - 12.5) * 8;
        }
        else if (progress <= 50)
        {
            key3 = (int)Math.Round(progress - 25) * 4;
        }
        else if (progress <= 75)
        {
            key4 = (int)Math.Round(progress - 50) * 4;
        }
        else if (progress <= 87.5)
        {
            key5 = (int)Math.Round(progress - 75) * 8;
        }
        else if (progress <= 93.75)
        {
            key6 = (int)Math.Round(progress - 87.5) * 16;
        }
        else if (progress <= 100)
        {
            key7 = (int)Math.Round(progress - 93.75) * 16;
        }

        // 結果を出力 デバッグ用
        Debug.Log("key1: " + key1);
        // Debug.Log("key2: " + key2);
        // Debug.Log("key3: " + key3);
        // Debug.Log("key4: " + key4);
        // Debug.Log("key5: " + key5);
        // Debug.Log("key6: " + key6);
        // Debug.Log("key7: " + key7);
        // Debug.Log("key1_index: "+key1_Index);


        // index番号は0:A,1:I,2:U,3:E,...に対応している　対応するindex番号のBlendShapeの値を設定
        skinnedMeshRenderer.SetBlendShapeWeight(0, key1);
        skinnedMeshRenderer.SetBlendShapeWeight(1, key2);
        skinnedMeshRenderer.SetBlendShapeWeight(2, key3);
        skinnedMeshRenderer.SetBlendShapeWeight(3, key4);
        skinnedMeshRenderer.SetBlendShapeWeight(4, key5);
        skinnedMeshRenderer.SetBlendShapeWeight(5, key6);
        skinnedMeshRenderer.SetBlendShapeWeight(6, key7);
        // skinnedMeshRenderer.SetBlendShapeWeight(key1_Index, key1);
        // skinnedMeshRenderer.SetBlendShapeWeight(key2_Index, key2);
        // skinnedMeshRenderer.SetBlendShapeWeight(key3_Index, key3);
        // skinnedMeshRenderer.SetBlendShapeWeight(key4_Index, key4);
        // skinnedMeshRenderer.SetBlendShapeWeight(key5_Index, key5);


    }
}