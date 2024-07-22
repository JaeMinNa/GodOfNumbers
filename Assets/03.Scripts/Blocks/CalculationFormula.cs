using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculationFormula : MonoBehaviour
{
    [SerializeField] private GameObject _numberFrames;
    [SerializeField] private GameObject _calculations;
    private int _numberFramesCount;
    private int _calculationsCount;
    private int _fillNumberFrames;

    private void OnEnable()
    {
        if (_numberFramesCount == 0) return;

        _fillNumberFrames = 0;
        ResetNumberFrames();
        RandomCalculations();
    }

    private void Start()
    {
        _numberFramesCount = _numberFrames.transform.childCount;
        _calculationsCount = _calculations.transform.childCount;
        _fillNumberFrames = 0;
        ResetNumberFrames();
        RandomCalculations();
    }

    private void ResetNumberFrames()
    {
        for (int i = 0; i < _numberFramesCount; i++)
        {
            _numberFrames.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void EnterNumber(int num)
    {
        if (_fillNumberFrames == _numberFramesCount) return;

        _numberFrames.transform.GetChild(_fillNumberFrames).GetChild(0).GetComponent<TextMeshProUGUI>().text = num.ToString();
        _fillNumberFrames++;
    }

    public void ClearNumber()
    {
        ResetNumberFrames();
        _fillNumberFrames = 0;
    }

    private void RandomCalculations()
    {
        for (int i = 0; i < _calculationsCount; i++)
        {
            int index = Random.Range(0, 4);
            string calculation = "";

            if (index == 0) calculation = "+";
            else if (index == 1) calculation = "-";
            else if (index == 2) calculation = "¡¿";
            else if (index == 3) calculation = "¡À";

            _calculations.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = calculation;
        }
    }
}
