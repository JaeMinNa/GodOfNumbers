using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private GameObject _inventory;

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

    #region GameStart
    public void GameStartButton()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        GameManager.I.ScenesManager.LoadScene("GameScene");
    }
    #endregion
}
