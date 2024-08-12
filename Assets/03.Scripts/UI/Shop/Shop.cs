using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private int _coin1;
    [SerializeField] private int _coin10;
    [SerializeField] private GameObject _completePanel;
    [SerializeField] private TMP_Text _numberText;
    [SerializeField] private TMP_Text _newNumberText;
    [SerializeField] private TMP_Text _originNumberText;
    private int _count;
    private LobbyController _lobbyController;

    private void Awake()
    {
        _lobbyController = GameObject.FindWithTag("LobbyController").GetComponent<LobbyController>();
    }

    private void Update()
    {
        Debug.Log(_count);
    }

    public void BuyNumberButton(int count)
    {
        if (count == 1)
        {
            if (GameManager.I.DataManager.GameData.Coin >= _coin1) GameManager.I.DataManager.GameData.Coin -= _coin1;
            else
            {
                GameManager.I.SoundManager.StartSFX("MissButton");
                return;
            }
        }
        else if (count == 10)
        {
            if (GameManager.I.DataManager.GameData.Coin >= _coin10) GameManager.I.DataManager.GameData.Coin -= _coin10;
            else
            {
                GameManager.I.SoundManager.StartSFX("MissButton");
                return;
            }
        }

        GameManager.I.SoundManager.StartSFX("BuyButton");
        _lobbyController.SetCoin();
        _count = count;
        _newNumberText.gameObject.SetActive(false);
        _originNumberText.gameObject.SetActive(false);

        while (true)
        {
            int value = Random.Range(0, 50);

            if (value >= 1 && value <= 9) continue;

            if (GameManager.I.DataManager.GameData.InventoryNumbers[value])
            {
                _originNumberText.gameObject.SetActive(true);
                _numberText.text = value.ToString();
                break;
            }
            else
            {
                _newNumberText.gameObject.SetActive(true);
                GameManager.I.DataManager.GameData.InventoryNumbers[value] = true;
                _numberText.text = value.ToString();
                break;
            }

        }

        _completePanel.SetActive(true);
    }

    public void BuyCompleteButton()
    {
        int count = _count;
        count--;
        _count = count;
        _completePanel.SetActive(false);

        if (count > 0) BuyNumberButton(count);
        else GameManager.I.SoundManager.StartSFX("ClickButton");
    }
}
