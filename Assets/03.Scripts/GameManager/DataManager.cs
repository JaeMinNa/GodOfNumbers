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

    // 초기화
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

    // 메모리 해제
    public void Release()
    {

    }

    [ContextMenu("Save Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 Save Data 라는 명령어가 생성됨
    public void DataSave()
    {
        ES3.Save("GameData", GameData); // Key값 설정, 선언한 class 변수명
    }

    [ContextMenu("Load Data")]
    public void DataLoad()
    {
        if (ES3.FileExists("SaveFile.txt"))
        {
            ES3.LoadInto("GameData", GameData); // 저장된 Key 값, 불러올 class 변수명
        }
        else
        {
            DataSave();
        }
    }

    public void DataDelete()
    {
        // 데이터 삭제
        ES3.DeleteFile("SaveFile.txt"); // 저장 파일 이름
        PlayerPrefs.DeleteAll();
    }
}