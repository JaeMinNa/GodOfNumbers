using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipCalculationButton : MonoBehaviour
{
    public int SlotNum;
    [SerializeField] private GameObject _calculationFormulaes;
    private int _number;
    private TMP_Text _numberText;
    private CalculationFormula _calculationFormula;

    private void Awake()
    {
        SlotNum = transform.GetSiblingIndex() - 1;
        _numberText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _number = GameManager.I.DataManager.GameData.EquipNumbers[SlotNum];
        SetEquipNumber();
        SetColor();
    }

    public void NumberButton()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");

        for (int i = 0; i < _calculationFormulaes.transform.childCount; i++)
        {
            if (_calculationFormulaes.transform.GetChild(i).gameObject.activeSelf)
            {
                _calculationFormula = _calculationFormulaes.transform.GetChild(i).GetComponent<CalculationFormula>();
                break;
            }
        }

        _calculationFormula.EnterNumber(_number);
    }

    private void  SetEquipNumber()
    {
        if (_number != -1) _numberText.text = _number.ToString();
        else transform.gameObject.SetActive(false);
    }

    private void SetColor()
    {
        if (_number == 0) transform.GetComponent<Image>().color = new Color(219 / 255f, 128 / 255f, 110 / 255f, 255 / 255f);
        else if (_number >= 1 && _number <= 9) transform.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        else if (_number >= 10 && _number <= 19) transform.GetComponent<Image>().color = new Color(156 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (_number >= 20 && _number <= 29) transform.GetComponent<Image>().color = new Color(219 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (_number >= 30 && _number <= 39) transform.GetComponent<Image>().color = new Color(122 / 255f, 124 / 255f, 221 / 255f, 255 / 255f);
        else if (_number >= 40 && _number <= 49) transform.GetComponent<Image>().color = new Color(217 / 255f, 122 / 255f, 221 / 255f, 255 / 255f);
    }
}
