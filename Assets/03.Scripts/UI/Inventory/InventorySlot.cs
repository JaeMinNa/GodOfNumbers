using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int _slotNum;
    private GameObject _frame;
    private GameObject _button;
    private TMP_Text _number;
    private Color _buttonColor;
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = transform.parent.parent.parent.parent.parent.GetComponent<Inventory>();
    }

    public void EquipButton()
    {
        if (IsGet(_slotNum) && !IsEquip(_slotNum))
        {
            int count = 0;

            for (int i = 0; i < GameManager.I.DataManager.GameData.EquipNumbers.Length; i++)
            {
                if (GameManager.I.DataManager.GameData.EquipNumbers[i] != -1) count++;
            }

            if (count == 5) GameManager.I.SoundManager.StartSFX("MissButton");
            else
            {
                GameManager.I.SoundManager.StartSFX("ClickButton");

                for (int i = 0; i < GameManager.I.DataManager.GameData.EquipNumbers.Length; i++)
                {
                    if (GameManager.I.DataManager.GameData.EquipNumbers[i] == -1)
                    {
                        GameManager.I.DataManager.GameData.EquipNumbers[i] = _slotNum;
                        _inventory.SetInventory();
                        break;
                    }
                }
            }
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("MissButton");
        }
    }

    public void ShowInventoryNumber()
    {
        _slotNum = transform.GetSiblingIndex();
        _frame = transform.GetChild(0).gameObject;
        _button = transform.GetChild(1).gameObject;
        _number = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        _number.text = _slotNum.ToString();

        SetColor();
        NoGetNumber();
        if (GameManager.I.DataManager.GameData.InventoryNumbers[_slotNum] && !(_slotNum >=1 && _slotNum <= 9)) GetNumber();  
    }

    private void NoGetNumber()
    {
        Color color = new Color(_buttonColor.r, _buttonColor.g, _buttonColor.b, 100 / 255f);
        _frame.GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 100 / 255f);
        _button.GetComponent<Image>().color = color;
        _number.GetComponent<TextMeshProUGUI>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 100 / 255f);
    }

    private void GetNumber()
    {
        Color color = new Color(_buttonColor.r, _buttonColor.g, _buttonColor.b, 255 / 255f);
        _button.GetComponent<Image>().color = color;
        _number.GetComponent<TextMeshProUGUI>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);

        if(IsEquip(_slotNum)) _frame.GetComponent<Image>().color = new Color(255 / 255f, 148 / 255f, 150 / 255f, 255 / 255f);
        else _frame.GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
    }
    
    private void SetColor()
    {
        if (_slotNum == 0) _buttonColor = new Color(219 / 255f, 128 / 255f, 110 / 255f, 255 / 255f);
        else if (_slotNum >= 1 && _slotNum <= 9) _buttonColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 100 / 255f);
        else if (_slotNum >= 10 && _slotNum <= 19) _buttonColor = new Color(156 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (_slotNum >= 20 && _slotNum <= 29) _buttonColor = new Color(219 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (_slotNum >= 30 && _slotNum <= 39) _buttonColor = new Color(122 / 255f, 124 / 255f, 221 / 255f, 255 / 255f);
        else if (_slotNum >= 40 && _slotNum <= 49) _buttonColor = new Color(217 / 255f, 122 / 255f, 221 / 255f, 255 / 255f);

        _button.GetComponent<Image>().color = _buttonColor;
    }

    private bool IsEquip(int num)
    {
        if (_slotNum >= 1 && _slotNum <= 9) return true;

        for (int i = 0; i < GameManager.I.DataManager.GameData.EquipNumbers.Length; i++)
        {
            if(GameManager.I.DataManager.GameData.EquipNumbers[i] == num)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsGet(int num)
    {
        if (_slotNum >= 1 && _slotNum <= 9) return true;
        if (GameManager.I.DataManager.GameData.InventoryNumbers[num]) return true;

        return false;
    }

}
