using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SerialManager : MonoBehaviour
{
    public SerialHandler serialHandler;
    public InputMode inputMode;

    //受信用変数
    private float velocity; //デバイスの移動速度(cm/s)
    private float displacement; //デバイスの変位(cm)
    private float battery; //バッテリー残量(%)
    private float ArmLength = 0;
    public bool check_length = false;
    private string tmpmode = "0";
    private long rcount = 0;

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
    }

    // データを受信したら
    void OnDataReceived(string message)
    {
        float v = 0.0f;
        float x = 0.0f;
        float b = 0;
        try
        {
            // JSONデータを解析
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(message);

            if (rcount >= 3)
            {
                if (data.ContainsKey("v"))
                {
                    v = float.Parse(data["v"].ToString());
                }
                if (data.ContainsKey("x"))
                {
                    x = float.Parse(data["x"].ToString());
                }
                if (data.ContainsKey("b"))
                {
                    b = float.Parse(data["b"].ToString());
                }
                Debug.Log("受信データ: " + "verocity: " + v + " displacement: " + x + " battery: " + b);
                velocity = v;
                displacement = x;
                battery = b;
            }
            rcount++;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        sendmsg();
    }

    private void Update()
    {
        sendmsg();
    }

    // オン用メソッド
    void sendmsg()
    {
        if (tmpmode != inputMode.mode)
        {
            if (inputMode.mode == "r")
            {
                ArmLength = -displacement;
                check_length = true;
                rcount = 0;
            }
            // シリアルポートにデータを送信
            serialHandler.Write(inputMode.mode);
            Debug.Log("データを送信");
            Debug.Log("mode: " + inputMode.mode);
            tmpmode = inputMode.mode;
        }
    }

    public float Getx()
    {
        return displacement;
    }
    
    public float Getv()
    {
        return velocity;
    }

    public float GetLength()
    {
        return ArmLength;
    }
}