using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager�� Manager�� �����ϴ� �ϳ��� ���Ҹ� ��
    public DataManager DataManager { get; private set; }
    public SoundManager SoundManager { get; private set; }

    public static GameManager I;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }

        DataManager = GetComponentInChildren<DataManager>();
        SoundManager = GetComponentInChildren<SoundManager>();

        Init();
    }

    private void Init()
    {
        DataManager.Init();
        SoundManager.Init();
    }

    private void Release()
    {
        DataManager.Release();
        SoundManager.Release();
    }
}