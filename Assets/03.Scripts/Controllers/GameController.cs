using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject _calculationFormulaes;

    [Header("GameData")]
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _scoreText;
    private int _hp;
    private int _score;

    [Header("Block")]
    public Dictionary<float, GameObject> Blocks;
    public GameObject BlockPrefab;
    public float BlockCreateTime;

    private void Awake()
    {
        Blocks = new Dictionary<float, GameObject>();
    }

    private void Start()
    {
        SetactiveCalculationFormula();
        StartCoroutine(COCreateBlock());
        _score = 0;
        _hp = 3;
        GetScore(0);
        LoseHp(0);
    }


    #region CalculationFormula
    public void SetactiveCalculationFormula()
    {
        for (int i = 0; i < _calculationFormulaes.transform.childCount; i++)
        {
            _calculationFormulaes.transform.GetChild(i).gameObject.SetActive(false);
        }

        int index = 0;
        if(_score <= 5000) index = Random.Range(0, 1);

        _calculationFormulaes.transform.GetChild(index).gameObject.SetActive(true);
    }
    #endregion

    #region Block
    IEnumerator COCreateBlock()
    {
        while (true)
        {
            CreateBlock(10);

            yield return new WaitForSeconds(BlockCreateTime);
        }
    }

    private void CreateBlock(int range) // 0 ~ range Áß ·£´ý »ý¼º
    {
        while (true)
        {
            int num = Random.Range(0, range + 1);
            if (Blocks.ContainsKey(num)) continue;
            else
            {
                float value = Random.Range(-2f, 2f);
                GameObject obj = Instantiate(BlockPrefab, new Vector3(value, 5.2f, 0), Quaternion.identity);
                obj.GetComponent<Block>().SetBlockNumber(num);
                Blocks.Add(num, obj);
                break;
            }
        }
    }
    #endregion

    #region GameData
    public void GetScore(int value)
    {
        _score += value;
        _scoreText.text = "Score : " + _score;
    }

    public void LoseHp(int value)
    {
        _hp -= value;
        _hpText.text = "HP : " + _hp + " / 3";
    }
    #endregion
}
