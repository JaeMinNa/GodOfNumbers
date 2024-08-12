using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _coinText;

    [Header("Inventory")]
    [SerializeField] private GameObject _inventory;

    [Header("Shop")]
    [SerializeField] private GameObject _shop;

    private void Start()
    {
        SetCoin();
    }

    #region UI
    public void SetCoin()
    {
        _coinText.text = GameManager.I.DataManager.GameData.Coin.ToString();
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
}
