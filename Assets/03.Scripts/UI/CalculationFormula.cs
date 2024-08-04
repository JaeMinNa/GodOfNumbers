using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;

public class CalculationFormula : MonoBehaviour
{
    [SerializeField] private GameObject _numberFrames;
    [SerializeField] private GameObject _calculations;
    private int _numberFramesCount;
    private int _calculationsCount;
    private int _fillNumberFrames;
    private GameController _gameController;

    private void Awake()
    {
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

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

    public void CalculateNumber()
    {
        if (_fillNumberFrames != _numberFramesCount)
        {
            GameManager.I.SoundManager.StartSFX("MissButton");
            return;
        }

        string cal = "";
        int frameCount = 0;
        int calCount = 0;

        for (int i = 0; i < _numberFramesCount + _calculationsCount; i++)
        {
            if(i % 2 == 0)
            {
                cal += _numberFrames.transform.GetChild(frameCount).GetChild(0).GetComponent<TextMeshProUGUI>().text;
                frameCount++;
            }
            else
            {
                cal += _calculations.transform.GetChild(calCount).GetChild(0).GetComponent<TextMeshProUGUI>().text;
                calCount++;
            }
        }

        string newCal = cal.Replace("×", "*");
        string newCal2 = newCal.Replace("÷", "/");
        float result = Evaluate(newCal2);
        Debug.Log("계산 결과 : " + result);

        if (_gameController.Blocks.ContainsKey(result))
        {
            GameManager.I.SoundManager.StartSFX("SuccessButton");
            GameObject obj = _gameController.Blocks[result];
            obj.GetComponent<Block>().ReduceHp();
            _gameController.GetScore(_numberFramesCount * 100);
            //_gameController.Blocks.Remove(result);
            _gameController.SetactiveCalculationFormula();
        }
        else
        {
            GameManager.I.SoundManager.StartSFX("FailButton");
            ClearNumber();
        }
    }

    private float Evaluate(string expression)
    {
        DataTable table = new DataTable();
        table.Columns.Add("expression", typeof(string), expression);
        DataRow row = table.NewRow();
        table.Rows.Add(row);
        float result = float.Parse((string)row["expression"]);
        return result;
    }

    private void RandomCalculations()
    {
        for (int i = 0; i < _calculationsCount; i++)
        {
            int index = Random.Range(0, 4);
            string calculation = "";

            if (index == 0) calculation = "+";
            else if (index == 1) calculation = "-";
            else if (index == 2) calculation = "×";
            else if (index == 3) calculation = "÷";

            _calculations.transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = calculation;
        }
    }
}
