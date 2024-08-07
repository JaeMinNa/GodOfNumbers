using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string CurrentSceneName;

    // 초기화
    public void Init()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;
    }

    // 메모리 해제
    public void Release()
    {

    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}

