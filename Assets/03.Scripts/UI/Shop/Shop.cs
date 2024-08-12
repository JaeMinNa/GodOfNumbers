using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private int _coin1;
    [SerializeField] private int _coin10;
    [SerializeField] private GameObject _completePanel;
    [SerializeField] private TMP_Text _numberText;
    [SerializeField] private Image _numberImage;
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
                SetNumberColor(value);
                break;
            }
            else
            {
                _newNumberText.gameObject.SetActive(true);
                GameManager.I.DataManager.GameData.InventoryNumbers[value] = true;
                _numberText.text = value.ToString();
                SetNumberColor(value);
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

    private void SetNumberColor(int num)
    {
        if (num == 0) _numberImage.color = new Color(219 / 255f, 128 / 255f, 110 / 255f, 255 / 255f);
        else if (num >= 10 && num <= 19) _numberImage.color = new Color(156 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (num >= 20 && num <= 29) _numberImage.color = new Color(219 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (num >= 30 && num <= 39) _numberImage.color = new Color(122 / 255f, 124 / 255f, 221 / 255f, 255 / 255f);
        else if (num >= 40 && num <= 49) _numberImage.color = new Color(217 / 255f, 122 / 255f, 221 / 255f, 255 / 255f);
    }
}
