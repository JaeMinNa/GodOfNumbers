using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [Header("Settings")]
    public string UserName;
    public int BestScore;
    public int Image;

    [Header("Inventory")]
    public bool[] InventoryNumbers = new bool[50];
    public int[] EquipNumbers = new int[5];
    public int Coin;
}

public class DataManager : MonoBehaviour
{
    public GameData GameData;

    // �ʱ�ȭ
    public void Init()
    {
        GameData = new GameData();

        for (int i = 0; i < GameData.EquipNumbers.Length; i++)
        {
            GameData.EquipNumbers[i] = -1;
        }

        for (int i = 1; i <= 9; i++)
        {
            GameData.InventoryNumbers[i] = true;
        }

        DataLoad();
    }

    // �޸� ����
    public void Release()
    {

    }

    [ContextMenu("Save Data")] // ������Ʈ �޴��� �Ʒ� �Լ��� ȣ���ϴ� Save Data ��� ��ɾ ������
    public void DataSave()
    {
        ES3.Save("GameData", GameData); // Key�� ����, ������ class ������
    }

    [ContextMenu("Load Data")]
    public void DataLoad()
    {
        if (ES3.FileExists("SaveFile.txt"))
        {
            ES3.LoadInto("GameData", GameData); // ����� Key ��, �ҷ��� class ������
        }
        else
        {
            DataSave();
        }
    }

    public void DataDelete()
    {
        // ������ ����
        ES3.DeleteFile("SaveFile.txt"); // ���� ���� �̸�
        PlayerPrefs.DeleteAll();
    }
}