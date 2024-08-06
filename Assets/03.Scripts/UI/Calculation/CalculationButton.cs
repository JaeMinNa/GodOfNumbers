using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculationButton : MonoBehaviour
{
    public int Number;
    [SerializeField] private GameObject _calculationFormulaes;
    private CalculationFormula _calculationFormula;

    private void Start()
    {
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Number.ToString();
    }

    public void NumberButton()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");

        for (int i = 0; i < _calculationFormulaes.transform.childCount; i++)
        {
            if(_calculationFormulaes.transform.GetChild(i).gameObject.activeSelf)
            {
                _calculationFormula = _calculationFormulaes.transform.GetChild(i).GetComponent<CalculationFormula>();
                break;
            }
        }

        _calculationFormula.EnterNumber(Number);
    }
}
