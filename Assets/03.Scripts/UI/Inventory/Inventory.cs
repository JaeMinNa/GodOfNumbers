using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("Equip")]
    [SerializeField] private TMP_Text _equipText;
    [SerializeField] private GameObject _equipSlotContent;

    [Header("Inventory")]
    [SerializeField] private TMP_Text _inventoryText;
    [SerializeField] private GameObject _inventorySlotContent;


    private void OnEnable()
    {
        SetInventory();
    }

    private void SetInventory()
    {
        CountText();

        for (int i = 0; i < _equipSlotContent.transform.childCount; i++)
        {
            _equipSlotContent.transform.GetChild(i).GetComponent<EquipSlot>().ShowEquipNumber();
        }

        for (int i = 0; i < _inventorySlotContent.transform.childCount; i++)
        {
            _inventorySlotContent.transform.GetChild(i).GetComponent<InventorySlot>().ShowInventoryNumber();
        }
    }

    private void CountText()
    {
        _equipText.text = "<#f8913f>" + EquipCount().ToString() + "</color> / 5";
        _inventoryText.text = "<#f8913f>" + InventoryCount().ToString() + "</color> / 50";
    }

    private int EquipCount()
    {
        int count = 0;

        for (int i = 0; i < GameManager.I.DataManager.GameData.EquipNumbers.Length; i++)
        {
            if (GameManager.I.DataManager.GameData.EquipNumbers[i] == -1) count++;
        }

        return 5 - count;
    }

    private int InventoryCount()
    {
        int count = 0;

        for (int i = 0; i < GameManager.I.DataManager.GameData.InventoryNumbers.Length; i++)
        {
            if (!GameManager.I.DataManager.GameData.InventoryNumbers[i]) count++;
        }

        return 50 - count;
    }
}
