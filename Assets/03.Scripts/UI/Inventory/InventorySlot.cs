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

    public void ShowInventoryNumber()
    {
        _slotNum = transform.GetSiblingIndex();
        _frame = transform.GetChild(0).gameObject;
        _button = transform.GetChild(1).gameObject;
        _number = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();

        SetColor();
        NoEquipNumber();
        _number.text = _slotNum.ToString();

        if (GameManager.I.DataManager.GameData.InventoryNumbers[_slotNum]) EquipNumber();  
    }

    private void NoEquipNumber()
    {
        Color color = new Color(_buttonColor.r, _buttonColor.g, _buttonColor.b, 100 / 255f);

        _frame.GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 100 / 255f);
        _button.GetComponent<Image>().color = color;
        _number.GetComponent<TextMeshProUGUI>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 100 / 255f);
    }

    private void EquipNumber()
    {
        Color color = new Color(_buttonColor.r, _buttonColor.g, _buttonColor.b, 255 / 255f);

        _frame.GetComponent<Image>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
        _button.GetComponent<Image>().color = color;
        _number.GetComponent<TextMeshProUGUI>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
    }
    
    private void SetColor()
    {
        if (_slotNum == 0) _buttonColor = new Color(219 / 255f, 128 / 255f, 110 / 255f, 255 / 255f);
        else if (_slotNum >= 1 && _slotNum <= 9) _buttonColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        else if (_slotNum >= 10 && _slotNum <=19) _buttonColor = new Color(156 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (_slotNum >= 20 && _slotNum <= 29) _buttonColor = new Color(219 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (_slotNum >= 30 && _slotNum <= 39) _buttonColor = new Color(122 / 255f, 124 / 255f, 221 / 255f, 255 / 255f);
        else if (_slotNum >= 40 && _slotNum <= 49) _buttonColor = new Color(217 / 255f, 122 / 255f, 221 / 255f, 255 / 255f);

        _button.GetComponent<Image>().color = _buttonColor;
    }

}
