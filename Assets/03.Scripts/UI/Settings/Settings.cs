using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [Header("User Name")]
    [SerializeField] TMP_Text _userNameText;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject _userNamePanel;

    [Header("User Image")]
    [SerializeField] GameObject[] _userImages;
    [SerializeField] GameObject _userImagePanel;

    [Header("Data")]
    [SerializeField] GameObject _deletePanel;

    private LobbyController _lobbyController;

    private void Awake()
    {
        _lobbyController = GameObject.FindWithTag("LobbyController").GetComponent<LobbyController>();
    }

    private void OnEnable()
    {
        _userNameText.text = GameManager.I.DataManager.GameData.UserName;
        SetImage();
    }

    #region User Name
    public void UserNameInput()
    {
        _userNameText.text = inputField.text;
        GameManager.I.DataManager.GameData.UserName = inputField.text;
        _lobbyController.SetUserName();
    }

    public void UserNameActive()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        _userNamePanel.SetActive(true);
    }

    public void UserNameInactive()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        _userNamePanel.SetActive(false);
    }
    #endregion

    #region User Image
    private void SetImage()
    {
        for (int i = 0; i < _userImages.Length; i++)
        {
            _userImages[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        _userImages[GameManager.I.DataManager.GameData.Image].transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SelectImageButton(int num)
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        GameManager.I.DataManager.GameData.Image = num;
        SetImage();
        _lobbyController.SetUserImage();
    }

    public void UserImageActive()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        _userImagePanel.SetActive(true);
    }

    public void UserImageInactive()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        _userImagePanel.SetActive(false);
    }
    #endregion

    #region Data
    public void DeleteActive()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        _deletePanel.SetActive(true);
    }

    public void DeleteInactive()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        _deletePanel.SetActive(false);
    }

    public void DeleteData()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        GameManager.I.DataManager.DataDelete();

        // 현재 실행 환경이 에디터이면 에디터 플레이모드 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        // 현재 실행 환경이 에디터가 아니면 프로그램 종료
        #else
        Application.Quit();
        #endif
    }
    #endregion
}
