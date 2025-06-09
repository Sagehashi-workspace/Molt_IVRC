using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class SerialManager : MonoBehaviour
{
    public SerialHandler serialHandler;
    public CalcPositionLevel calcpositionlevel;

    //受信用変数
    public float force;            // 受信データのforce
    public bool endst = false;

    void Start()
    {
        serialHandler.OnDataReceived += OnDataReceived;
    }

    // データを受信したら
    void OnDataReceived(string message)
    {
        Debug.Log("受信データ: " + message);
        try
        {
            // JSONデータを解析
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(message);

            if (data.ContainsKey("actual_power"))
            {
                force = float.Parse(data["actual_power"].ToString());
            }

        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
        }
        sendmsg();
    }

    // オン用メソッド
    void sendmsg()
    {
        bool status = false;
        var Data = new Dictionary<string, object>
            {
                { "position_level", calcpositionlevel.position_level },
                { "end_status", status }
            };

        string jsonData = JsonConvert.SerializeObject(Data);

        // シリアルポートにデータを送信
        serialHandler.Write(jsonData);
        Debug.Log("データを送信");
        Debug.Log("position_level: " + calcpositionlevel.position_level + "  end_status: " + status);
    }
}
