using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MoveSkin : MonoBehaviour
{
    float speed = 0.3f;
    // ���͒l�͈̔�
    double minInput = -0.291; // �X�^�[�g�n�_��x���W
    double maxInput = -0.51; // �S�[���n�_��x���W

    // �o�͒l�͈̔�
    double minOutput = 0;
    double maxOutput = 100;

    public GameObject headParent;
    public GameObject head;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    // private float m_weight;
    private float progress;

    // �����ɑΉ�����blendshape��index�ԍ�����ꂽ���������擾�����܂������Ȃ�
    // private int key1_Index;
    // private int key2_Index;
    // private int key3_Index;
    // private int key4_Index;
    // private int key5_Index;

    // �ϐ�key1~key8��錾�@�eBlendShape��Value
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

        // �Ή�����belendShape��index�ԍ����擾���邪�A��肭�����ĂȂ��̂Ŏg���ĂȂ�(10/22)
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

        // A�L�[�i���ړ��j
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += speed * transform.right * Time.deltaTime;
        }
        Vector3 posi = this.transform.position;
        // Debug.Log("x = " + posi.x);


        double output = ((posi.x - minInput) / (maxInput - minInput)) * (maxOutput - minOutput) + minOutput;

        // �o�͒l��0����100�͈̔͂ɂȂ�܂�
        // Debug.Log(output);

        //BlendShape�̐��Ɛ��@���璲��
        progress = (float)output;
        // Debug.Log("progress"+progress);

        // �ő�ŏ�����
        if (progress < 0)
        { progress = 0; }
        if (progress > 100)
        { progress = 100; }

        Debug.Log("progress" + progress);

        // // progress�̒l�ɂ����key1~key8�ɒl����
        // // progress(0~700)�ɑΉ�����BlendShape�𐧌�@���@�o���o��
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

        // �eBlendShape(Key)�̒�����12.5 :  12.5 :  25 :    25 :     12.5 : 6.25 :  6.25�̂ɍ��킹�Ē���

        if (progress <= 12.5)
        {
            // �Ή���Ԃɑ΂���BlendShape�̒l��0~100�ɂȂ�悤�Ɋ|���Ē���
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

        // ���ʂ��o�� �f�o�b�O�p
        Debug.Log("key1: " + key1);
        // Debug.Log("key2: " + key2);
        // Debug.Log("key3: " + key3);
        // Debug.Log("key4: " + key4);
        // Debug.Log("key5: " + key5);
        // Debug.Log("key6: " + key6);
        // Debug.Log("key7: " + key7);
        // Debug.Log("key1_index: "+key1_Index);


        // index�ԍ���0:A,1:I,2:U,3:E,...�ɑΉ����Ă���@�Ή�����index�ԍ���BlendShape�̒l��ݒ�
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