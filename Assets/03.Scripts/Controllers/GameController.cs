using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject _calculationFormulaes;
    private CalculationFormula _calculationFormula;

    private void Start()
    {
        SetactiveCalculationFormula();
    }

    private void SetactiveCalculationFormula()
    {
        for (int i = 0; i < _calculationFormulaes.transform.childCount; i++)
        {
            _calculationFormulaes.transform.GetChild(i).gameObject.SetActive(false);
        }

        //int index = Random.Range(0, 4);
        int index = 0;
        _calculationFormulaes.transform.GetChild(index).gameObject.SetActive(true);
    }
}
