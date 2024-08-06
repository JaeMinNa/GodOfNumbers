using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipSlot : MonoBehaviour
{
    public int _slotNum;
    private GameObject _button;
    private TMP_Text _number;

    public void ShowEquipNumber()
    {
        _slotNum = transform.GetSiblingIndex();
        _button = transform.GetChild(1).gameObject;
        _number = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();

        _button.SetActive(false);

        if (GameManager.I.DataManager.GameData.EquipNumbers[_slotNum] == -1) return;
        else
        {
            _number.text = GameManager.I.DataManager.GameData.EquipNumbers[_slotNum].ToString();
            _button.SetActive(true);
        }
    }
}
