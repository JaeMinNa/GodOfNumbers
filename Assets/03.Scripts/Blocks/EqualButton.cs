using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqualButton : MonoBehaviour
{
    [SerializeField] GameObject _calculationFormulaes;
    private CalculationFormula _calculationFormula;

    public void EqualNumberButton()
    {
        for (int i = 0; i < _calculationFormulaes.transform.childCount; i++)
        {
            if (_calculationFormulaes.transform.GetChild(i).gameObject.activeSelf)
            {
                _calculationFormula = _calculationFormulaes.transform.GetChild(i).GetComponent<CalculationFormula>();
                break;
            }
        }

        _calculationFormula.CalculateNumber();
    }
}
