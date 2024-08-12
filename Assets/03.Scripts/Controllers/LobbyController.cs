using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _userNameText;
    [SerializeField] private Sprite[] _userImages;
    [SerializeField] private Image _userImage;
    [SerializeField] private Image _settingsUserImage;

    [Header("Inventory")]
    [SerializeField] private GameObject _inventory;

    [Header("Shop")]
    [SerializeField] private GameObject _shop;

    [Header("GameSettings")]
    [SerializeField] private GameObject _settings;
    [SerializeField] private TMP_Text _settingsUserNameText;
    private Settings _settingsScript;

    private void Awake()
    {
        _settingsScript = _settings.GetComponent<Settings>();
    }

    private void Start()
    {
        GameManager.I.SoundManager.StartBGM("LobbyBGM");
        SetCoin();
        SetUserName();
        SetUserImage();
        SetAudio();
    }

    #region UI
    public void SetCoin()
    {
        _coinText.text = GameManager.I.DataManager.GameData.Coin.ToString();
    }

    public void SetUserName()
    {
        _userNameText.text = GameManager.I.DataManager.GameData.UserName;
        _settingsUserNameText.text = GameManager.I.DataManager.GameData.UserName;
    }

    public void SetUserImage()
    {
        _userImage.sprite = _userImages[GameManager.I.DataManager.GameData.Image];
        _settingsUserImage.sprite = _userImages[GameManager.I.DataManager.GameData.Image];
    }
    #endregion

    #region Inventory
    public void InventroyActive()
    {
        GameManager.I.SoundManager.StartSFX("ChangeButton");
        _inventory.SetActive(true);
    }

    public void InventroyInActive()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        _inventory.SetActive(false);
    }
    #endregion

    #region Shop
    public void ShopActive()
    {
        GameManager.I.SoundManager.StartSFX("ChangeButton");
        _shop.SetActive(true);
    }

    public void ShopInactive()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        _shop.SetActive(false);
    }
    #endregion

    #region GameStart
    public void GameStartButton()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        GameManager.I.ScenesManager.LoadScene("GameScene");
    }
    #endregion

    #region GameSettings
    public void SettingsActive()
    {
        GameManager.I.SoundManager.StartSFX("ChangeButton");
        _settings.SetActive(true);
    }

    public void SettingsInactive()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        _settings.SetActive(false);
    }

    private void SetAudio()
    {
        _settingsScript.SetAuido();
    }
    #endregion
}
