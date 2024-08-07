using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string CurrentSceneName;

    // �ʱ�ȭ
    public void Init()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;
    }

    // �޸� ����
    public void Release()
    {

    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}

