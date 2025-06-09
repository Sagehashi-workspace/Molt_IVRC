using System.Collections;
using System.Collections.Generic;
using DistantLands.Cozy.Data;
using UnityEngine;

public class ChangeTerrain : MonoBehaviour
{
    // �����̒n�`���Ǘ����邽�߂̃��X�g
    [SerializeField] private List<Terrain> terrains;

    // ���݂̒n�`�̃C���f�b�N�X
    [SerializeField] private int _ChangeTerrain = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (terrains != null && terrains.Count > 0)
        {
            ChangeTerrainByIndex(0);
        }
    }
    
    // �w�肳�ꂽ�C���f�b�N�X�̒n�`�݂̂�L����
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
