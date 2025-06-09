using System.Collections;
using System.Collections.Generic;
using DistantLands.Cozy.Data;
using UnityEngine;

public class ChangeTerrain : MonoBehaviour
{
    // 複数の地形を管理するためのリスト
    [SerializeField] private List<Terrain> terrains;

    // 現在の地形のインデックス
    [SerializeField] private int _ChangeTerrain = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (terrains != null && terrains.Count > 0)
        {
            ChangeTerrainByIndex(0);
        }
    }
    
    // 指定されたインデックスの地形のみを有効化
    public void ChangeTerrainByIndex(int index)
    {
        if (index < 0 || index >= terrains.Count)
        {
            Debug.LogWarning("Invalid Terrain index.");
            return;
        }
        foreach (Terrain terrain in terrains)
        {
            terrain.gameObject.SetActive(false);
        }

        terrains[index].gameObject.SetActive(true);
    }

    public void ChangeIndex(int index)
    {
        if (index < 0 || index >= terrains.Count)
        {
            Debug.LogWarning("Invalid Terrain index.");
            return;
        }
        _ChangeTerrain = index;
    }
}
