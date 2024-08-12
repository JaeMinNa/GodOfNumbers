using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;
using UnityEngine.UI;

public class CalculationFormula : MonoBehaviour
{
    [SerializeField] private GameObject _numberFrames;
    [SerializeField] private GameObject _calculations;
    private int _numberFramesCount;
    private int _normalNumberCount;
    private int _equipNumberCount;
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
        _normalNumberCount = 0;
        _equipNumberCount = 0;
        _gameController.GetChangeCount(0);
        ResetNumberFrames();
        RandomCalculations();
    }

    private void Start()
    {
        _numberFramesCount = _numberFrames.transform.childCount;
        _calculationsCount = _calculations.transform.childCount;
        _fillNumberFrames = 0;
        _normalNumberCount = 0;
        _equipNumberCount = 0;
        ResetNumberFrames();
        RandomCalculations();
    }

    private void ResetNumberFrames()
    {
        for (int i = 0; i < _numberFramesCount; i++)
        {
            _numberFrames.transform.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            _numberFrames.transform.GetChild(i).GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        }
    }

    public void EnterNumber(int num)
    {
        if (_fillNumberFrames == _numberFramesCount) return;

        if (num >= 1 && num <= 9) _normalNumberCount++;
        else _equipNumberCount++;

        SetColor(num);
        _numberFrames.transform.GetChild(_fillNumberFrames).GetChild(1).GetComponent<TextMeshProUGUI>().text = num.ToString();
        _fillNumberFrames++;
    }

    public void ClearNumber()
    {
        ResetNumberFrames();
        _fillNumberFrames = 0;
        _normalNumberCount = 0;
        _equipNumberCount = 0;
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
                cal += _numberFrames.transform.GetChild(frameCount).GetChild(1).GetComponent<TextMeshProUGUI>().text;
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
            _gameController.GetScore((_normalNumberCount * 100) + (_equipNumberCount * 500));
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

    private void SetColor(int number)
    {
        if (number == 0) _numberFrames.transform.GetChild(_fillNumberFrames).GetComponent<Image>().color = new Color(219 / 255f, 128 / 255f, 110 / 255f, 255 / 255f);
        else if (number >= 1 && number <= 9) _numberFrames.transform.GetChild(_fillNumberFrames).GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        else if (number >= 10 && number <= 19) _numberFrames.transform.GetChild(_fillNumberFrames).GetComponent<Image>().color = new Color(156 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (number >= 20 && number <= 29) _numberFrames.transform.GetChild(_fillNumberFrames).GetComponent<Image>().color = new Color(219 / 255f, 221 / 255f, 122 / 255f, 255 / 255f);
        else if (number >= 30 && number <= 39) _numberFrames.transform.GetChild(_fillNumberFrames).GetComponent<Image>().color = new Color(122 / 255f, 124 / 255f, 221 / 255f, 255 / 255f);
        else if (number >= 40 && number <= 49) _numberFrames.transform.GetChild(_fillNumberFrames).GetComponent<Image>().color = new Color(217 / 255f, 122 / 255f, 221 / 255f, 255 / 255f);
    }
}
