using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    public int _slotNum;
    private GameObject _button;
    private TMP_Text _numberText;
    private int _number;
    private Color _buttonColor;
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = transform.parent.parent.parent.parent.parent.GetComponent<Inventory>();
    }

    public void UnequipButton()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        int[] array = new int[5];
        int count = 0;
        GameManager.I.DataManager.GameData.EquipNumbers[_slotNum] = -1;

        for (int i = 0; i < array.Length; i++)
        {
            if (GameManager.I.DataManager.GameData.EquipNumbers[i] != -1)
            {
                array[count] = GameManager.I.DataManager.GameData.EquipNumbers[i];
                count++;
            }
        }

        for (int i = count; i < array.Length; i++)
        {
            array[i] = -1;
        }

        GameManager.I.DataManager.GameData.EquipNumbers = array;
        _inventory.SetInventory();
        GameManager.I.DataManager.DataSave();
    }

    public void ShowEquipNumber()
    {
        _slotNum = transform.GetSiblingIndex();
        _button = transform.GetChild(1).gameObject;
        _numberText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        _number = GameManager.I.DataManager.GameData.EquipNumbers[_slotNum];
        _button.SetActive(false);

        if (GameManager.I.DataManager.GameData.EquipNumbers[_slotNum] == -1) return;
        else
        {
            _numberText.text = _number.ToString();
            SetColor();
            _button.SetActive(true);
        }
    }

    private void SetColor()
    {
        if (_number == 0) _buttonColor = new Color(219 / 255f, 128 / 255f, 110 / 255f, 255 / 255f);
        else if (_number >= 1 && _number <= 9) _buttonColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        else if (_number >= 10 && _number <= 19) _buttonColor = new Color(156 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (_number >= 20 && _number <= 29) _buttonColor = new Color(219 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (_number >= 30 && _number <= 39) _buttonColor = new Color(122 / 255f, 124 / 255f, 221 / 255f, 255 / 255f);
        else if (_number >= 40 && _number <= 49) _buttonColor = new Color(217 / 255f, 122 / 255f, 221 / 255f, 255 / 255f);

        _button.GetComponent<Image>().color = _buttonColor;
    }
}
